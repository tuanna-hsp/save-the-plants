using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingBehaviour : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle ambientToggle;
    public Toggle vibrationToggle;

    private bool isStarted;

    // Use this for initialization
    void Start()
    {
        musicToggle.isOn = PersistantManager.IsMusicEnabled();
        musicToggle.onValueChanged.AddListener(ToggleMusic);

        ambientToggle.isOn = PersistantManager.IsAmbientEnabled();
        ambientToggle.onValueChanged.AddListener(ToggleAmbient);

        vibrationToggle.isOn = PersistantManager.IsVibrationEnabled();
        vibrationToggle.onValueChanged.AddListener(ToggleVibration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMusic(bool enabled)
    {
        PersistantManager.EnableMusic(enabled);
    }

    public void ToggleAmbient(bool enabled)
    {
        PersistantManager.EnableAmbient(enabled);
    }

    public void ToggleVibration(bool enabled)
    {
        PersistantManager.EnableVibration(enabled);
    }
}
