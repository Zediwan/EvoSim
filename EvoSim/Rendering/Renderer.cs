using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EvoSim.Rendering;

public class Renderer(WriteableBitmap bitmap, int width, int height)
{
    private readonly uint[] _pixels = new uint[width * height];

    public void Clear()
    {
        for (var i = 0; i < _pixels.Length; i++)
        {
            _pixels[i] = 0xFF000000; // ARGB Black
        }
    }

    public void DrawEntities(EcsEngine ecsEngine)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<PositionComponent>())
        {
            var pos = entity.GetComponent<PositionComponent>();

            if (pos.X < 0 || pos.X >= width || pos.Y < 0 || pos.Y >= height) continue;

            _pixels[pos.Y * width + pos.X] = entity.HasComponent<ColorComponent>() ? entity.GetComponent<ColorComponent>().ARGB : 0xFFFFFFFF; // Default to white
        }

        bitmap.WritePixels(new Int32Rect(0, 0, width, height), _pixels, width * 4, 0);
    }
}