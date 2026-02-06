using EvoSim.Rendering;
using EvoSim.Simulation;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EvoSim.View;

public partial class MainWindow : Window
{
    private readonly SimulationEngine _simulation;
    private readonly DispatcherTimer _simulationTimer;
    private readonly Stopwatch _stopwatch;

    private readonly Renderer _renderer;
    private readonly DispatcherTimer _renderTimer;

    private const int _width = 800;
    private const int _height = 600;
    
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
        _simulationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(32) };
        _simulationTimer.Tick += OnSimulationTick;

        _renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(32) }; // ~30 FPS
        _renderTimer.Tick += OnRenderTick;

        // Initialize stopwatch for dynamic deltaTime
        _stopwatch = Stopwatch.StartNew();

        _simulationTimer.Start();
        _renderTimer.Start();
    }

    private void OnSimulationTick(object sender, EventArgs e)
    {
        // Calculate dynamic deltaTime
        var deltaTime = (float)_stopwatch.Elapsed.TotalSeconds;
        _stopwatch.Restart();

        _simulation.Update(deltaTime);
    }

    private void OnRenderTick(object sender, EventArgs e)
    {
        _renderer.Clear();
        _renderer.DrawEntities(_simulation.EcsEngine);
    }
}
