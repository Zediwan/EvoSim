using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using EvoSim.Simulation;
using EvoSim.Rendering;

namespace EvoSim;

public partial class MainWindow : Window
{
    private readonly SimulationEngine _simulation;
    private readonly Renderer _renderer;
    private readonly DispatcherTimer _timer;
    private readonly WriteableBitmap _bitmap;

    private readonly int _width = 800;
    private readonly int _height = 600;
    private readonly float _deltaTime = 0.016f; // ~60 FPS

    public MainWindow()
    {
        InitializeComponent();

        // Initialize bitmap for rendering
        _bitmap = new WriteableBitmap(_width, _height, 96, 96, PixelFormats.Bgra32, null);
        SimulationImage.Source = _bitmap;

        // Initialize simulation logic
        _simulation = new SimulationEngine(_width, _height);
        _simulation.InitializeEntities(10000);

        // Initialize renderer for drawing entities
        _renderer = new Renderer(_bitmap, _width, _height);

        // Setup timer for game loop
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_deltaTime * 1000) };
        _timer.Tick += OnSimulationTick;
        _timer.Start();
    }

    private void OnSimulationTick(object sender, EventArgs e)
    {
        _simulation.Update(_deltaTime);
        _renderer.Clear();
        _renderer.DrawEntities(_simulation.EcsEngine);
    }
}