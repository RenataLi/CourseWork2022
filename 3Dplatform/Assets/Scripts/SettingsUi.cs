using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUi : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown _dropdownQuality;
    [SerializeField] private Slider _slider;

    public void ShowSettings(bool showSettings)
    {
        gameObject.GetComponent<Canvas>().enabled = showSettings;
            
        if (!showSettings)
        {
            return;
        }
        
        _dropdownQuality.options = new List<TMP_Dropdown.OptionData>()
        {
            new TMP_Dropdown.OptionData("Very Low"),
            new TMP_Dropdown.OptionData("Low"),
            new TMP_Dropdown.OptionData("Medium"),
            new TMP_Dropdown.OptionData("High"),
            new TMP_Dropdown.OptionData("Very High"),
            new TMP_Dropdown.OptionData("Ultra")
        };
        
        _dropdownQuality.value = SettingsReader.Settings.setting.Quality;
        _slider.value = SettingsReader.Settings.setting.Volume;

    }


    public void ApplyQuality()
    {

        SettingsReader.Settings.setting.Quality = _dropdownQuality.value;
        
        QualitySettings.SetQualityLevel(_dropdownQuality.value);
        SettingsReader.Settings.UpdateSettings();

        Debug.Log(QualitySettings.GetQualityLevel()); 
    }
    
    public void ApplyVolume()
    {

        SettingsReader.Settings.setting.Volume = _slider.value;
        MusicManager.Mu.SetVolume(_slider.value);
        SettingsReader.Settings.UpdateSettings();

    }
}
