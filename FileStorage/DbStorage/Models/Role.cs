namespace DbStorage.Models
{
    public class Role : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
    }
}
