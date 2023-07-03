using Newtonsoft.Json;
class Joke
{
    public static async Task<JokeData> GetNewJokeAsync(string user)
    {
        string URL = $"https://api.chucknorris.io/jokes/random?name={user}&category=dev";

        HttpClient client = new HttpClient();

        try
        {
            var response = await client.GetAsync(URL);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var joke = JsonConvert.DeserializeObject<JokeData>(jsonResponse);
            return joke;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}