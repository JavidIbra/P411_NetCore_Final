using System.ComponentModel.DataAnnotations;

namespace CodeAcademyApp.Models.Entity
{
    public class Contact : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }
        [Required, MaxLength(150)]
        public string Surname { get; set; }
        [Required, MaxLength(150)]
        public string PhoneNumber { get; set; }
        public int EducationCategoryId { get; set; }
        public virtual EducationCategory EducationCategory { get; set; }

    }
}
