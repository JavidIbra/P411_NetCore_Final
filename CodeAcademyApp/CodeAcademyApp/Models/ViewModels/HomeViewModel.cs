using CodeAcademyApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Service> services { get; set; }
        public List<Banner> banners { get; set; }
        public Contact contact { get; set; }
        public List<EducationCategory> educationCategories { get; set; }
    }
}
