using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Attributes
{
    public class MinDateAttribute : ValidationAttribute
    {
        private int _year { get; }
        private int _month { get; }
        private int _day { get; }
        public MinDateAttribute(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            DateOnly date = (DateOnly) value;
            DateOnly minDate = new DateOnly(_year, _month, _day);
            return date >= minDate ? true : false;
        }
    }
}
