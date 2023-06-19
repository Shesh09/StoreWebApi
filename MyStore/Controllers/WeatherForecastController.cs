using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration config; 
        private readonly IOptions<MySettings> appSettings; //store as it is       //var 3
        private readonly MySettings mySettings;  //strip it of IOption interface  //var 3

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IOptions<MySettings> appSettings)
        {
            _logger = logger;
            this.config = config;
            this.appSettings = appSettings;         //var 3
            this.mySettings = appSettings.Value;    //var 3
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherAsync()
        {
            //var openWeatherUrl = config.GetSection("MySettings").GetSection("OpenWeatherMapUrl").Value; ////var 1
            //var apiKey = config.GetSection("MySettings").GetSection("ApiKey").Value;                    ////var 1    

            //var openWeatherUrl = config.GetValue<string>("MySettings:OpenWeatherMapUrl");   //var 2
            //var apiKey = config.GetValue<string>("MySettings:ApiKey");                      //var 2

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{mySettings.OpenWeatherMapUrl}{mySettings.ApiKey}");  //var 3a 
            //HttpResponseMessage response = await client.GetAsync($"{appSettings.Value.OpenWeatherMapUrl}{appSettings.Value.ApiKey}");     //var 3b
            //HttpResponseMessage response = await client.GetAsync($"{openWeatherUrl}{apiKey}");    //var 1 si var 2
            //HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=Iasi&appid=c9b369c7684de4afef9a9a36b842afe0"); ////var 0
            
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            var contentData = string.Empty;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                contentData = response.Content.ReadAsStringAsync().Result;
            }

            return Ok(contentData);
        }





        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
