using System;

namespace Sangmado.Inka.Configuration
{
    public interface IConfiguration
    {
        T GetItem<T>(string itemName) where T : IConvertible;
        void Add(string itemName, string itemValue);
        void Update(string itemName, string itemValue);
        void Remove(string itemName);
    }
}
