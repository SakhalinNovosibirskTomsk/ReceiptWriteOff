namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string? message) : base(message)
    {
    }
}