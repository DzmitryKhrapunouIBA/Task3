using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBA.Task3.DAL.Models
{
    public class Entity : IEntity
    {
        public Entity() { }

        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
    }

    public interface IEntity
    {
        int Id { get; set; }
    }
}