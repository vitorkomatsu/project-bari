using System;
using Microsoft.Extensions.Configuration;

namespace Barigui.MicroService
{
    public class Settings
    {
        public int Delay { get; private set; }
        public string ServiceName { get; private set; }

        public Settings(IConfiguration configuration)
        {
            Delay = Convert.ToInt32(configuration["Settings:Delay"]);
            ServiceName = configuration["Settings:ServiceName"];
        }
    }
}
