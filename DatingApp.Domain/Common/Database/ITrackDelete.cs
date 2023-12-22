namespace DatingApp.Domain.Common.Database;
public interface IDeletable
{

}
public interface INoTrackDelete : IDeletable
{

}

public interface ITrackDelete : IDeletable
{
    // Use Shadow Properties in Delete
}