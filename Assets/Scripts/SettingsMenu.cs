using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeSliderText;
    [SerializeField] private TextMeshProUGUI sensitivitySliderText;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Toggle fullScreenBtn;
    private const int sensitivityMultiplier = 4;


    void Awake()
    {
        // the settings menu is initally deactivated
        gameObject.SetActive(false);

        // for sensitivity
        float sliderValue = PlayerPrefs.GetFloat("Sensitivity");
        ChangeSensitivity(sliderValue);
        sensitivitySliderText.text = sliderValue.ToString("0");
        sensitivitySlider.value = sliderValue;

        // for volume
        float vSliderValue = PlayerPrefs.GetFloat("Volume");
        ChangeVolume(vSliderValue);
        volumeSliderText.text = f(vSliderValue).ToString("0");
        volumeSlider.value = vSliderValue;

        fullScreenBtn.isOn = PlayerPrefs.GetInt("ToggleOn") == 1;
        Screen.fullScreen = fullScreenBtn.isOn;
    }

    private void Update()
    {
        SetSensitivitySlider();
        SetVolumeSlider();
        SetToggle();
    }

    /*         VOLUME SLIDER LOGIC         */
    public void SetVolumeSlider()
    {
        float sliderValue = volumeSlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Volume", sliderValue);
        ChangeVolume(sliderValue);

        volumeSliderText.text = f(sliderValue).ToString("0");
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }

    public void ChangeVolumeSliderText(float num)
    {
        volumeSliderText.text = (num).ToString("0");
    }

    // Maps a number between -80 and 0 to the range 0-100.
    private float f(float x) { return (5.0f/4.0f) * x + 100.0f; }
    /***************************************/


    /*       MOUSE SENSITIVITY SLIDER LOGIC      */

    // Very similar to the volume slider
    public void SetSensitivitySlider()
    {
        float sliderValue = sensitivitySlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Sensitivity", sliderValue);
        ChangeSensitivity(sliderValue);

        sensitivitySliderText.text = sliderValue.ToString("0");
    }

    public void ChangeSensitivity(float sensitivity)
    {
        // Debug.Log($"Time is {Time.time} and Sens is {sensitivityMultiplier * sensitivity}");

        PlayerCam.SetMouseSensitivity(sensitivityMultiplier * sensitivity);
    }

    public void ChangeSensitivitySliderText(float num)
    {
        sensitivitySliderText.text = num.ToString("0");
    }

    /*********************************************/


    public void SetFullscreen(bool IsToggleOn)
    {
        int n = IsToggleOn ? 1 : 0;
        PlayerPrefs.SetInt("ToggleOn", n);
    }

    public void SetToggle()
    {
        Screen.fullScreen = fullScreenBtn.isOn;
        PlayerPrefs.SetInt("ToggleOn", fullScreenBtn.isOn ? 1 : 0);
    }
}
