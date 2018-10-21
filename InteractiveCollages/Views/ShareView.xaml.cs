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

namespace InteractiveCollages.Views
{
    /// <summary>
    /// Interaction logic for ShareView.xaml
    /// </summary>
    public partial class ShareView : UserControl
    {
        private MainWindow main;
        public ShareView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void ButtonShare_Click(object sender, RoutedEventArgs e)
        {
            main.DataContext = new StartView(main);
        }
    }
}
