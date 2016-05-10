namespace TowerDefence.Interfaces.Core
{
    public interface IAsset
    {
        string Name { get; set; }

        int X { get; set; }

        int Y { get; set; }

        int Width { get; set; }

        int Height { get; set; }
    }
}
