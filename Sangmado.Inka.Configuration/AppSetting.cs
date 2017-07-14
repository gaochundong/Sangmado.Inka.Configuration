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
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException("itemName");

            ConfigurationManager.RefreshSection("appSettings");

            if (ConfigurationManager.AppSettings.AllKeys.Contains(itemName))
            {
                return (T)Convert.ChangeType(
                    ConfigurationManager.AppSettings[itemName],
                    typeof(T), CultureInfo.InvariantCulture);
            }

            return default(T);
        }

        public void AddItem<T>(string itemName, T itemValue)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException("itemName");
            if (itemValue == null)
                throw new ArgumentNullException("itemValue");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings.AllKeys.Contains(itemName))
                throw new InvalidOperationException(string.Format("Item [{0}] already exists.", itemName));

            config.AppSettings.Settings.Add(itemName, itemValue.ToString());

            config.Save(ConfigurationSaveMode.Modified);
        }

        public void UpdateItem<T>(string itemName, T itemValue)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException("itemName");
            if (itemValue == null)
                throw new ArgumentNullException("itemValue");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (!config.AppSettings.Settings.AllKeys.Contains(itemName))
                throw new InvalidOperationException(string.Format("Item [{0}] does not exist.", itemName));

            config.AppSettings.Settings[itemName].Value = itemValue.ToString();

            config.Save(ConfigurationSaveMode.Modified);
        }

        public void AddOrUpdateItem<T>(string itemName, T itemValue)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException("itemName");
            if (itemValue == null)
                throw new ArgumentNullException("itemValue");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (!config.AppSettings.Settings.AllKeys.Contains(itemName))
            {
                config.AppSettings.Settings.Add(itemName, itemValue.ToString());
            }
            else
            {
                config.AppSettings.Settings[itemName].Value = itemValue.ToString();
            }

            config.Save(ConfigurationSaveMode.Modified);
        }

        public void AppendItem<T>(string itemName, T appendedItemValue)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException("itemName");
            if (appendedItemValue == null)
                throw new ArgumentNullException("appendedItemValue");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add(itemName, appendedItemValue.ToString());

            config.Save(ConfigurationSaveMode.Modified);
        }

        public void RemoveItem(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException("itemName");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove(itemName);

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
