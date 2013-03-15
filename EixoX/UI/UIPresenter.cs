using EixoX.Globalization;
using EixoX.Interceptors;
using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    /// <summary>
    /// Represents the User Interface presentation object
    /// </summary>
    public class UIPresenter<T> : IEnumerable<UIPresenterControl>
    {
        private readonly List<UIPresenterControl> _Controls;

        public UIPresenter(
            UIControlFactory factory,
            int localeCultureId)
        {

            UIAspect<T> aspect = UIAspect<T>.Instance;
            GlobalizationAspect<T> globalization = GlobalizationAspect<T>.Instance;
            RestrictionAspect<T> restrictions = RestrictionAspect<T>.Instance;
            InterceptorAspect<T> interceptors = InterceptorAspect<T>.Instance;

            this._Controls = new List<UIPresenterControl>(aspect.Count);

            foreach (SingleAnnotationAspectMember<UIControlAttribute> child in aspect)
            {

                int globalizationOrdinal = globalization.GetOrdinal(child.Name);

                GlobalizationList globalizationList = globalizationOrdinal >= 0 ?
                    globalization[globalizationOrdinal].Terms : null;

                string label = globalizationList == null ? null : globalizationList.GetTerm("label", localeCultureId);
                string hint = globalizationList == null ? null : globalizationList.GetTerm("hint", localeCultureId);

                UIPresenterControl control = new UIPresenterControl(
                    child,
                    string.IsNullOrEmpty(label) ? child.Annotation.DefaultLabel : label,
                    string.IsNullOrEmpty(hint) ? child.Annotation.DefaultHint : hint,
                    localeCultureId,
                    restrictions.GetRestrictionList(child.Name),
                    interceptors.GetInterceptorList(child.Name),
                    globalizationList,
                    factory.CreateControlFor(child.Annotation));
            }

        }

        public int Count { get { return this._Controls.Count; } }

        public UIPresenterControl this[int ordinal]
        {
            get { return _Controls[ordinal]; }
        }

        public UIPresenterControl this[string name]
        {
            get { return _Controls[GetOrdinalOrException(name)]; }
        }


        public void Render(object entity, bool validateRestrictions, object options, object output)
        {
            foreach (UIPresenterControl control in this._Controls)
                control.Render(entity, validateRestrictions, options, output);
        }

        public void Render(object entity, bool validateRestrictions, object options, object output, params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
                this[names[i]].Render(entity, validateRestrictions, options, output);
        }

        public int GetOrdinal(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int count = _Controls.Count;
                for (int i = 0; i < count; i++)
                    if (name.Equals(_Controls[i].Name, StringComparison.OrdinalIgnoreCase))
                        return i;

            }
            return -1;
        }

        public int GetOrdinalOrException(string name)
        {
            int ordinal = GetOrdinal(name);
            if (ordinal < 0)
                throw new ArgumentException(name + " not on ui aspect for " + typeof(T).FullName);
            else
                return ordinal;
        }

        public IEnumerator<UIPresenterControl> GetEnumerator()
        {
            return _Controls.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Controls.GetEnumerator();
        }
    }
}
