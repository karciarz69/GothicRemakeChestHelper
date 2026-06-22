using System.Collections.ObjectModel;

namespace GothicRemakeChestHelper.Models
{
    public class RuleRow
    {
        public int source_segment { get; set; }
        public string row_label => $"Segment {source_segment} 🡆";
        public ObservableCollection<RuleCell> cells { get; set; } = new ObservableCollection<RuleCell>();
    }
}