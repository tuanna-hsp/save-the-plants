using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject quitPanel;

	public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("map");
    }

    public void OnHighscoreButtonClick() {
        SceneManager.LoadScene("highscore");
    }

    public void OnTutorialButtonClick()
    {

    }

    void Update()
    {
        // Esc (PC) or back button (Android)
        if (Input.GetKey(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
    }

    public void ConfirmQuit()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        quitPanel.SetActive(false);
    }
}
