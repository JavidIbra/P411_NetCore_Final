using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.Entity
{
    public class Service : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }
        [Required, MaxLength(150)]
        public string Text { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile Img { get; set; }

    }
}
