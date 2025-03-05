using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Okkam.Cars.WebApi.Validators;

/// <summary>
/// Проверяет что url находится в зоне ".ru"
/// </summary>
public class RuUrlAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        if (value is not string url) return ValidationResult.Success;
        return Regex.IsMatch(url, "(http\\://|https\\://)?.+?\\.ru(\\:\\d{1,5})?(/.+)?")
            ? ValidationResult.Success!
            : new ValidationResult("Domain zone must be \".ru\"");
    }
}