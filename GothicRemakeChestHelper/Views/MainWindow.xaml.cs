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
        }

    }
}