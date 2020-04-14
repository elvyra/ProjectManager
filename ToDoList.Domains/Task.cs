using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ToDoList.Domains.Enums;

namespace ToDoList.Domains
{
    public class Task
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

        [EnumDataType(typeof(Evaluation))]
        public Evaluation Evaluation { get; set; }

        [EnumDataType(typeof(Priority))]
        public Priority Priority { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created On")]
        public DateTime Created { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        /*  [Range(typeof(DateTime), "this.Created", "this.Created.AddDays(365)",
                  ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]*/
        [Display(Name = "Till date")]
        public DateTime TillDate { get; set; }

        [Display(Name = "Project")]
        public Project Project { get; set; }

        [Display(Name = "Assigned To")]
        public Person AssignedTo { get; set; }

        [Timestamp]
        [Display(Name = "Last Updated")]
        public byte[] Timestamp { get; set; }


        public Task()
        {
            Id = new Guid();
            Created = DateTime.Now;
            TillDate = Created.AddDays(1);
        }

        // For demo data creation 
        public Task(string title, Evaluation evaluation, Priority priority, Status status)
        {
            Id = Guid.NewGuid();
            Title = title;
            Evaluation = evaluation;
            Priority = priority;
            Status = status;
            Created = DateTime.Now;
            TillDate = DateTime.Now.AddDays(3);
        }

        public Task(string title, Evaluation evaluation, Priority priority, Status status, Person assignedTo) : this(title, evaluation, priority, status)
        {
            AssignedTo = assignedTo;
        }

        public Task(Guid id, string title, string description, Evaluation evaluation, Priority priority, Status status, DateTime created, DateTime tillDate)
        {
            Id = id;
            Title = title;
            Description = description;
            Evaluation = evaluation;
            Priority = priority;
            Status = status;
            Created = created;
            TillDate = tillDate;
        }

        /// <summary>
        /// Generates new valid Id for task
        /// </summary>
        /// <returns> new Id </returns>

        public Guid NewId()
        {
            Id = Guid.NewGuid();
            return Id;
        }

        /// <summary>
        /// Sets given Id to task
        /// </summary>
        /// <param name="newId"></param>
        /// <returns> updated task </returns>

        public Task SetId(Guid newId)
        {
            Id = newId;
            return this;
        }
    }
}
