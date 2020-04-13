using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion
{
    public class DominationService
    {
        public bool IsNewLabelDominated(Label newLabel, ICollection<Label> currentLabels)
        {
            foreach (var currentLabel in currentLabels)
            {
                if (IsLabelDominated(newLabel, currentLabel))
                    return true;
            }
            return false;
        }

        public void RemoveDominatedLabels(Label newLabel, ICollection<Label> currentLabels)
        {
            var toRemoveLabels = new List<Label>();
            foreach (var currentLabel in currentLabels)
            {
                if (IsLabelDominated(currentLabel, newLabel))
                    toRemoveLabels.Add(currentLabel);
            }
            foreach (var toRemoveLabel in toRemoveLabels)
            {
                currentLabels.Remove(toRemoveLabel);
            }
        }

        private bool IsLabelDominated(Label label, Label dominationLabel)
        {
            var newCriteria = label.GetAllCriteria();
            var dominationCriteria = dominationLabel.GetAllCriteria();
            bool atLeastOneCriterionIsDominated = false;
            bool atLeastOneCriterionDominates = false;
            for (int index = 0; index < newCriteria.Count(); index++)
            {
                var criterion = newCriteria[index];
                var dominationCriterion = dominationCriteria[index];
                if (criterion.Value > dominationCriterion.Value)
                    atLeastOneCriterionIsDominated = true;
                if (criterion.Value < dominationCriterion.Value)
                    atLeastOneCriterionDominates = true;
            }
            if (atLeastOneCriterionIsDominated && !atLeastOneCriterionDominates)
                return true;
            return false;
        }

        
    }
}
