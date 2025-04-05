namespace ReceiptWriteOff.Application.Implementations.Exceptions;

public class AlreadyExistsException(string? message) : Exception(message);