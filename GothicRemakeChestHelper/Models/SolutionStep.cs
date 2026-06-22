using System.Collections.Generic;

namespace GothicRemakeChestHelper.Models
{
    public class SolutionStep
    {
        public string move_description { get; set; }
        public List<LetterState> letters { get; set; }
    }

    public class LetterState
    {
        public string display_text { get; set; }
        public bool is_changed { get; set; }
    }
}