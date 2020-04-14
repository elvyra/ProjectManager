using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains;
using ToDoList.Domains.Enums;

namespace ToDoList.web.Areas.AdminDashboard.Models
{
    public class EditProjectViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Title { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }

        [EnumDataType(typeof(Stage))]
        public Stage Stage { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        /*  [Range(typeof(DateTime), "this.Created", "this.Created.AddDays(365)",
                  ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]*/
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }


        [DataType(DataType.EmailAddress)]
        public string ProjectOwnerEmail { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ScrumMasterEmail { get; set; }

        public IList<ProjectTeam> Team { get; set; } = new List<ProjectTeam>();

        public bool IsPublic { get; set; }




        public IList<Person> Users { get; set; }
        public IList<Person> Clients { get; set; }
    }
}
