namespace EAM.Data.Comm
{
    public interface IEntity
    {
    }

    public interface IEntity<T> : IEntity
    {
        T EntityId { get; set; }
    }
}