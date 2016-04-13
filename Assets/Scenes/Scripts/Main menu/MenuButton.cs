using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{

    public GameObject pressed;
    public GameObject unpressed;

    void OnMouseDown()
    {
        pressed.SetActive(true);
        unpressed.SetActive(false);
    }

    void OnMouseUp()
    {
        pressed.SetActive(false);
        unpressed.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        pressed.SetActive(false);
        unpressed.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
