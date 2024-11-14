using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Core.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
        public required string Isbn { get; set; }
        public bool IsLoaned { get; set; }
    }
}