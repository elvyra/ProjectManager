using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.web.Areas.AdminDashboard.Models
{
    public class CreateUserViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string Surname { get; set; }

        public string Role{ get; set; }
    }
}
