using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public struct ClassSortTerm : ClassSort
    {
        private readonly Aspect _Aspect;
        private readonly int _Ordinal;
        private readonly SortDirection _Direction;

        public ClassSortTerm(Aspect aspect, int ordinal, SortDirection direction)
        {
            this._Aspect = aspect;
            this._Ordinal = ordinal;
            this._Direction = direction;
        }

        public ClassSortTerm(Aspect aspect, string name, SortDirection direction)
            : this(aspect, aspect.GetOrdinalOrException(name), direction) { }

        public Aspect Aspect
        {
            get { return this._Aspect; }
        }

        public int Ordinal
        {
            get { return this._Ordinal; }
        }

        public SortDirection Direction
        {
            get { return this._Direction; }
        }

        public AspectMember Member
        {
            get { return this._Aspect.GetMember(_Ordinal); }
        }

        private void SortAscending<T>(IList<T> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                IComparable ci = (IComparable)list[i];
                int ni = i;

                for (int j = i + 1; j < count; i++)
                {
                    if (ci.CompareTo(list[j]) > 0)
                        ni = j;
                }

                if (ni > i)
                {
                    T temp = list[i];
                    list[i] = list[ni];
                    list[ni] = temp;
                }
            }
        }

        private void SortDescending<T>(IList<T> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                IComparable ci = (IComparable)list[i];
                int ni = i;

                for (int j = i + 1; j < count; i++)
                {
                    if (ci.CompareTo(list[j]) < 0)
                        ni = j;
                }

                if (ni > i)
                {
                    T temp = list[i];
                    list[i] = list[ni];
                    list[ni] = temp;
                }
            }
        }


        private void SortDescending<T>(LinkedList<T> list)
        {
            for (LinkedListNode<T> i = list.First; i != null; i = i.Next)
            {
                IComparable ci = (IComparable)i.Value;
                LinkedListNode<T> other = i;

                for (LinkedListNode<T> j = i.Next; j != null; j = j.Next)
                {
                    if (ci.CompareTo(j.Value) > 0)
                        other = j;
                }

                if (other != i)
                {
                    T temp = i.Value;
                    i.Value = other.Value;
                    other.Value = temp;
                }
            }

        }

        private void SortAscending<T>(LinkedList<T> list)
        {
            for (LinkedListNode<T> i = list.First; i != null; i = i.Next)
            {
                IComparable ci = (IComparable)i.Value;
                LinkedListNode<T> other = i;

                for (LinkedListNode<T> j = i.Next; j != null; j = j.Next)
                {
                    if (ci.CompareTo(j.Value) < 0)
                        other = j;
                }

                if (other != i)
                {
                    T temp = i.Value;
                    i.Value = other.Value;
                    other.Value = temp;
                }
            }
        }

        public IEnumerable<T> Sort<T>(IEnumerable<T> entities)
        {
            switch (_Direction)
            {
                case SortDirection.Ascending:
                    if (entities is IList<T>)
                    {
                        SortAscending<T>((IList<T>)entities);
                        return entities;
                    }
                    else if (entities is LinkedList<T>)
                    {
                        SortAscending<T>((LinkedList<T>)entities);
                        return entities;
                    }
                    else
                    {
                        List<T> list = new List<T>(entities);
                        SortAscending<T>(list);
                        return list;
                    }
                case SortDirection.Descending:
                    if (entities is IList<T>)
                    {
                        SortDescending<T>((IList<T>)entities);
                        return entities;
                    }
                    else if (entities is LinkedList<T>)
                    {
                        SortDescending<T>((LinkedList<T>)entities);
                        return entities;
                    }
                    else
                    {
                        List<T> list = new List<T>(entities);
                        SortDescending<T>(list);
                        return list;
                    }
                default:
                    throw new NotImplementedException("Unknown sort direction: " + _Direction);
            }
        }
    }
}
