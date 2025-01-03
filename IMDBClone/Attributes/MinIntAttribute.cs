using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Attributes
{
    public class MinIntAttribute : ValidationAttribute
    {
        public int _min { get; }

        public MinIntAttribute(int min)
        {
            _min = min;
        }


        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            int num = (int)value;
            return num >= _min;
        }
    }
}
