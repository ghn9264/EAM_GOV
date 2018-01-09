namespace EAM.Data.Comm
{
    public class EntityBase : IEntity
    {
    }

    public class EntityBase<T> : IEntity<T>
    {
        public T EntityId { get; set; }
    }
}