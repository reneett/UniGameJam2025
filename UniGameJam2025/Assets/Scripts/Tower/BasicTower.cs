using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BasicTower : MonoBehaviour
{

    public Transform rotationPoint;


    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject upgradeScreen;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMPro.TextMeshProUGUI upgradeText;
    [SerializeField] private TMPro.TextMeshProUGUI upgradeCost;
    [SerializeField] private Button closeButton;
    public UIManager uiManager;
    public TowerHandler towerHandler;

    //bullet vars stolen from Lia
    [SerializeField] private GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireCooldown = 1f; //seconds between shots
    private float lastFireTime = -Mathf.Infinity;     //time the last shot was fired

    [SerializeField] private CircleCollider2D detectionCollider;
    public float radius = 5f; //tower circle collider size
    public float radiusModifier = 1.25f; //what the radius changes by
    public float speed = 1f; //speed modifier, which decreases cooldown
    public float speedModifier = .95f; //what the speed decreases by
    public float damage = 20f; //damage of each bullet
    public float damageModifier = 1.25f; //what the damage increases by
    public bool freezable = true;
    private int frozen = 0;

    public List<Star> trackingStars; //list of currently tracked stars
    public Transform target; //current star being tracked
    public int currentUpgrade = 0; // 1: Radius, 2: Speed, 3: Damage
    private String[] upgrades = new string[] { "Radius", "Speed", "Damage" };
    public int[] upgradeLevels = new int[] { 1, 1, 1 };
    [SerializeField] public int currentCost = 25;
    

    void Start()
    {
        target = null;
        detectionCollider.radius = radius;

        upgradeButton.onClick.AddListener(UpgradeController);
        closeButton.onClick.AddListener(CloseUpgrader);

        //radius visual setup
        lineRenderer.positionCount = 500 + 1; // +1 to close the circle
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false; // local positions
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        DrawRadius();
    }

    // Update is called once per frame
    void Update()
    {
        FindNextStar();
        RotateGun();

        if (target != null && Time.time >= lastFireTime + fireCooldown)
        {
            float angle = MathF.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject bullet = Instantiate(projectilePrefab, rotationPoint.position, targetRotation);
            bullet.GetComponent<BulletScript>().dmg = damage;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.linearVelocity = (target.transform.position - transform.position).normalized * projectileSpeed;
            lastFireTime = Time.time;
        }

        if (upgradeScreen.activeSelf)
        { 
            if (uiManager.currMoney < currentCost)
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }
        }
    }

    //finds the closest star within range
    private void FindNextStar()
    {
        if (trackingStars.Count() <= 0)
        {
            target = null;
            trackingStars.Clear();
        }
        else
        {
            target = trackingStars[0].transform;
        }
        return;
    }

    //rotates tower to be facing star
    private void RotateGun()
    {
        if (target != null)
        {
            float angle = MathF.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, 200f * Time.deltaTime);
        }
    }

    public void UpgradeController()
    {
        Debug.Log("Upgrading");
        switch (currentUpgrade)
        {
            case 1:
                UpgradeRadius();
                break;
            case 2:
                UpgradeSpeed();
                break;
            case 3:
                UpgradeDamage();
                break;
            default:
                Debug.Log("no current upgrade");
                break;
        }
        uiManager.changeMoney(-1*currentCost);
        currentCost = (int) (currentCost * 1.5);
        currentUpgrade = 0;
        CloseUpgrader();
    }

    private void UpgradeSpeed()
    {
        speed *= speedModifier;
        upgradeLevels[1] += 1;
    }

    private void UpgradeRadius()
    {
        radius *= radiusModifier;
        upgradeLevels[0] += 1;
        UpdateCollider();
    }

    private void UpgradeDamage()
    {
        upgradeLevels[2] += 1;
        damage *= damageModifier;
    }

    private void UpdateCollider()
    {
        detectionCollider.radius = radius;
        DrawRadius();
    }

    //redraws the detection radius
    public void DrawRadius()
    {
        float angleStep = 360f / 500;
        for (int i = 0; i <= 500; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    public void CloseUpgrader()
    {
        upgradeScreen.SetActive(false);
        lineRenderer.enabled = false;
    }

    //opens the upgrade screen when selected
    public void Clicked()
    {
        if (!upgradeScreen.activeSelf)
        {
            towerHandler.SwapTower(this);

            //display attack radius
            lineRenderer.enabled = true;

            if (currentUpgrade == 0)
            {
                currentUpgrade = UnityEngine.Random.Range(1, 4);
            }
            upgradeText.text = upgrades[currentUpgrade - 1];
            upgradeCost.text = '$' + currentCost.ToString();

            if (uiManager.currMoney < currentCost)
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }
            upgradeScreen.SetActive(true);
        }

    }

    public void StackFrost(float duration, float modifier)
    {
        if (freezable && frozen < 3)
        {
            frozen += 1;
            StartCoroutine(SetCooldown(duration, modifier));
        }
    }

    IEnumerator SetCooldown(float duration, float modifier)
    {
        fireCooldown *= modifier;
        yield return new WaitForSeconds(duration);
        fireCooldown /= modifier;
        frozen -= 1;
    }
}
