using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject quitPanel;
    public GameObject profileInputPanel;
    public Button soundButton;
    public Text input;
    public Text profileText;
    public Text soundText;
    public Sprite soundOn, soundOff;

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

    void Start()
    {
        profileText.text = PersistantManager.GetProfile();
    }

    public void OnProfileUpdated()
    {
        string newProfile = input.text;
        if (newProfile.Length > 0)
        {
            profileText.text = newProfile;
            PersistantManager.SaveProfile(newProfile);
        }
        profileInputPanel.SetActive(false);
    }

    public void CancelProfileUpdate()
    {
        profileInputPanel.SetActive(false);
    }

    public void ShowProfilePanel()
    {
        profileInputPanel.SetActive(true);
    }

    public void ToggleSound()
    {
        Image soundImage = soundButton.GetComponent<Image>();
        if (soundImage.sprite == soundOn)
        {
            soundImage.sprite = soundOff;
            AudioListener.pause = true;
            soundText.text = "Off";
        }
        else
        {
            soundImage.sprite = soundOn;
            AudioListener.pause = false;
            soundText.text = "On";
        }
    }
}
