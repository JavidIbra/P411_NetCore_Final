using CodeAcademyApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.ViewModels
{
    public class GroupViewModel
    {
        public List<EducationCategory> EducationCategories { get; set; }
        public List<Group> Groups { get; set; }
    }
}
