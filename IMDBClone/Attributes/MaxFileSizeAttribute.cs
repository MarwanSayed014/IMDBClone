using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public int _maxFileSize { get; }
        private bool _nullable { get; }

        public MaxFileSizeAttribute(int maxSizeInBytes, bool nullable)
        {
            _maxFileSize = maxSizeInBytes;
            _nullable = nullable;
        }

        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file != null) 
                return file.Length <= _maxFileSize? true : false;
            return _nullable;
        }
    }
}
