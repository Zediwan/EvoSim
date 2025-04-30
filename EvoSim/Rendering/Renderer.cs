using System.Windows;
using System.Windows.Media.Imaging;
using EvoSim.ECS.Core;
using EvoSim.ECS.Components;

namespace EvoSim.Rendering;

public class Renderer
{
    private readonly WriteableBitmap _bitmap;
    private readonly uint[] _pixels;
    private readonly int _width;
    private readonly int _height;

    public Renderer(WriteableBitmap bitmap, int width, int height)
    {
        _bitmap = bitmap;
        _width = width;
        _height = height;
        _pixels = new uint[width * height];
    }

    public void Clear()
    {
        for (int i = 0; i < _pixels.Length; i++)
            _pixels[i] = 0xFF000000; // ARGB Black
    }

    public void DrawEntities(EcsEngine ecsEngine)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<PositionComponent>())
        {
            var pos = entity.GetComponent<PositionComponent>();

            if (pos.X < 0 || pos.X >= _width || pos.Y < 0 || pos.Y >= _height) continue;

            _pixels[pos.Y * _width + pos.X] = entity.HasComponent<ColorComponent>() ? entity.GetComponent<ColorComponent>().ARGB : 0xFFFF0000; // Default to red;
        }

        _bitmap.WritePixels(new Int32Rect(0, 0, _width, _height), _pixels, _width * 4, 0);
    }
}