using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains.Enums;

namespace ToDoList.web.Areas.UserDashboard.Models
{
    public class TaskViewModel
    {

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Title { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Description { get; set; }

        [EnumDataType(typeof(Evaluation))]
        public Evaluation Evaluation { get; set; }

        [EnumDataType(typeof(Priority))]
        public Priority Priority { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        /*  [Range(typeof(DateTime), "this.Created", "this.Created.AddDays(365)",
                  ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]*/
        [Display(Name = "Till date")]
        public DateTime TillDate { get; set; }

        [Display(Name = "Project")]
        public Guid ProjectId { get; set; }


        public TaskViewModel()
        {
        }

    }
}

