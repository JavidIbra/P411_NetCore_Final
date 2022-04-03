using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAcademyApp.Models.Entity
{
    public class Group : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
        public int EducationCategoryId { get; set; }
        public virtual EducationCategory EducationCategory { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
