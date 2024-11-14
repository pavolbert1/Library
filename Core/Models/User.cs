using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
    }
}