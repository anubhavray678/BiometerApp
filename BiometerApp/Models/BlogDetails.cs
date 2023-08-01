
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BiometerApp.Models
{
    public class BlogDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string AutherName { get; set; }

        [Required]
        public string BlogTitle { get; set; }

        [Required]
        public string FirstPara { get; set; }

        [Required]
        [NotMapped]
       
        public IFormFile CoverImage { get; set; }

        [Required]
        public string SecondPara { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now;


        [Required]
        [DataType(DataType.Upload)]
        public string CoverImagePath { get; set; }



    }
}
