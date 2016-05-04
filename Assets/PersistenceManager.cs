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

    public static void setSelectedMap(int mapLevel)
    {
        PlayerPrefs.SetInt("selected_map", mapLevel);
    }

    public static Difficulty getDifficulty()
    {
        return (Difficulty) PlayerPrefs.GetInt(Constant.DIFFICULTY_PREFS, (int) Difficulty.MEDIUM);
    }

    public static void setDifficulty(Difficulty difficulty)
    {
        PlayerPrefs.SetInt(Constant.DIFFICULTY_PREFS, (int) difficulty);
    }
}
