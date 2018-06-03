using Rover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rover.Services.Interface;
using Common;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
namespace Rover.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService()
        {
        }

        public string GetWebApiPath()
        {
            var apiEndPoint = Common.ConfigSettingsReader.GetConfigurationValues(Constants.APIEndPoint);
            var RoverNavigationService = Common.ConfigSettingsReader.GetConfigurationValues(Constants.RoverNavigationService);
            return apiEndPoint + RoverNavigationService;
        }
    }
}
