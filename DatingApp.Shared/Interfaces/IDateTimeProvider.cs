namespace DatingApp.Shared.Interfaces;

public interface IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; }
    
}