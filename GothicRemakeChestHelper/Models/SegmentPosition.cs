using System.ComponentModel;

namespace GothicRemakeChestHelper.Models
{
    public class SegmentPosition : INotifyPropertyChanged
    {
        private string _selected_letter = "D";

        public int segment_id { get; set; }
        public string segment_label => $"Segment {segment_id}:";

        public string[] available_letters { get; } = { "A", "B", "C", "D", "E", "F", "G" };

        public string selected_letter
        {
            get => _selected_letter;
            set
            {
                _selected_letter = value;
                on_property_changed(nameof(selected_letter));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_property_changed(string property_name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
    }
}