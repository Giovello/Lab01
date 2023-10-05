using System.ComponentModel.DataAnnotations;

namespace Lab01.Models
{
    public class Employer
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public DateTime? IncorporatedDate { get; set; }
    }
}