using Newtonsoft.Json;

namespace RMIPSS_System.Services;

public class MarshalleseTranslateService
{
    private readonly string apiUrl = "https://api.mymemory.translated.net/get";

    public async Task<string> TranslateTextAsync(string text)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync($"{apiUrl}?q={Uri.EscapeDataString(text)}&langpair=en|mh");
            dynamic jsonResponse = JsonConvert.DeserializeObject(response);
            return jsonResponse?.responseData?.translatedText ?? text;
        }
    }
    
}