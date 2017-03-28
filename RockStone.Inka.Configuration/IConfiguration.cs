using System;

namespace RockStone.Inka.Configuration
{
    public interface IConfiguration
    {
        T GetItem<T>(string itemName) where T : IConvertible;
    }
}
