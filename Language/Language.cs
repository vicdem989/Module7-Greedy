namespace LANGUAGE
{

    using ENGLISH;
    using NORWEGIAN;
    using Greedy;

    public class Language
    {

        public static string currentLanguage = string.Empty;

        public static ApplicationStrings appText = SetLanguage();
        public static List<ApplicationStrings> Languages = new List<ApplicationStrings>();

        public Language()
        {
            Console.Clear();
        }

        public static ApplicationStrings SetDefaultLanguage()
        {
            currentLanguage = "en";
            return LangEN.appTextEN;
        }

        public static ApplicationStrings SetLanguage()
        {
            currentLanguage = LanguageSettings.GetLanguageFromFile();
            if (currentLanguage == "no")
            {
                appText = LangNO.appTextNO;
                return LangNO.appTextNO;
            }
            else if (currentLanguage == "en")
            {
                appText = LangEN.appTextEN;
                return LangEN.appTextEN;
            }
            return LangEN.appTextEN;
        }


    }

    public class ApplicationStrings
    {

        //TicTacToe
        public string? GameOver { get; set; }
        public string? YouGot { get; set; }
        public string? OutOf { get; set; }
        public string? MeaningYouGot { get; set; }
        public string? Of { get; set; }
        public string? PlayAgain { get; set; }
        public string? EitherYesOrNo { get; set; }
        public string? ThanksForPlaying { get; set; }
        public string? Score { get; set; }
        public string? PercentageMaxScore { get; set; }


        //MainMenu

        public string? SmallGame { get; set; }
        public string? LargeGame { get; set; }
        public string? XLGame { get; set; }
        public string? Language { get; set; }
        public string? Back { get; set; }
        public string? NewGame { get; set; }
        public string? Settings { get; set; }
        public string? Quit { get; set; }



        //Langauge 
        public string? WhatLanguage { get; set; } //Do you want english or norwegian
        public string? InvalidInput { get; set; }

    }
}