using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domains
{
    public class ProjectTeam
    {
        public Guid Id { get; private set; }
        public Project Project { get; set; }

        public string Email { get; set; }
        public Person Person { get; set; }
    }
}
