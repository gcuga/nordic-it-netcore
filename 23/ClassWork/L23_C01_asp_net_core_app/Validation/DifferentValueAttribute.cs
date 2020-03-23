using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace L23_C01_asp_net_core_app.Validation
{
    //https://github.com/microsoft/referencesource/tree/master/System.ComponentModel.DataAnnotations

    public class DifferentValueAttribute : ValidationAttribute
    {
        public string OtherProperty { get; private set; }

        public DifferentValueAttribute(string otherProperty)
        {
            OtherProperty = otherProperty ?? throw new ArgumentNullException(nameof(otherProperty));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Property: {validationContext.MemberName} not found");
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (Equals(value, otherPropertyValue))
            {
                return new ValidationResult($"{validationContext.MemberName} shouldn't be the same as {otherPropertyInfo.Name}");
            }

            return ValidationResult.Success;
        }
    }
}
