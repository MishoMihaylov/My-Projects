using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TowerDefence.Models.Core
{
    public class Asset
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("x")]
        public int X { get; set; }
        [XmlAttribute("y")]
        public int Y { get; set; }
        [XmlAttribute("width")]
        public int Width { get; set; }
        [XmlAttribute("height")]
        public int Height { get; set; }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
