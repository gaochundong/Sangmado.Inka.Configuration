using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Sangmado.Inka.Configuration
{
    public class AppSetting : IAppSetting
    {
        private static AppSetting _instance = new AppSetting();

        public static AppSetting Singleton()
        {
            return _instance;
        }

        public T GetItem<T>(string itemName) where T : IConvertible
        {
            ConfigurationManager.RefreshSection("appSettings");

            if (ConfigurationManager.AppSettings.AllKeys.Contains(itemName))
            {
                return (T)Convert.ChangeType(
                    ConfigurationManager.AppSettings[itemName],
                    typeof(T), CultureInfo.InvariantCulture);
            }

            return default(T);
        }
    }
}
