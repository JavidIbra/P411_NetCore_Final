using CodeAcademyApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.ViewModels
{
    public class TeacherViewModel
    {
        public List<Group> Groups { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<EducationCategory> EducationCategories { get; set; }
        //public AppUser User { get; set; }
        public Teacher Teacher { get; set; }
    }
}
