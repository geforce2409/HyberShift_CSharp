using System.Globalization;
using System.Windows.Controls;

namespace HyberShift_CSharp.Domain
{
	public class NotEmptyValidationRule : ValidationRule
	{
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value as string;

            if (!string.IsNullOrEmpty(str))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Field is required");
            }
        }
	}
}