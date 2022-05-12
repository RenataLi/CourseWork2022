using System;
using Newtonsoft.Json;

[Serializable]
public class Setting
{
    public int CompleteLevels;

    [JsonProperty("volume")]
    public float Volume;
    
    [JsonProperty("quality")]
    public int Quality;
}