using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public Text description;

	public void OnEasyButtonCLick()
    {
        PersistantManager.SetDifficulty(Difficulty.EASY);
        description.GetComponent<Text>().text = "Reduce the number of enemies by half and increase spawn interval";
    }

    public void OnMediumButtonClick()
    {
        PersistantManager.SetDifficulty(Difficulty.MEDIUM);
        description.GetComponent<Text>().text = "Standard enemy quatity and spawn interval";
    }

    public void OnInsaneButtonClick()
    {
        PersistantManager.SetDifficulty(Difficulty.HARD);
        description.GetComponent<Text>().text = "Double the number of enemies and reduce spawn interval.";
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("map");
    }

    public void OnQuitToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Update()
    {
        // Esc (PC) or back button (Android)
        if (Input.GetKey(KeyCode.Escape))
        {
            OnBackButtonClick();
        }
    }
}
