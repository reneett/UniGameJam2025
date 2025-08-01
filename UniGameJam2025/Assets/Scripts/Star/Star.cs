using System;
using UnityEngine;
using UnityEngine.Splines;

public class Star : MonoBehaviour
{
    public SplineContainer splineContainer;
    public float speed = 2f;
    private float currentSplineProgress = 0f;

    public SpriteRenderer spriteRenderer;

    // public HealthBar healthBar;
    public float maxHP = 100f;
    public float currentHp;

    //uimanager
    private UIManager uiMan;
    public static event Action<Star> OnExplode;
    void Start()
    {
        uiMan = FindObjectOfType<UIManager>();

        currentHp = maxHP;
        if (splineContainer)
        {
            transform.position = splineContainer.Spline.EvaluatePosition(0f);
        }
    }
    void Update()
    {
        if (splineContainer == null) return;

        float length = splineContainer.Spline.GetLength();
        float step = speed * Time.deltaTime / length;
        currentSplineProgress += step;
        currentSplineProgress = Mathf.Clamp01(currentSplineProgress); // progress is between 0 & 1, so don't let it go over

        Vector3 position = splineContainer.Spline.EvaluatePosition(currentSplineProgress);
        transform.position = position;

    }

    public void TakeDamage(float amount)
    {
        currentHp = Math.Max(0, currentHp - amount);
        // healthBar.SetHealth(currentHp, maxHP);
        UpdateColor();
        if (currentHp <= 0f)
        {
            uiMan.changeMoney(10);
            Explode();
        }
    }

    public void UpdateColor()
    {
        float damagePercent = 1f - (currentHp / maxHP);
        Color color = Color.Lerp(Color.white, Color.red, damagePercent);
        spriteRenderer.color = color;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Debug.Log("Collision Detected");
            TakeDamage(20);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Earth"))
        {
            Debug.Log("Hit Earth");
            uiMan.changeHealth(1);
            Explode();
        }
    }

  public void Explode()
    {
        OnExplode?.Invoke(this);
        Destroy(gameObject);
    }
}
