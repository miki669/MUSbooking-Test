namespace MUSbooking.Exceptions;

public class OrderBadRequestException : Exception
{
    public OrderBadRequestException(string message) : base(message)
    {
    }
}