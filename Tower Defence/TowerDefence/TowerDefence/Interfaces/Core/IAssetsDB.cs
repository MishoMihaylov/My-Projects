using System.Collections.Generic;
using TowerDefence.Models.Core;

namespace TowerDefence.Interfaces.Core
{
    public interface IAssetsDB
    {
        string UIImagePack { get; set; }

        List<IAsset> Assets { get; set; }

        Dictionary<string, Asset> GetElement { get; set; }
    }
}
