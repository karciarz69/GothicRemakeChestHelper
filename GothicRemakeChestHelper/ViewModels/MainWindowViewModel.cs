using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using GothicRemakeChestHelper.Models;
using GothicRemakeChestHelper.Services;

namespace GothicRemakeChestHelper.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<RuleRow> puzzle_matrix { get; set; }
        public ObservableCollection<int> segment_headers { get; set; }

        private int[] _start_positions;
        public ICommand calculate_command { get; }

        public MainWindowViewModel(int segment_count, int[] start_positions)
        {
            _start_positions = start_positions;
            calculate_command = new RelayCommand(calculate_solution);

            puzzle_matrix = new ObservableCollection<RuleRow>();
            segment_headers = new ObservableCollection<int>();

            for (int i = 1; i <= segment_count; i++)
            {
                segment_headers.Add(i);
            }

            for (int row = 1; row <= segment_count; row++)
            {
                var new_row = new RuleRow { source_segment = row };

                for (int col = 1; col <= segment_count; col++)
                {
                    new_row.cells.Add(new RuleCell
                    {
                        target_segment = col,
                        is_editable = (row != col)
                    });
                }

                puzzle_matrix.Add(new_row);
            }
        }

        private void calculate_solution()
        {
            var rules = new List<PuzzleRule>();

            foreach (var row in puzzle_matrix)
            {
                var rule = new PuzzleRule { source_segment = row.source_segment };
                for (int i = 0; i < row.cells.Count; i++)
                {
                    var cell = row.cells[i];
                    if (cell.current_state == CellDirection.Right)
                        rule.effects.Add(i, 1);
                    else if (cell.current_state == CellDirection.Left)
                        rule.effects.Add(i, -1);
                }
                rules.Add(rule);
            }

            var solver = new PuzzleSolver();
            var results = solver.find_solution(_start_positions, rules);

            // Popraw pajacu te dane wejsciowe
            if (results == null)
            {
                MessageBox.Show(
                    "Nie można znaleźć rozwiązania!\n\nPrzejrzyj swoje ustawienia startowe oraz zaznaczone strzałki. W obecnej konfiguracji ułożenie wszystkich segmentów na pozycji docelowej jest matematycznie niemożliwe (układ się blokuje).\nNa 50% może też sie okazać że tej skrzynki nie da sie otworzyć w tym etapie gry/bez klucza",
                    "Niemożliwa kombinacja",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return; 
            }

            var results_window = new Views.ResultsWindow(results);
            results_window.ShowDialog();
        }
    }
}