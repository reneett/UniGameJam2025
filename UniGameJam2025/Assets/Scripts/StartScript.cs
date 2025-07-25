using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScript : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button credits;
    [SerializeField] private Button back;
    [SerializeField] private Image background;

    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private TextMeshProUGUI title;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //play.enabled = true;
        //credits.enabled = true;
        //background.enabled = true;
        //title.enabled = true;
        play.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        title.gameObject.SetActive(true);

        //back.enabled = false;
        //creditText.enabled = false;
        back.gameObject.SetActive(false);
        creditText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void clickCredits()
    {
        play.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        title.gameObject.SetActive(false);

        back.gameObject.SetActive(true);
        creditText.gameObject.SetActive(true);
    }

    public void clickBack()
    {
        play.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        title.gameObject.SetActive(true);

        back.gameObject.SetActive(false);
        creditText.gameObject.SetActive(false);
    }

    public void clickQuit()
    {
        Application.Quit();

        //if testing in editor, use: UnityEditor.EditorApplication.isPlaying = false;
    }
}
