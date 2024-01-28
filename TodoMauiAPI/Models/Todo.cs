using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace TodoMauiAPI.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public string? TodoName { get; set; }
    }
}
