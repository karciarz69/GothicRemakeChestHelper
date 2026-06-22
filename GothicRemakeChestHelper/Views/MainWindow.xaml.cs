using System.Windows;
using GothicRemakeChestHelper.ViewModels;

namespace GothicRemakeChestHelper.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(int segment_count, int[] start_positions)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(segment_count, start_positions);
            try
            {
                this.Icon = System.Windows.Media.Imaging.BitmapFrame.Create(
                    new System.Uri("pack://application:,,,/ikona.ico", System.UriKind.Absolute));
            }
            catch { }
        }

    }
}