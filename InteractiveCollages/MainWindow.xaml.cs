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

namespace InteractiveCollages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new ViewController(this).GoToView(new StartView(this));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


        }

    }
}
