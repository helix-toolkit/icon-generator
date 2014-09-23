namespace IconGenerator
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.F12)
                {
                    Grab(this.view, "Icon.png");
                    e.Handled = true;
                }
            };
        }

        private static void Grab(Control control, string path)
        {
            var topLeft = control.PointToScreen(new Point(0, 0));
            var bitmap = ScreenCapture.Capture(
                (int)topLeft.X,
                (int)topLeft.Y,
                (int)control.ActualWidth,
                (int)control.ActualHeight);
            bitmap.Save(path);
        }
    }
}
