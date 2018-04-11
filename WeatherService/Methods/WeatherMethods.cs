using System.Linq;
using System.Net.Http;
using Messages;
using Messages.DataTypes.Database.Weather;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherService.Methods
{
    class WeatherMethods
    {
        //Harjje add methods
        public static WeatherReturnObject getWeatherStuff(string loc)
        {
            WeatherReturnObject returnObj = new WeatherReturnObject();
            string locations = getCompanyLocation(loc);

            if(locations.Equals("[]"))
            {
                returnObj.weatherText = "Not found";
                return returnObj;
            }

            JArray arr = JArray.Parse(locations);
            JObject obj = arr.Children<JObject>().ElementAt(0);
            string locationKey = (string)obj.SelectToken("Key");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://dataservice.accuweather.com/currentconditions/v1/" + locationKey + "?apikey=GBVmAs0AxohAnsyVmdCzO7GKw3cPfgCd&details=true").Result;
            string result = response.Content.ReadAsStringAsync().Result;

            if (result.Equals("[]"))
            {
                returnObj.weatherText = "Not found";
                return returnObj;
            }

            arr = JArray.Parse(result);
            obj = arr.Children<JObject>().ElementAt(0);

            returnObj.weatherText = (string)obj["WeatherText"];
            returnObj.weatherIcon = (int)obj["WeatherIcon"];
            returnObj.temperature = (double)obj["Temperature"]["Metric"]["Value"];
            returnObj.realFeelTemperature = (double)obj["RealFeelTemperature"]["Metric"]["Value"];

            return returnObj;
        }

        private static string getCompanyLocation(string loc)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://dataservice.accuweather.com/locations/v1/cities/search?apikey=GBVmAs0AxohAnsyVmdCzO7GKw3cPfgCd&q=" + loc).Result;
            string rValue = response.Content.ReadAsStringAsync().Result;
            return rValue;
        }
    }
}
