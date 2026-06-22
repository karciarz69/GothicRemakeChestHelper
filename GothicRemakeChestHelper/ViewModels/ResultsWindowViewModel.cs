using System.Collections.Generic;
using System.Collections.ObjectModel;
using GothicRemakeChestHelper.Models;

namespace GothicRemakeChestHelper.ViewModels
{
    public class ResultsWindowViewModel
    {
        public ObservableCollection<SolutionStep> solution_steps { get; set; }

        public ResultsWindowViewModel(List<SolutionStep> steps)
        {
            solution_steps = new ObservableCollection<SolutionStep>(steps);
        }
    }
}