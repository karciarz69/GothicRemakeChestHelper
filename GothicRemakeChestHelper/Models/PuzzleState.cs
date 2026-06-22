namespace GothicRemakeChestHelper.Models
{
    public class PuzzleState
    {
        public int[] current_positions { get; set; }
        public string move_description { get; set; }
        public PuzzleState parent_state { get; set; }

        public string get_state_hash()
        {
            return string.Join(",", current_positions);
        }
    }
}