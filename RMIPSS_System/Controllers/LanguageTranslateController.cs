using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RMIPSS_System.Models;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Route("Language")]
[ApiController]
public class LanguageTranslateController(
    MarshalleseTranslateService marshalleseTranslateService)
{
    
        // [HttpPost("TranslatePage")]
        // public async Task<String> TranslatePage(string targetLang = "fr")
        // {
        //     var content = "Welcome to RMIPSS Language Translate!";
        //
        //     var translatedText = await translateService.TranslateTextAsync(content,"en", targetLang);
        //     return translatedText;
        //
        // }
    
    
    [HttpPost("TranslatePage/Marshallese")]
    [HttpPost]
    public async Task<ActionResult> TranslatePage(TranslationRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Text))
        {
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { Error = "Text is required." }),
                ContentType = "application/json"
            };
        }

        var translatedText = await marshalleseTranslateService.TranslateTextAsync(request.Text);
        var jsonResponse = JsonConvert.SerializeObject(new { TranslatedText = translatedText });

        return new ContentResult
        {
            Content = jsonResponse,
            ContentType = "application/json"
        };
    }
    
    public class TranslationRequest
    {
        public string Text { get; set; }
    }
}
