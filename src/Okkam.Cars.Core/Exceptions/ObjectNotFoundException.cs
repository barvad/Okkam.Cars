namespace Okkam.Cars.Core.Exceptions;

/// <summary>
/// Исключение возникающее если указанный объект не найден.
/// </summary>
public class ObjectNotFoundException : Exception
{
    /// <inheritdoc />
    public ObjectNotFoundException()
    {
    }

    /// <inheritdoc />
    public ObjectNotFoundException(string message)
        : base(message)
    {
    }

    /// <inheritdoc />
    public ObjectNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}