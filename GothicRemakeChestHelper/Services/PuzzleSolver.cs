using System.Collections.Generic;
using System.Linq;
using GothicRemakeChestHelper.Models;

namespace GothicRemakeChestHelper.Services
{
    public class PuzzleSolver
    {
        public List<SolutionStep> find_solution(int[] start_positions, List<PuzzleRule> rules, int target_position = 4)
        {
            var queue = new Queue<PuzzleState>();
            var visited = new HashSet<string>();

            var initial_state = new PuzzleState
            {
                current_positions = start_positions,
                move_description = "Pozycja startowa",
                parent_state = null
            };

            if (initial_state.current_positions.All(p => p == target_position))
                return build_solution_path(initial_state);

            queue.Enqueue(initial_state);
            visited.Add(initial_state.get_state_hash());

            while (queue.Count > 0)
            {
                var current_state = queue.Dequeue();

                for (int i = 0; i < start_positions.Length; i++)
                {
                    foreach (int direction in new[] { -1, 1 })
                    {
                        var new_state = apply_move(current_state, i, direction, rules);

                        if (new_state != null)
                        {
                            if (new_state.current_positions.All(p => p == target_position))
                                return build_solution_path(new_state);

                            string hash = new_state.get_state_hash();
                            if (!visited.Contains(hash))
                            {
                                visited.Add(hash);
                                queue.Enqueue(new_state);
                            }
                        }
                    }
                }
            }
            return null;
        }

        private PuzzleState apply_move(PuzzleState state, int segment_index, int direction, List<PuzzleRule> rules)
        {
            int[] new_positions = (int[])state.current_positions.Clone();
            int actual_move = direction * -1;
            new_positions[segment_index] += actual_move;

            if (new_positions[segment_index] < 1 || new_positions[segment_index] > 7) return null;

            var rule = rules.FirstOrDefault(r => r.source_segment == segment_index + 1);
            if (rule != null)
            {
                foreach (var effect in rule.effects)
                {
                    int target_index = effect.Key;
                    int effect_direction = effect.Value;
                    int final_movement = effect_direction * actual_move;
                    new_positions[target_index] += final_movement;

                    if (new_positions[target_index] < 1 || new_positions[target_index] > 7) return null;
                }
            }

            string dir_text = direction == -1 ? "w lewo" : "w prawo";
            return new PuzzleState
            {
                current_positions = new_positions,
                move_description = $"Przesuń segment {segment_index + 1} {dir_text}",
                parent_state = state
            };
        }

        private List<SolutionStep> build_solution_path(PuzzleState final_state)
        {
            var states_path = new List<PuzzleState>();
            var current = final_state;

            while (current != null && current.parent_state != null)
            {
                states_path.Insert(0, current);
                current = current.parent_state;
            }

            var grouped_steps = new List<SolutionStep>();

            if (states_path.Count > 0)
            {
                string current_move = states_path[0].move_description;
                int count = 1;
                PuzzleState before_state = states_path[0].parent_state;
                int step_number = 1;

                for (int i = 1; i < states_path.Count; i++)
                {
                    if (states_path[i].move_description == current_move)
                    {
                        count++;
                    }
                    else
                    {
                        grouped_steps.Add(create_step(step_number++, current_move, count, before_state, states_path[i - 1]));
                        current_move = states_path[i].move_description;
                        count = 1;
                        before_state = states_path[i].parent_state;
                    }
                }
                grouped_steps.Add(create_step(step_number, current_move, count, before_state, states_path[states_path.Count - 1]));
            }

            return grouped_steps;
        }

        private SolutionStep create_step(int step_number, string desc, int count, PuzzleState before, PuzzleState after)
        {
            string suffix = count > 1 ? $" x{count}" : "";
            var step = new SolutionStep
            {
                move_description = $"Krok {step_number}: {desc}{suffix}",
                letters = new List<LetterState>()
            };

            for (int i = 0; i < after.current_positions.Length; i++)
            {
                char letter = (char)('A' + after.current_positions[i] - 1);
                bool changed = before.current_positions[i] != after.current_positions[i];
                string comma = (i == after.current_positions.Length - 1) ? "" : ", ";

                step.letters.Add(new LetterState
                {
                    display_text = $"{letter}{comma}",
                    is_changed = changed
                });
            }
            return step;
        }
    }
}