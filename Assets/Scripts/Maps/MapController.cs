using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour {

    public void OnMap1ButtonCLick()
    {
        PersistantManager.setSelectedMap(1);
    }

    public void OnMap2ButtonClick()
    {
        PersistantManager.setSelectedMap(2);
    }

    public void OnMap3ButtonClick()
    {
        PersistantManager.setSelectedMap(3);
    }

    public void OnBackButtonClicl()
    {
        SceneManager.LoadScene("menu");
    }

    public void OnContinueButtonClick()
    {
        SceneManager.LoadScene("level");
    }

}
