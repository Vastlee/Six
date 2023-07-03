using Newtonsoft.Json;

public class JokeData {
    [JsonProperty("categories")]
    public string[] Categories { get; set; }
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    [JsonProperty("icon_url")]
    public string IconURL { get; set; }
    [JsonProperty("id")]
    public string ID { get; set; }
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
    [JsonProperty("url")]
    public string URL { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
}
