using System.Configuration;

namespace SIGCOMT.Common
{
    public class ConfigurationAppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int TimeOutSession()
        {
            var timeOutSession = ConfigurationManager.AppSettings.Get("TimeOutSession");

            if (string.IsNullOrEmpty(timeOutSession))
            {
                return 30;
            }
            return int.Parse(timeOutSession);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string EmailPattern()
        {
            var emailPattern = ConfigurationManager.AppSettings.Get("EmailPattern");
            return emailPattern;
        }
    }
}