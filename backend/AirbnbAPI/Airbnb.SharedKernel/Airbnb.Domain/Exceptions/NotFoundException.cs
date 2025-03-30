namespace Airbnb.SharedKernel.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, string keyName, object key) : base(
        $"Entity \"{name}\" with key: {keyName} = ({key}) was not found.")
    {
    }
}