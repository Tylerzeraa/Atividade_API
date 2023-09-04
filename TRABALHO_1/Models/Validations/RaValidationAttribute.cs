using System.ComponentModel.DataAnnotations;

namespace TRABALHO_1.Models.Validations
{
    public class RaValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Console.WriteLine("Validando " + value);
            if (value == null || !value.ToString().StartsWith("RA") || value.ToString().Length != 8)
            {
                Console.WriteLine(" nao é valido " + value);
                return new ValidationResult("Escreva um RA válido");
            }

            for (int i = 2; i < 8; i++)
            {
                char digit = value.ToString()[i];
                if (!Char.IsDigit(digit))
                {
                    return new ValidationResult("Escreva um RA válido");
                }
            }

            return ValidationResult.Success;
        }
    }
}
