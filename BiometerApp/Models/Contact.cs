using System.ComponentModel.DataAnnotations;
namespace BiometerApp.Models

{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string message { get; set; }
    }
}
