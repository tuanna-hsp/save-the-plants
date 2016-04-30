using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {

	public Text goldLabel;
	private int gold;
	public int Gold {
  		get { return gold; }
  		set {
			gold = value;
    		goldLabel.GetComponent<Text>().text = "" + gold;
		}
	}

	public Text waveLabel;
	public GameObject[] nextWaveLabels;
    public GameObject pauseMenu;
    public GameObject tutorial;

	public bool gameOver = false;

	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (!gameOver) {
				for (int i = 0; i < nextWaveLabels.Length; i++) {
					nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				}
			}
			waveLabel.text = "" + (wave + 1);
		}
	}

	public Text healthLabel;
	public GameObject[] healthIndicator;

	private int health;
	public int Health {
		get { return health; }
		set {
			// 1
			if (value < health) {
				Camera.main.GetComponent<CameraShake>().Shake();
			}
			// 2
			health = value;
			healthLabel.text = "" + health;
			// 2
			if (health <= 0 && !gameOver) {
				gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
			// 3 
			for (int i = 0; i < healthIndicator.Length; i++) {
				if (i < Health) {
					healthIndicator[i].SetActive(true);
				} else {
					healthIndicator[i].SetActive(false);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		Gold = 1000;
		Wave = 0;
		Health = healthIndicator.Length;

        if (!PersistantManager.IsTutorialShown())
        {
            ShowTutorial();
            PersistantManager.SetTutorialAsShown();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PauseGame()
    {
        Debug.Log("Game is pausing");
        Time.timeScale = 0;
        
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Game is resuming");
        Time.timeScale = 1;
        
        pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void ShowTutorial()
    {
        tutorial.SetActive(true);
        // Pause game
        Time.timeScale = 0;
    }

    public void HideTutorial()
    {
        tutorial.SetActive(false);
        Time.timeScale = 1;
    }
}

public enum Difficulty
{
    EASY = 1, MEDIUM = 2, HARD = 3
}