using System.Text;
using Newtonsoft.Json;

namespace RMIPSS_System.Services;

public class LanguageTranslateService
{
            private readonly HttpClient _httpClient;
            private readonly string apiUrl = "translate"; 

        public LanguageTranslateService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LibreTranslateClient");
        }

        public async Task<string> TranslateTextAsync(string text, string sourceLang, string targetLang)
        {
            
                var requestBody = new
                {
                q = string.IsNullOrEmpty(text) ? "Hello" : text,  // Ensure non-empty text
                source = sourceLang,
                target = "az",
                format = "text",
                alternatives = 3,
                api_key = ""
                };
                
                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                try
                {
                    var response = await _httpClient.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(result);
                    return jsonResponse?.translatedText ?? text;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("HTTP Request Error: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error: " + ex.Message);
                    throw;
                }
            }
        }
    
