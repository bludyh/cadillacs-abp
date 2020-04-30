using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Infrastructure.Common.Attributes
{
    public class CompositeKeyAttribute : ValidationAttribute
    {

        public CompositeKeyAttribute(params string[] additionalFields) {
            AdditionalFields = additionalFields;
        }

        public string[] AdditionalFields { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var keyValues = new List<object>();
            var memberNames = new List<string> { validationContext.MemberName };
            memberNames.AddRange(AdditionalFields);

            foreach (var name in memberNames)
                keyValues.Add(validationContext.ObjectType.GetProperty(name).GetValue(validationContext.ObjectInstance));

            if (keyValues.All(kv => kv == null)
                || keyValues.All(kv => kv != null))
                return ValidationResult.Success;

            return new ValidationResult("The key is incomplete. One or more fields have null values.", memberNames);
        }

    }
}
