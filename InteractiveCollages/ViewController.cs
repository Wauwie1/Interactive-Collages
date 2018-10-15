namespace InteractiveCollages
{
    public class ViewController
    {
        private MainWindow main { get; set; }
        public ViewController(MainWindow main)
        {
            this.main = main;
        }

        public void GoToView(object view)
        {
            main.DataContext = view;
        }
    }
}