using System.Collections.Generic;
using ToDoList.Domains;

namespace ToDoList.Services
{
    public interface IPublicDataService
    {
        IList<Project> PublicProjectsList();
        IList<Person> PublicPersonsList();
        IList<FAQ> FAQList();
    }
}