namespace Services.Exceptions.Commons
{
    public record FieldErrorValidation(string FieldName, ValidationExceptionTypes ValidationException);
}
