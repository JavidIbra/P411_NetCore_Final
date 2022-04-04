using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.Entity
{
    public class Banner : BaseEntity
    {
        [Required , MaxLength(150)]
        public string Title { get; set; }
        [Required, MaxLength(500)]
        public string Text { get; set; }

        [Required, MaxLength(200)]
        public string Color { get; set; }
    }
}
