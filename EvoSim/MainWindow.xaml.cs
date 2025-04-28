using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;

        // Drawing
        private WriteableBitmap _bitmap;
        private int _width = 800;
        private int _height = 600;
        private int _dpi = 96;
        private uint[] _pixels; // Class variable for pixel data

        private EcsEngine _ecsEngine;

        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            _bitmap = new WriteableBitmap(_width, _height, _dpi, _dpi, PixelFormats.Bgra32, null);
            SimulationImage.Source = _bitmap;
            _pixels = new uint[_width * _height]; // Initialize the pixel array
            
            _ecsEngine = new EcsEngine();
            _ecsEngine.AddSystem(new EnergySystem());
            _ecsEngine.AddSystem(new HealthSystem());
            _ecsEngine.AddSystem(new MovementSystem(_width, _height));

            // Create an entity with Health and Energy components
            for (var i = 0; i < 10000; i++)
            {
                SpawnEntity();
            }

            // Set up a timer to periodically call the Update method
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };
            _timer.Tick += (sender, args) =>
            {
                float deltaTime = 0.016f; // ~60 FPS
                _ecsEngine.Update(deltaTime);

                ClearBitmap();
                DrawEntities(_ecsEngine);
            };

            _timer.Start();
        }

        private void SpawnEntity()
        {
            var entity = _ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent()
            {
                Health = 100,
                MaxHealth = 100
            });
            entity.AddComponent(new EnergyComponent()
            {
                Energy = 100,
                MaxEnergy = 100
            });
            entity.AddComponent(new PositionComponent()
            {
                X = random.Next(_width),
                Y = random.Next(_height)
            });
            entity.AddComponent(new MovementComponent()
            {
                DX = random.Next(-1, 2),
                DY = random.Next(-1, 2)
            });
        }

        private void ClearBitmap()
        {
            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = 0xFF000000; // ARGB Black
            }

            Int32Rect rect = new Int32Rect(0, 0, _width, _height);
            _bitmap.WritePixels(rect, _pixels, _width * 4, 0);
        }

        private void DrawEntities(EcsEngine ecsEngine)
        {
            foreach (var entity in ecsEngine.GetEntitiesWith<PositionComponent>())
            {
                var pos = entity.GetComponent<PositionComponent>();

                if (pos.X >= 0 && pos.X < _width && pos.Y >= 0 && pos.Y < _height)
                {
                    int index = pos.Y * _width + pos.X;
                    _pixels[index] = 0xFFFF0000; // ARGB Red
                }
            }

            Int32Rect rect = new Int32Rect(0, 0, _width, _height);
            _bitmap.WritePixels(rect, _pixels, _width * 4, 0);
        }
    }
}