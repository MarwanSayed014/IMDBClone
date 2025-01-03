using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Attributes
{
    public class CurrentYearMaxAttribute : ValidationAttribute
    {
        public CurrentYearMaxAttribute()
        {
                
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            int year = (int) value;
            return year <= DateTime.Now.Year;
        }
    }
}
