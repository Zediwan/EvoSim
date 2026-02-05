using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using EvoSim.Rendering;
using EvoSim.Simulation;

namespace EvoSim.View;

public partial class MainWindow : Window
{
    private readonly SimulationEngine _simulation;
    private readonly Renderer _renderer;
    private readonly DispatcherTimer _timer;

    private const int _width = 800;
    private const int _height = 600;
    private readonly Stopwatch _stopwatch;

    public MainWindow()
    {
        InitializeComponent();

        // Initialize bitmap for rendering
        var bitmap = new WriteableBitmap(_width, _height, 96, 96, PixelFormats.Bgra32, null);
        SimulationImage.Source = bitmap;

        // Initialize simulation logic
        _simulation = new SimulationEngine(_width, _height);
        _simulation.InitializeEntities(10000);

        // Initialize renderer for drawing entities
        _renderer = new Renderer(bitmap, _width, _height);

        // Setup timer for game loop
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) }; // ~60 FPS
        _timer.Tick += OnSimulationTick;

        // Initialize stopwatch for dynamic deltaTime
        _stopwatch = Stopwatch.StartNew();

        _timer.Start();
    }

    private void OnSimulationTick(object sender, EventArgs e)
    {
        // Calculate dynamic deltaTime
        var deltaTime = (float)_stopwatch.Elapsed.TotalSeconds;
        _stopwatch.Restart();

        // Update simulation and render
        _simulation.Update(deltaTime);
        _renderer.Clear();
        _renderer.DrawEntities(_simulation.EcsEngine);
    }
}
