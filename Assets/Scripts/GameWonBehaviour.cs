using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameWonBehaviour : MonoBehaviour
{
    public Text scoreText;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setData(int score, int starCount)
    {
        scoreText.GetComponent<Text>().text = "Score: " + score;

        if (starCount >= 1)
        {
            star1.SetActive(true);
            star1.GetComponent<Animator>().SetTrigger("Appear");
        }
        if (starCount >= 2)
        {
            star2.SetActive(true);
            star2.GetComponent<Animator>().SetTrigger("Appear");
        }
        if (starCount >= 3)
        {
            star3.SetActive(true);
            star3.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
