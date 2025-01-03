using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Attributes
{
    public class NowMaxDateAttribute : ValidationAttribute
    {
        public NowMaxDateAttribute()
        {
                
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            DateOnly date = (DateOnly)value;
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            return date <= now ? true : false;
        }
    }
}
