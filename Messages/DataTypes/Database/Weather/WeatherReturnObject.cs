using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.DataTypes.Database.Weather
{
    [Serializable]
    public class WeatherReturnObject
    {
        public string weatherText { get; set; }
        public double temperature { get; set; }
        public double realFeelTemperature { get; set; }
        public int weatherIcon { get; set; }
    }
}
