using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI.Core.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime PlannedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsReturned { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
    }
}