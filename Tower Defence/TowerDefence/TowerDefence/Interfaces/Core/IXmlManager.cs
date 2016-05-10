namespace TowerDefence.Interfaces.Core
{
    public interface IXmlManager
    {
        T Load(string path);

        void Save(string path);
    }
}
