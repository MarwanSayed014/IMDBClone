using IMDBClone.Helpers;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private string[] _validTypes { get; }
        private bool _nullable { get; }

        public AllowedExtensionsAttribute(string[] validTypes, bool nullable)
        {
            _validTypes = validTypes;
            _nullable = nullable;
        }

        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file != null) 
            {
                return ServerFile.CheckFileExtension(file ,_validTypes);
            }
            return _nullable;
        }

    }
}
