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
    public abstract class UIPresenter<T, TControl> : IEnumerable<TControl> where TControl : UIPresenterControl
    {
        private readonly List<TControl> _Controls;

        protected abstract TControl CreateControl(
            SingleAnnotationAspectMember<UIControlAttribute> member,
            string label,
            string hint,
            int localeCultureId,
            RestrictionList restrictions,
            InterceptorList interceptors,
            GlobalizationList globalization);

        public UIPresenter(int localeCultureId)
        {
            UIAspect<T> aspect = UIAspect<T>.Instance;
            GlobalizationAspect<T> globalization = GlobalizationAspect<T>.Instance;
            RestrictionAspect<T> restrictions = RestrictionAspect<T>.Instance;
            InterceptorAspect<T> interceptors = InterceptorAspect<T>.Instance;

            this._Controls = new List<TControl>(aspect.Count);

            foreach (SingleAnnotationAspectMember<UIControlAttribute> child in aspect)
            {

                int globalizationOrdinal = globalization.GetOrdinal(child.Name);

                GlobalizationList globalizationList = globalizationOrdinal >= 0 ?
                    globalization[globalizationOrdinal].Terms : null;

                string label = globalizationList == null ? null : globalizationList.GetTerm("label", localeCultureId);
                string hint = globalizationList == null ? null : globalizationList.GetTerm("hint", localeCultureId);

                _Controls.Add(CreateControl(
                    child,
                    string.IsNullOrEmpty(label) ? child.Annotation.DefaultLabel : label,
                    string.IsNullOrEmpty(hint) ? child.Annotation.DefaultHint : hint,
                    localeCultureId,
                    restrictions.GetRestrictionList(child.Name),
                    interceptors.GetInterceptorList(child.Name),
                    globalizationList));
            }

        }

        public int Count { get { return this._Controls.Count; } }

        public TControl this[int ordinal]
        {
            get { return _Controls[ordinal]; }
        }

        public TControl this[string name]
        {
            get { return _Controls[GetOrdinalOrException(name)]; }
        }

        public IEnumerable<TControl> GetMembers(params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
                yield return _Controls[GetOrdinalOrException(names[i])];
        }

        public IEnumerable<TControl> GetGroupMembers(string groupName)
        {
            foreach (TControl child in _Controls)
                if (child.InGroup(groupName))
                    yield return child;
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

        public IEnumerator<TControl> GetEnumerator()
        {
            return _Controls.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Controls.GetEnumerator();
        }
    }
}
