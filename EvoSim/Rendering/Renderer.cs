using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EvoSim.Rendering;

public class Renderer
{
    private readonly WriteableBitmap _bitmap;
    private readonly uint[] _pixels;
    private const uint BackgroundColor = 0xFF000000;
    private const uint DefaultEntityColor = 0xFFFFFFFF;

    public Renderer(WriteableBitmap bitmap)
    {
        _bitmap = bitmap;
        _pixels = new uint[_bitmap.PixelWidth * _bitmap.PixelHeight];
    }

    public void Clear()
    {
        for (var i = 0; i < _pixels.Length; i++)
        {
            _pixels[i] = BackgroundColor;
        }
    }

    public void DrawEntities(EcsEngine ecsEngine)
    {
        var width = _bitmap.PixelWidth;
        var height = _bitmap.PixelHeight;
        foreach (var entity in ecsEngine.GetEntitiesWith<PositionComponent>())
        {
            var pos = entity.GetComponent<PositionComponent>();
            if (pos.X < 0 || pos.X >= width || pos.Y < 0 || pos.Y >= height) continue;

            _pixels[pos.Y * width + pos.X] = entity.HasComponent<ColorComponent>() ? entity.GetComponent<ColorComponent>().ARGB : DefaultEntityColor;
        }

        _bitmap.WritePixels(new Int32Rect(0, 0, width, height), _pixels, width * 4, 0);
    }
}