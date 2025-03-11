namespace Airbnb.Application;

public class CustomValidationException : Exception
{
    public List<string> Errors { get; }

    public CustomValidationException(List<string> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }
    
    public override string StackTrace => string.Empty; // Скрываем стек-трейс
}