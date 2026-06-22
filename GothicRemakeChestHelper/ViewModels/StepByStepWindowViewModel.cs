using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using GothicRemakeChestHelper.Models;

namespace GothicRemakeChestHelper.ViewModels
{
    public class StepByStepWindowViewModel : INotifyPropertyChanged
    {
        private List<SolutionStep> _steps;
        private int _current_index = 0;

        public SolutionStep current_step => _steps[_current_index];
        public string progress_text => $"Krok {_current_index + 1} z {_steps.Count}";

        public ICommand next_step_command { get; }

        public StepByStepWindowViewModel(List<SolutionStep> steps)
        {
            _steps = steps;
            next_step_command = new RelayCommand(next_step);
        }

        private void next_step()
        {
            if (_current_index < _steps.Count - 1)
            {
                _current_index++;
                on_property_changed(nameof(current_step));
                on_property_changed(nameof(progress_text));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_property_changed(string property_name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
    }
}