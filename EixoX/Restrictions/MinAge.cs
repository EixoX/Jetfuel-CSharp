using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    public class MinAge : Attribute, Restriction
    {
        private int _Years;
        private string _Message;


        public MinAge(int years, string message)
        {
            this._Years = years;
            this._Message = message;
        }

        public MinAge(int years)
            : this(years, "Not old nough") { }

        public int Years { get { return this._Years; } }


        public bool Validate(object input)
        {
            if (input == null)
                return true;

            DateTime date = Convert.ToDateTime(input);

            if (date == DateTime.MinValue)
                return true;

            return DateTimeHelper.GetAgeInYears(DateTime.Now, date) >= _Years;
        }



        public string RestrictionMessageFormat
        {
            get { return this._Message; }
        }
    }
}
