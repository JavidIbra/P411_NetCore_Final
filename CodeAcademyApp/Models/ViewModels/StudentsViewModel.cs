using CodeAcademyApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.ViewModels
{
    public class StudentsViewModel
    {
        public Group Group { get; set; }
        public List<Student> Students { get; set; }
    }
}
