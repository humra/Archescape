﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour {

    [SerializeField]
    private Slider soundtrackVolumeSlider;
    [SerializeField]
    private Slider environmentalVolumeSlider;
    [SerializeField]
    private Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    public IUIHandler uiHandler;
    public GameObject settingsUI;

    private void Start()
    {
        soundtrackVolumeSlider = GameObject.FindGameObjectWithTag(UITagRepository.soundtrackVolumeSlider).GetComponent<Slider>();
        environmentalVolumeSlider = GameObject.FindGameObjectWithTag(UITagRepository.environmentalVolumeSlider).GetComponent<Slider>();
        resolutionDropdown = GameObject.FindGameObjectWithTag(UITagRepository.resolutionDropdown).GetComponent<Dropdown>();

        FillResolutionDropdownOptions();

        settingsUI = GameObject.FindGameObjectWithTag(UITagRepository.settingsPanel);

        settingsUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(UIKeybindRepository.pauseMenu))
        {
            settingsUI.SetActive(!settingsUI.activeSelf);

            if(settingsUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
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

    public void SetScreenResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, true);
    }

    private void FillResolutionDropdownOptions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            if (CheckUniqueResolutionString(option, options))
            {
                options.Add(option);
            }
        }

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].height == Screen.height && resolutions[i].width == Screen.width)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();
    }

    private bool CheckUniqueResolutionString(string testString, List<string> existingStrings)
    {
        foreach (string existingString in existingStrings)
        {
            if (existingString.Equals(testString))
            {
                return false;
            }
        }

        return true;
    }
}
