using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ToDoList.Domains.Enums;

namespace ToDoList.Domains
{
    public class Project
    {
        [Required]
        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; private set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Title { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        public string Description { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?![\s.]+$)[a-zA-Z0-9\s.,.!?\-_'@#$%&*\\():;]*$", ErrorMessage = "Only latin letters, numbers and some special punctuation symbols are allowed")]
        [Display(Name = "Client Company")]
        public string ClientCompany { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }

        [Display(Name = "As Client")]
        public Person Client { get; set; }        

        [EnumDataType(typeof(Stage))]
        public Stage Stage { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        /*  [Range(typeof(DateTime), "this.Created", "this.Created.AddDays(365)",
                  ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]*/
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public IList<Task> Tasks { get; set; } = new List<Task>();

        [Display(Name = "Project Owner")]
        public Person ProjectOwner { get; set; }

        [Display(Name = "Scrum Master")]
        public Person ScrumMaster { get; set; }

        public IList<ProjectTeam> Team { get; set; } = new List<ProjectTeam>();

        public bool IsPublic { get; set; }

        [Timestamp]
        [Display(Name = "Last Updated")]
        public byte[] Timestamp { get; set; }

       

        public Project()
        {
            Id = new Guid();
            StartDate = DateTime.Now;
            EndDate = StartDate.AddMonths(1);
        }

        public Project(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            StartDate = DateTime.Now;
            EndDate = StartDate.AddMonths(1);
        }

        public Project(string title, string description, bool isPublic, string clientCompany, DateTime startDate, int duration)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsPublic = isPublic;
            ClientCompany = clientCompany;
            StartDate = startDate;
            EndDate = startDate.AddMonths(duration);
        }  
        
        public Project(string title, string description, bool isPublic, string clientCompany, string clientEmail, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsPublic = isPublic;
            ClientCompany = clientCompany;
            ClientEmail = clientEmail;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Project(Guid id, string title, string description, string clientCompany, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientCompany = clientCompany;
            StartDate = startDate;
            EndDate = endDate;
        }
        /// <summary>
        /// Generates new valid Id for project
        /// </summary>
        /// <returns> new Id </returns>

        public Guid NewId()
        {
            Id = Guid.NewGuid();
            return Id;
        }

        /// <summary>
        /// Sets given Id to project
        /// </summary>
        /// <param name="newId"></param>
        /// <returns> updated project </returns>

        public Project SetId(Guid newId)
        {
            Id = newId;
            return this;
        }

        /// <summary>
        /// Sets given new Start Date to project
        /// </summary>
        /// <param name="newDateTime"></param>
        /// <returns>updated project</returns>
        public Project SetStartData(DateTime newDateTime)
        {
            StartDate = newDateTime;
            return this;
        }
    }
}
