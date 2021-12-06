using System.ComponentModel.DataAnnotations;

namespace Weather.Models
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
