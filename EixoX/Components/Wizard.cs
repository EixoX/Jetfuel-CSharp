using EixoX.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EixoX.Components
{
    public class Wizard
    {
        private int MinStepIdWithError { get; set; }
        public WizardStep StepWithError { get; set; }

        public List<WizardStep> Steps { get; set; }
        public Dictionary<WizardStep, List<Object>> StepErrors { get; set; }
        
        public Wizard(params WizardStep[] steps)
        {
            this.MinStepIdWithError = Int32.MaxValue;
            this.Steps = new List<WizardStep>();
            this.StepErrors = new Dictionary<WizardStep, List<Object>>();

            for (int i = 0; i < steps.Length; i++)
            {
                this.Steps.Add(steps[i]);
                this.StepErrors.Add(steps[i], new List<object>());
            }
        }

        public void Invalidate(WizardStep step, string fieldName, string message)
        {
            if (!this.Steps.Contains(step))
                throw new ArgumentException("Step " + step + " not found.");

            this.StepErrors[step].Add(new String[] { fieldName, message });

            if (step.StepId < this.MinStepIdWithError)
            {
                this.MinStepIdWithError = step.StepId;
                this.StepWithError = step;
            }
        }

        public bool HasError
        {
            get
            {
                return this.MinStepIdWithError != Int32.MaxValue;
            }
        }
    }
}