using System.ComponentModel.DataAnnotations;

namespace PracticalTask5_RestApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Group { get; set; } = string.Empty;

        [Range(0, 5, ErrorMessage = "GPA must be between 0 and 5.")]
        public double GPA { get; set; }
    }
}
