using System;
using System.Resources;
using System.Globalization;

namespace MyGame
{
    /// <summary>
    /// Enum of supported languages
    /// </summary>
    public enum SupportedLanguage
    {
        English,
        Spanish,
        //French,
        //German,
        //Japanese,
    }

    public class Language
    {
        // Singleton
        public static Language Manager = new Language();
        ResourceManager rm = new ResourceManager(typeof(Language));
        string[] supportedLanguageCultures = new string[] 
        { 
            "en", 
            "es",
            //"fr",
            //"ge",
            //"jp",
        };

        ResourceSet rs;

        Language()
        {
            try
            {
                // Use the culture of the player's environment
                SetCulture(CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                // Resort to the default environment, English
                SetCulture(SupportedLanguage.English);
            }
        }

        /// <summary>
        /// Converts a resource into a translated text
        /// </summary>
        /// <param name="resourceName">Resource to translate</param>
        /// <returns>If a translation is found, it is returned. Otherwise the function returns "(Untranslated)resourceName"</returns>
        public string GetText(string resourceName)
        {
            //return rs.GetString(resourceName) ?? "(Untranslated)" + resourceName;
            return rs.GetString(resourceName);
        }

        /// <summary>
        /// Like GetText, but this method is used for translating objects, such as graphics, for languages.
        /// </summary>
        /// <param name="resourceName">Resource to translate</param>
        /// <returns>Return the translated object</returns>
        public object GetObject(string resourceName)
        {
            return rs.GetObject(resourceName);
        }

        /// <summary>
        /// Assign a culture to the Language Manager
        /// </summary>
        /// <param name="culture">Culture Info</param>
        public void SetCulture(CultureInfo culture)
        {
            rs = rm.GetResourceSet(culture, true, true);
        }

        /// <summary>
        /// Assign a culture to the Language Manager
        /// </summary>
        /// <param name="language">Supported Language</param>
        public void SetCulture(SupportedLanguage language)
        {
            SetCulture(CultureInfo.CurrentCulture);
            //SetCulture(CultureInfo.GetCultureInfo(
            //    supportedLanguageCultures[(int)language]));
        }
    }

    /// <summary>
    /// Helper static class for Method Extensions
    /// </summary>
    public static class LanguageExtender
    {
        /// <summary>
        /// String Extension Method to translate text from a key string.
        /// </summary>
        /// <param name="textKey">An entry of the TextKey resource file</param>
        /// <returns>The translation for the TextKey in the current language</returns>
        public static string Translate(this String textKey)
        {
            return Language.Manager.GetText(textKey);
        }
        public static string Put(this String textKey)
        {
            return Language.Manager.GetText(textKey);
        }

        /// <summary>
        /// String Extension Method to translate an object from a key string.
        /// </summary>
        /// <param name="textKey">An entry of the TextKey resource file</param>
        /// <returns>The translated object for the TextKey in the current language</returns>
        public static object TranslateObject(this String textKey)
        {
            return Language.Manager.GetObject(textKey);
        }

        /// <summary>
        /// SupportedLanguage Extension Method to change languages
        /// </summary>
        /// <param name="language">New Language</param>
        public static void Set(this SupportedLanguage language)
        {
            Language.Manager.SetCulture(language);
        }
    }
}

