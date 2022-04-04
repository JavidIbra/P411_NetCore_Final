using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAcademyApp.Models.Entity
{
    public class Student : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(150),EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string PhoneNumber { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int Mark { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
