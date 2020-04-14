using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToDoList.Domains
{
    public class Person
    {
        [Required]
        [Key]
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

        public bool IsPublic { get; set; }

        [Timestamp]
        [Display(Name = "Last Updated")]
        public byte[] Timestamp { get; set; }


        public IList<Task> Tasks { get; set; } = new List<Task>();

        [InverseProperty("ProjectOwner")]
        [Display(Name = "As Project Owner")]
        public IList<Project> AsProjectOwner { get; set; } = new List<Project>();

        [InverseProperty("ScrumMaster")]
        [Display(Name = "As Scrum Master")]
        public IList<Project> AsScrumMaster { get; set; } = new List<Project>();

        [Display(Name = "In Projects Teams")]
        public IList<ProjectTeam> InTeams { get; set; } = new List<ProjectTeam>();

        [Display(Name = "In Projects Teams")]
        public IList<Project> AsClient { get; set; } = new List<Project>();

    }
}
