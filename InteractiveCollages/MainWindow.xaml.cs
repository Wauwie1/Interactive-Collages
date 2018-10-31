using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InteractiveCollages.Views;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.MessageBox;

namespace InteractiveCollages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int VideoIndex { get; set; }
        public int minGreen { get; set; }
        public int maxGreen { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            new ViewController(this).GoToView(new StartView(this));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            minGreen = 8;
            maxGreen = 42;

        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Key.F8 == e.Key)
            {
                new CameraSettings(this).Show();
            }
        }
    }
}
