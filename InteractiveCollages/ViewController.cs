namespace InteractiveCollages
{
    public class ViewController
    {
        public ViewController(MainWindow main)
        {
            this.main = main;
        }

        private MainWindow main { get; }

        public void GoToView(object view)
        {
            main.DataContext = view;
        }
    }
}