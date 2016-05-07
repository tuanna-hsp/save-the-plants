using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour {

    public void OnMap1ButtonCLick()
    {
        PersistantManager.SetSelectedMap(1);
    }

    public void OnMap2ButtonClick()
    {
        PersistantManager.SetSelectedMap(2);
    }

    public void OnMap3ButtonClick()
    {
        PersistantManager.SetSelectedMap(3);
    }

    public void OnBackButtonClicl()
    {
        SceneManager.LoadScene("menu");
    }

    public void OnContinueButtonClick()
    {
        SceneManager.LoadScene("level");
    }

    void Update()
    {
        // Esc (PC) or back button (Android)
        if (Input.GetKey(KeyCode.Escape))
        {
            OnBackButtonClicl();
        }
    }
}
