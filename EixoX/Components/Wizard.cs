using EixoX.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EixoX.Components
{
    public class Wizard<T>
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

        public Dictionary<string, Dictionary<string, object>> ValidateStep(int stepId)
        {
            Dictionary<string, Dictionary<string, object>> fieldValidations = new Dictionary<string, Dictionary<string, object>>();
            bool stepValidated = true;
            ClassSchema<T> me = ClassSchema<T>.Instance;
            foreach (AspectMember member in me.GetMembers())
            {
                WizardStep stepAttr = member.GetAttribute<WizardStep>(true);
                if (stepAttr != null)
                {
                    if (stepId == stepAttr.StepId)
                    {
                        fieldValidations.Add(member.Name, new Dictionary<string, object>());
                        
                        bool validation = Restrictions.RestrictionAspect<T>.Instance.Validate(this, member.Name);
                        string message = Restrictions.RestrictionAspect<T>.Instance.GetRestrictionMessage(this, member.Name, 1046);
                        fieldValidations[member.Name].Add("validated", validation);
                        fieldValidations[member.Name].Add("message", message);

                        if (!validation) stepValidated = false;
                    }
                }
            }

            fieldValidations.Add("step", new Dictionary<string, object>());
            fieldValidations["step"].Add("validated", stepValidated);

            return fieldValidations;
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