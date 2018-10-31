using System.Windows;
using System.Windows.Input;
using InteractiveCollages.Views;
using Application = System.Windows.Forms.Application;

namespace InteractiveCollages
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new StartView(this);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            minGreen = 8;
            maxGreen = 42;
        }

        public int VideoIndex { get; set; }
        public int minGreen { get; set; }
        public int maxGreen { get; set; }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.F8 == e.Key) new CameraSettings(this).Show();
        }
    }
}