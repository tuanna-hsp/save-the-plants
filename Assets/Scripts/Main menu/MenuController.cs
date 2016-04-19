using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public void OnPlayButtonClick()
    {
        Application.LoadLevel("map");
    }

    public void OnHighscoreButtonClick() {
        Application.LoadLevel("highscore");
    }

    public void OnTutorialButtonClick()
    {

    }
}
