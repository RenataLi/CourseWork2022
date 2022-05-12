
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SettingsReader : MonoBehaviour
{
    public TextAsset settingJson;
    public Setting setting;

    public static SettingsReader Settings;
    private void Awake()
    {
        if (Settings == null)
        {
            Settings = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void InitSettings()
    {
        Debug.Log("Init");
        string path = Application.dataPath + "/Settings.json";
        using (StreamReader reader = new StreamReader(path))
        {
            var content = reader.ReadToEnd();
            setting = JsonUtility.FromJson<Setting>(content);
        }
        QualitySettings.SetQualityLevel(setting.Quality);
        MusicManager.Mu.SetVolume(setting.Volume);
    }

    public void UpdateSettings()
    {
        string content = JsonUtility.ToJson(setting);

        using (var sw = new StreamWriter(Application.dataPath + "/Settings.json"))
        {
            sw.Write(content);
        }
    }


}
