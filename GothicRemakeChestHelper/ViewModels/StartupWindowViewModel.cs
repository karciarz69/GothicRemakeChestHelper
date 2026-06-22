using System.Collections.ObjectModel;
using System.ComponentModel;
using GothicRemakeChestHelper.Models;

namespace GothicRemakeChestHelper.ViewModels
{
    public class StartupWindowViewModel : INotifyPropertyChanged
    {
        private int _segment_count = 7;

        public int[] available_counts { get; } = { 4, 5, 6, 7 };

        public string[] position_legend { get; } = { "A", "B", "C", "D", "E", "F", "G" };

        public ObservableCollection<SegmentPosition> starting_positions { get; set; }

        public int segment_count
        {
            get => _segment_count;
            set
            {
                _segment_count = value;
                on_property_changed(nameof(segment_count));
                update_segment_list();
            }
        }

        public StartupWindowViewModel()
        {
            starting_positions = new ObservableCollection<SegmentPosition>();
            update_segment_list();
        }

        private void update_segment_list()
        {
            starting_positions.Clear();
            for (int i = 1; i <= segment_count; i++)
            {
                starting_positions.Add(new SegmentPosition { segment_id = i });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_property_changed(string property_name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
    }
}