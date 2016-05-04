using UnityEngine;
using System.Collections;

public static class PersistantManager
{
    public static bool IsTutorialShown()
    {
        return PlayerPrefs.GetInt("show_tutorial") == 1;
    }

    public static void SetTutorialAsShown()
    {
        PlayerPrefs.SetInt("show_tutorial", 1);
    }

    public static int getSelectedMap()
    {
        return PlayerPrefs.GetInt("selected_map", 1);
    }
}
