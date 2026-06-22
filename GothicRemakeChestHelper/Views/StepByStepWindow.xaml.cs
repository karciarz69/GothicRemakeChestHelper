using System.Collections.Generic;
using System.Windows;
using GothicRemakeChestHelper.Models;
using GothicRemakeChestHelper.ViewModels;

namespace GothicRemakeChestHelper.Views
{
    public partial class StepByStepWindow : Window
    {
        public StepByStepWindow(List<SolutionStep> steps)
        {
            InitializeComponent();
            DataContext = new StepByStepWindowViewModel(steps);
            try
            {
                this.Icon = System.Windows.Media.Imaging.BitmapFrame.Create(
                    new System.Uri("pack://application:,,,/ikona.ico", System.UriKind.Absolute));
            }
            catch { }
        }

        private void close_button_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}