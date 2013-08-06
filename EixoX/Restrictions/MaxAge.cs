using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    public class MaxAge : Attribute, Restriction
    {
        private int _Years;
        private string _Message;


        public MaxAge(int years, string message)
        {
            this._Years = years;
            this._Message = message;
        }

        public MaxAge(int years)
            : this(years, "Too old") { }

        public int Years { get { return this._Years; } }


        public bool Validate(object input)
        {
            if (input == null)
                return true;

            DateTime date = Convert.ToDateTime(input);

            if (date == DateTime.MinValue)
                return true;

            return DateTimeHelper.GetAgeInYears(DateTime.Now, date) <= _Years;

        }

        public string RestrictionMessageFormat
        {
            get { return _Message; }
        }
    }
}
