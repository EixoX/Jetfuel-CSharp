using EixoX.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIControlArrayChoices : UIControlChoices
    {
        private readonly int[] _collection;

        public UIControlArrayChoices(int[] array)
        {
            this._collection = array;
        }

        public IEnumerable<KeyValuePair<object, object>> GetChoices()
        {
            foreach (var item in _collection)
                yield return new KeyValuePair<object, object>(item, item);
        }
    }
}
