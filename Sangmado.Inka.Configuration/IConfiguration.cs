using System;

namespace Sangmado.Inka.Configuration
{
    public interface IConfiguration
    {
        T GetItem<T>(string itemName) where T : IConvertible;
        void AddItem<T>(string itemName, T itemValue);
        void UpdateItem<T>(string itemName, T itemValue);
        void AddOrUpdateItem<T>(string itemName, T itemValue);
        void AppendItem<T>(string itemName, T appendedItemValue);
        void RemoveItem(string itemName);
    }
}
