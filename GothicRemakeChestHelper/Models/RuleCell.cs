using System.ComponentModel;
using System.Windows.Input;
using GothicRemakeChestHelper.ViewModels;

namespace GothicRemakeChestHelper.Models
{
    public enum CellDirection { Empty, Left, Right }

    public class RuleCell : INotifyPropertyChanged
    {
        private CellDirection _current_state = CellDirection.Empty;
        private bool _is_editable = true; 

        public int target_segment { get; set; }

        public bool is_editable
        {
            get => _is_editable;
            set
            {
                _is_editable = value;
                on_property_changed(nameof(is_editable));
            }
        }

        public CellDirection current_state
        {
            get => _current_state;
            set
            {
                if (!is_editable) return;
                _current_state = value;
                on_property_changed(nameof(current_state));
                on_property_changed(nameof(display_icon));
            }
        }

        public string display_icon
        {
            get
            {
                if (current_state == CellDirection.Left) return "🡄";
                if (current_state == CellDirection.Right) return "🡆";
                return " ";
            }
        }

        public ICommand left_click_command => new RelayCommand(() =>
        {
            if (!is_editable) return; 
            if (current_state == CellDirection.Right) current_state = CellDirection.Empty;
            else current_state = CellDirection.Right;
        });

        public ICommand right_click_command => new RelayCommand(() =>
        {
            if (!is_editable) return; 
            if (current_state == CellDirection.Left) current_state = CellDirection.Empty;
            else current_state = CellDirection.Left;
        });

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_property_changed(string property_name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
    }
}