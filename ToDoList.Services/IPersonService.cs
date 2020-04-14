using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Domains;

namespace ToDoList.Services
{
    public interface IPersonService
    {
        IList<Person> GetAllUsers();

        Person GetUserByEmail(string email);

        Person DeleteUserByEmail(string email);

        Person GetUserByEmailForDashboard(string email);

        Person EditUser(Person user);
    }
}