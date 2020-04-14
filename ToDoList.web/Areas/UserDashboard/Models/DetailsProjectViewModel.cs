using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains.Enums;

namespace ToDoList.web.Areas.UserDashboard.Models
{
#nullable enable
    public class DetailsProjectViewModel
    {
        [Required]
        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Title { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string? Description { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        [Display(Name = "Client Company")]
        public string? ClientCompany { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string? ProjectOwnerName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string? ProjectOwnerSurname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? ProjectOwnerEmail { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string? ScrumMasterName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.]*$", ErrorMessage = "Only latin letters and numbers are allowed")]
        public string? ScrumMasterSurname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? ScrumMasterEmail { get; set; }

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

        public int TotalTasksCount { get; set; }
        public int DoneTasksCount { get; set; }
        public int TeamMembersCount { get; set; }


        public DetailsProjectViewModel()
        {
            Title = "   ";
        }
        public DetailsProjectViewModel(Guid id, string title, string? description, string? clientCompany, string? projectOwnerName, string? projectOwnerSurname, string? projectOwnerEmail, DateTime startDate, DateTime endDate, Stage stage, int totalTasksCount, int doneTasksCount, int teamMembersCount)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientCompany = clientCompany;
            ProjectOwnerName = projectOwnerName;
            ProjectOwnerSurname = projectOwnerSurname;
            ProjectOwnerEmail = projectOwnerEmail;
            StartDate = startDate;
            EndDate = endDate;
            Stage = stage;
            TotalTasksCount = totalTasksCount;
            DoneTasksCount = doneTasksCount;
            TeamMembersCount = teamMembersCount;
        }
    }
#nullable restore
}
