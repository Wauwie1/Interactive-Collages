using System.Windows;
using System.Windows.Controls;

namespace InteractiveCollages.Views
{
    /// <summary>
    ///     Interaction logic for ShareView.xaml
    /// </summary>
    public partial class ShareView : UserControl
    {
        private readonly MainWindow main;

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