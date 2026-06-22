using System.Collections.Generic;
using System.Windows;
using GothicRemakeChestHelper.Models;
using GothicRemakeChestHelper.ViewModels;

namespace GothicRemakeChestHelper.Views
{
    public partial class ResultsWindow : Window
    {
        private List<SolutionStep> _passed_steps;

        public ResultsWindow(List<SolutionStep> steps)
        {
            InitializeComponent();
            _passed_steps = steps;
            DataContext = new ResultsWindowViewModel(steps);
        }

        private void close_button_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void step_mode_button_click(object sender, RoutedEventArgs e)
        {
            var step_window = new StepByStepWindow(_passed_steps);
            step_window.Owner = this;
            step_window.ShowDialog();
        }
    }
}