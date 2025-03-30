namespace Airbnb.CassandraRepository.Exceptions;

public class CassandraDbException : Exception
{
    public CassandraDbException()
    {
    }

    public CassandraDbException(string message) : base(message)
    {
    }

    public CassandraDbException(string message, Exception inner) : base(message, inner)
    {
    }
}