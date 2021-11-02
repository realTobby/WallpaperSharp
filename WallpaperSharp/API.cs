using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WallpaperSharp
{
    public class API
    {
        private ConfigModel loadedConfig;

        private string searchTagEndpoint = "search?categories=000&purity=110&resolutions=1920x1080&ratios=16x9&sorting=date_added&order=desc";

        private string baseEndpoint = "https://wallhaven.cc/api/v1/";
        

        public API()
        {
            GetConfiguration();
        }

        private void GetConfiguration()
        {
            loadedConfig = new ConfigModel("cfg_Settings.txt");
        }

        public WallpaperModel SearchTag(string tag)
        {
            string endpointToCall = baseEndpoint + searchTagEndpoint;
            endpointToCall = AuthenticateRequest(endpointToCall);
            endpointToCall = SetPurityLevel(endpointToCall);
            WallpaperModel pM = WallpaperModel.FromJson(GetJsonFromEndpoint(endpointToCall));
            return pM;
        }

        private string AuthenticateRequest(string unauthenticatedRequest)
        {
            string authenticated = unauthenticatedRequest + "&apikey=" + loadedConfig.API_KEY;
            return authenticated;
        }

        private string SetPurityLevel(string request)
        {
            string result = request + "&purity=" + loadedConfig.GetPurityString();
            return result;
        }

        private string GetJsonFromEndpoint(string url)
        {
            string jsonResult = "";
            using (WebClient wc = new WebClient())
            {
                jsonResult = wc.DownloadString(url);
            }
            return jsonResult;
        }

    }
}
