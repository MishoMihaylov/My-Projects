using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using TowerDefence.Models.Unilities;
using TowerDefence.Interfaces.Core;

namespace TowerDefence.Models.Core
{
    public class AssetsDB 
    {
        [XmlAttribute("UIImagePack")]
        public string UIImagePack { get; set; }
        [XmlElement("Asset")]
        public List<Asset> Assets { get; set; }
        [XmlIgnore]
        public Dictionary<string, Asset> GetElement { get; private set; }

        private static AssetsDB istance;

        private AssetsDB()
        {
            this.GetElement = new Dictionary<string, Asset>();
        }

        [XmlIgnore]
        public static AssetsDB Instance {
            get
            {
                if(istance == null)
                {
                    istance = new AssetsDB();

                    XmlSerializer xmlSerializer = new XmlSerializer(istance.GetType());

                    using (TextReader reader = new StreamReader(Constants.MenuUIXMLPath))
                    {
                        istance = (AssetsDB)xmlSerializer.Deserialize(reader);
                    };
                }

                foreach (var asset in istance.Assets)
                {
                    istance.GetElement.Add(asset.Name, asset);
                }

                return istance;
            }
        }     
    }
}
 