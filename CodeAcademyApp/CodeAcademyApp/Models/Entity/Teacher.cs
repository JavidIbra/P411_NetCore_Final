using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.Entity
{
    public class Teacher : BaseEntity
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public List<Group> Groups { get; set; }

    }
}
