using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAcademyApp.Models.Entity
{
    public class EducationCategory : BaseEntity
    {
        [Required,MaxLength(150)]
        public string Name { get; set; }

        //public virtual Contact Contact { get; set; }
        public  List<Contact> Contacts { get; set; }

        public List<Group> Groups { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile Img { get; set; }



    }
}
