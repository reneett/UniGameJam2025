using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button pause;
    [SerializeField] private Button resume;
    [SerializeField] private Image pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
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
        pauseMenu.gameObject.SetActive(true);
    }

    public void clickResume()
    {
        Time.timeScale = 1f;

        pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
    }
}
