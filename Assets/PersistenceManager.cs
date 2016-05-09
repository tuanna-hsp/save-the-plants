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

    public static int GetSelectedMap()
    {
        return PlayerPrefs.GetInt("selected_map", 1);
    }

    public static void SetSelectedMap(int mapLevel)
    {
        PlayerPrefs.SetInt("selected_map", mapLevel);
    }

    public static Difficulty GetDifficulty()
    {
        return (Difficulty) PlayerPrefs.GetInt(Constant.DIFFICULTY_PREFS, (int) Difficulty.MEDIUM);
    }

    public static void SetDifficulty(Difficulty difficulty)
    {
        PlayerPrefs.SetInt(Constant.DIFFICULTY_PREFS, (int) difficulty);
    }

    public static void SaveProfile(string profileName)
    {
        PlayerPrefs.SetString("profile", profileName);
    }

    public static string GetProfile()
    {
        return PlayerPrefs.GetString("profile", "Default");
    }

    public static void EnableMusic(bool enabled)
    {
        PlayerPrefs.SetInt("music", enabled ? 1 : 0);
    }

    public static bool IsMusicEnabled()
    {
        int enabled = PlayerPrefs.GetInt("music", 1);
        return enabled == 1 ? true : false;
    }

    public static void EnableAmbient(bool enabled)
    {
        PlayerPrefs.SetInt("ambient", enabled ? 1 : 0);
    }

    public static bool IsAmbientEnabled()
    {
        int enabled = PlayerPrefs.GetInt("ambient", 1);
        return enabled == 1 ? true : false;
    }

    public static void EnableVibration(bool enabled)
    {
        PlayerPrefs.SetInt("vibration", enabled ? 1 : 0);
    }

    public static bool IsVibrationEnabled()
    {
        int enabled = PlayerPrefs.GetInt("vibration", 1);
        return enabled == 1 ? true : false;
    }
}
