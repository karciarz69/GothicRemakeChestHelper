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
        }

        private void close_button_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}