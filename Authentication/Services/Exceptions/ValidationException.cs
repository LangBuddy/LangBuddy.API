using Services.Exceptions.Commons;

namespace Services.Exceptions
{
    public class ValidationException: Exception
    {
        public List<FieldErrorValidation> FieldErrorValidation { get; set; } = new List<FieldErrorValidation>();
        public ValidationException(string message, List<FieldErrorValidation> fieldErrorValidation) : base(message) 
        {
            FieldErrorValidation = fieldErrorValidation;
        }
    }
}
