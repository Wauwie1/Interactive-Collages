using System.Windows;
using System.Windows.Controls;

namespace InteractiveCollages.Views
{
    /// <summary>
    ///     Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        private readonly MainWindow main;

        public StartView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Button_start_Click(object sender, RoutedEventArgs e)
        {
            new ViewController(main).GoToView(new PhotoView(main));
        }
    }
}