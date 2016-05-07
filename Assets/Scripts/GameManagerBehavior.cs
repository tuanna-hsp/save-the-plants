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
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public GameObject quitPanel;

    public GameObject[] mapPrefabs;

	public bool gameEnded = false;

    private int waveCount;
    public int WaveCount
    {
        set {
            waveCount = value;
            waveLabel.text = (wave + 1) + "/" + WaveCount;
        }

        get {
            return waveCount;
        }
    }

	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (!gameEnded) {
				for (int i = 0; i < nextWaveLabels.Length; i++) {
					nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				}
			}
			waveLabel.text = (wave + 1) + "/" + WaveCount;
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
			if (health <= 0 && !gameEnded) {
                doGameOver();
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
        
        // Instantiate map
        switch (PersistantManager.getSelectedMap())
        {
            case 1:
                GameObject.Instantiate(mapPrefabs[0], mapPrefabs[0].transform.position, Quaternion.identity);
                break;
            case 2:
                GameObject.Instantiate(mapPrefabs[1], mapPrefabs[1].transform.position, Quaternion.identity);
                break;
            case 3:
                GameObject.Instantiate(mapPrefabs[2], mapPrefabs[2].transform.position, Quaternion.identity);
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Esc (PC) or back button (Android)
        if (Input.GetKey(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
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
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
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

    public void nextLevel()
    {

    }

    public void doGameOver()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
    }

    public void doGameWon()
    {
        // Calculate score
        int score = gold;
        int healthRemaining = 0;
        foreach (GameObject indicator in healthIndicator)
        {
            if (indicator.activeInHierarchy)
            {
                healthRemaining++;
            }
        }
        score += healthRemaining * 1000;
        int star = score / (healthIndicator.Length * 1000 / 3);

        gameEnded = true;
        gameWonPanel.SetActive(true);
        gameWonPanel.GetComponent<GameWonBehaviour>().setData(score, star);
    }

    public void CancelQuit()
    {
        quitPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

public enum Difficulty
{
    EASY = 1, MEDIUM = 2, HARD = 3
}