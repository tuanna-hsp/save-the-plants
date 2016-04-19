using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

    public void OnMap1ButtonCLick()
    {
        Constant.map = 1;
    }

    public void OnMap2ButtonClick()
    {
        Constant.map = 2;
    }

    public void OnMap3ButtonClick()
    {
        Constant.map = 3;
    }

    public void OnBackButtonClicl()
    {
        Application.LoadLevel("menu");
    }

    public void OnContinueButtonClick()
    {
        Application.LoadLevel("level");
    }

}
