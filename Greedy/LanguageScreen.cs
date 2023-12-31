namespace Greedy
{
    using System.Runtime.InteropServices;
    using Utils;
    using LANGUAGE;

    public class LanguageSettings : GameEngine.IScene
    {

        private static string path = "config.txt";
        public LanguageSettings()
        {
            Language.currentLanguage = CheckConfig();
        }

        public static void CreateSettings()
        {
            Output.Write(Language.appText.WhatLanguage, true);
            string input = Console.ReadLine().ToLower();

            while (input != "en" && input != "english" && input != "no" && input != "norwegian")
            {
                Console.Clear();
                Output.Write(Language.appText.InvalidInput, true);
                input = Console.ReadLine().ToLower();
            }

            if (input == "en" || input == "english" || input == "engelsk")
            {
                Language.currentLanguage = "en";
            }
            else if (input == "no" || input == "norwegian" || input == "norsk")
            {
                Language.currentLanguage = "no";

            }
   
            OutputToFIle(ChangeLanguage());
        }


        public static string CheckConfig()
        {
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
                Language.SetDefaultLanguage();
                CreateConfigContent();
            }
            else
            {
                GetLanguageFromFile();
                Language.SetLanguage();
            }
            return Language.currentLanguage;
        }

        private static string ChangeLanguage()
        {
            return ("Language = " + Language.currentLanguage);
        }

        public static void CreateConfigContent()
        {
            OutputToFIle("CONFIG FILE");
            OutputToFIle(ChangeLanguage());
        }

        public static void OutputToFIle([Optional] string text)
        {
            StreamWriter sw = new StreamWriter(path);
            if (text != null)
                sw.Write(text);
            sw.Close();
        }
        public static string GetLanguageFromFile()
        {
            StreamReader sr = new StreamReader(path);
            String line = string.Empty;
            string[] desiredLanguage;
            line = sr.ReadLine() ?? string.Empty;
            if (line.Contains("Language"))
            {
                desiredLanguage = line.Split("=");
                Language.currentLanguage = desiredLanguage[1].Trim();
            }
            sr.Close();
            Console.WriteLine(Language.currentLanguage);
            return Language.currentLanguage;

        }

        #region GameEngine
        public Action<Type, Object[]> OnExitScreen { get; set; }

        public void init()
        {
        }
        public void input() { }
        public void update() { }
        public void draw()
        {
            Console.Clear();
            CreateSettings();
            string outputGraphics = "";
            CheckConfig();
            OnExitScreen(typeof(MenuScreen), new object[] { outputGraphics });
        }

        #endregion

    }
}