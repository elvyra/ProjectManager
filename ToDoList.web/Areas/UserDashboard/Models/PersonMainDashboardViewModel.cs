using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.web.Areas.UserDashboard.Models
{
#nullable enable
    public class PersonMainDashboardViewModel
    {
        [StringLength(60)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string? Name { get; set; }

        [StringLength(60)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string? Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public PersonMainDashboardViewModel()
        {

        }
        public PersonMainDashboardViewModel(string? name, string? surname, string? email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
#nullable restore
}
