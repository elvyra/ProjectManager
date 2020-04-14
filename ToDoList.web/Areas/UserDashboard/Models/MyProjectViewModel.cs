using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains.Enums;

namespace ToDoList.web.Areas.UserDashboard.Models
{
#nullable enable
    public class MyProjectViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string? Title { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation su=ymbols are allowed")]
        public string? Description { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        [Display(Name = "Client Company")]
        public string? ClientCompany { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? ClientEmail { get; set; }

        public PersonMainDashboardViewModel? ProjectOwner { get; set; }

        public PersonMainDashboardViewModel? ScrumMaster { get; set; }

        public IList<PersonMainDashboardViewModel> Team { get; set; } = new List<PersonMainDashboardViewModel>();

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [EnumDataType(typeof(Stage))]
        public Stage Stage { get; set; }

        public bool IsPublic { get; set; }

        public int Duration { get; set; }

        public int Completeness { get; set; }

        public int TillDeadline { get; set; }


        public MyProjectViewModel()
        {
        }
    }
#nullable restore
}
