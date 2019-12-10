using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperSharp
{
    public class ConfigModel
    {
        private Dictionary<string, string> rawData = new Dictionary<string, string>();

        public string API_KEY { get; set; }
        public string LOGIN_NAME { get; set; }
        public bool NSFW { get; set; } = false;

        public ConfigModel(string path)
        {

            ReadConfig(path);

        }

        public string GetPurityString()
        {
            if(NSFW == true)
            {
                return "001";
            }
            return "100";
        }

        private void SaveConfig()
        {

        }

        private void ReadConfig(string path)
        {
            if(System.IO.File.Exists(path))
            {
                string[] data = System.IO.File.ReadAllLines(path);
                foreach (string line in data)
                {
                    if (!line.StartsWith("#"))
                    {
                        string[] rawDataLine = line.Split('=');
                        rawData.Add(rawDataLine[0], rawDataLine[1]);
                    }

                }
            }
            else
            {
                string[] initFile = new string[5];
                initFile[0] = "# THIS IS THE CONFIGURATION FILE FOR THE Wallpaper Downloader";
                initFile[1] = "# FILL OUT THE BLANK FIELDS WITH THE CORRECT VALUES";
                initFile[2] = "API_KEY=";
                initFile[3] = "LOGIN_NAME=";
                initFile[4] = "PURITY_LEVEL=SFW";
                System.IO.File.WriteAllLines(path, initFile);
            }

            API_KEY = rawData["API_KEY"];
            LOGIN_NAME = rawData["LOGIN_NAME"];

            if(rawData["PURITY_LEVEL"] == "NSFW")
            {
                NSFW = true;
            }
            if(rawData["PURITY_LEVEL"] == "SFW")
            {
                NSFW = false;
            }

            
        }

    }
}
