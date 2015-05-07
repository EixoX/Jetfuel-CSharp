using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Components
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class WizardStep : Attribute
    {
        public int StepId { get; set; }
        public string StepName { get; set; }

        public WizardStep(int stepId, string stepName)
        {
            this.StepId = stepId;
            this.StepName = stepName;
        }

        public override bool Equals(object obj)
        {
            WizardStep otherStep = (WizardStep)obj;
            return this.StepId == otherStep.StepId && this.StepName.Equals(otherStep.StepName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
