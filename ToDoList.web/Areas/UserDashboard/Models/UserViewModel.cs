using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.web.Areas.UserDashboard.Models
{
    public class UserViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string Surname { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Education { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Address { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Skills { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Notes { get; set; }

        public int IsProjectOwnerCount { get; set; }
        public int IsScrumMasterCount { get; set; }
        public int IsInTeamCount { get; set; }
    }
}
