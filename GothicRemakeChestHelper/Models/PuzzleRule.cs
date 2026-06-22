using System.Collections.Generic;

namespace GothicRemakeChestHelper.Models
{
    public class PuzzleRule
    {
        public int source_segment { get; set; }

        // Klucz: indeks segmentu docelowego (od 0), Wartość: kierunek ruchu (-1 lewo, 1 prawo)
        public Dictionary<int, int> effects { get; set; } = new Dictionary<int, int>();
    }
}