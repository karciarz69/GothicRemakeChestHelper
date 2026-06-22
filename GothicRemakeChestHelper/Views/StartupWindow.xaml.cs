using System.Windows;
using GothicRemakeChestHelper.ViewModels;

namespace GothicRemakeChestHelper.Views
{
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
            DataContext = new StartupWindowViewModel();
            try
            {
                this.Icon = System.Windows.Media.Imaging.BitmapFrame.Create(
                    new System.Uri("pack://application:,,,/ikona.ico", System.UriKind.Absolute));
            }
            catch { }
        }

        private void next_button_click(object sender, RoutedEventArgs e)
        {
            var current_vm = (StartupWindowViewModel)DataContext;

            // Konwersja liter na liczby 1-7
            int[] start_pos_array = current_vm.starting_positions
                .Select(p => Array.IndexOf(p.available_letters, p.selected_letter) + 1)
                .ToArray();

            var main_win = new MainWindow(current_vm.segment_count, start_pos_array);
            main_win.Show();
            this.Close();
        }
    }
}