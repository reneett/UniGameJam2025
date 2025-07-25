using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //pause vars
    [SerializeField] private Button pause;
    [SerializeField] private Button resume;
    [SerializeField] private Image pauseMenu;
    [SerializeField] private Image quit;

    //stat UI vars
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI waveNum;

    [SerializeField] private Button nextWave;

    //stat vars
    public int currMoney = 100;
    public int currHealth = 10;
    public int currWave = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        nextWave.gameObject.SetActive(false);

        money.text = currMoney.ToString();
        health.text = currHealth.ToString();
        waveNum.text = "Wave " + currWave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickPause()
    {
        Time.timeScale = 0f;

        pause.gameObject.SetActive(false);
        resume.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(true);
    }

    public void clickResume()
    {
        Time.timeScale = 1f;

        pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
    }

    public void clickQuit()
    {
        Application.Quit();

        //if testing in editor, use: UnityEditor.EditorApplication.isPlaying = false;
    }

    public void changeMoney(int addMoney)
    {
        currMoney += addMoney;
        money.text = currMoney.ToString();
    }
    public void changeHealth(int loseHealth)
    {
        currHealth -= loseHealth;
        health.text = currHealth.ToString();
    }

    public void progressWave()
    {
        nextWave.gameObject.SetActive(false);
        currWave++;
        waveNum.text = "Wave " + currWave.ToString();
    }
}
