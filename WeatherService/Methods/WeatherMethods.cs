using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Messages.DataTypes.Database.Weather;

namespace WeatherService.Methods
{
    class WeatherMethods
    {
        //Harjje add methods
        public WeatherReturnObject getWeatherStuff(string loc)
        {
            string locations = getCompanyLocation(loc);

            WeatherReturnObject returnObj = new WeatherReturnObject();
        }

        private string getCompanyLocation(string loc)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync("http://dataservice.accuweather.com/locations/v1/cities/search?apikey=g7mFC2QG1JgcakSVgunj3WoAXXVtLcIV&q=" + loc).Result;

            string rValue = response.Content.ReadAsStringAsync().Result;

            return rValue;
        }
    }
}
