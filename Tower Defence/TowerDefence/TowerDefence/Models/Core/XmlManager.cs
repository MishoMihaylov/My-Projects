using System;
using System.Xml.Serialization;
using System.IO;
using TowerDefence.Interfaces.Core;

namespace TowerDefence.Models.Core
{
    
    public class XmlManager : IXmlManager
    {
        private Type CurrentType { get; set; }

        public XmlManager(Type currentType)
        {
            this.CurrentType = currentType;
        }

        public T Load<T>(string path)
        {
            T instance;

            using(TextReader xmlReader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(CurrentType);
                instance = (T)xml.Deserialize(xmlReader);
            }

            return instance;
        }

        public void Save(string path)
        {
            //Not sure if ill need that for now
            throw new NotImplementedException();
        }
    }
}
