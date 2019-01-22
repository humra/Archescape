using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour {

    [SerializeField]
    private Slider soundtrackVolumeSlider;
    [SerializeField]
    private Slider environmentalVolumeSlider;

    public IUIHandler uiHandler;
    public GameObject settingsUI;

    private void Start()
    {
        soundtrackVolumeSlider = GameObject.FindGameObjectWithTag(UITagRepository.soundtrackVolumeSlider).GetComponent<Slider>();
        environmentalVolumeSlider = GameObject.FindGameObjectWithTag(UITagRepository.environmentalVolumeSlider).GetComponent<Slider>();
        settingsUI = GameObject.FindGameObjectWithTag(UITagRepository.settingsPanel);

        settingsUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(UIKeybindRepository.pauseMenu))
        {
            settingsUI.SetActive(!settingsUI.activeSelf);
        }
    }

    public void SetSoundtrackVolume(float newValue)
    {
        uiHandler.SetSoundtrackVolume(newValue);
    }

    public void SetEnvironmentalVolume(float newValue)
    {
        uiHandler.SetEnvironmentalVolume(newValue);
    }
}
