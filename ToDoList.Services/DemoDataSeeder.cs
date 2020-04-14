using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Data;
using ToDoList.Domains;
using ToDoList.Domains.Enums;

namespace ToDoList.Services
{
    public class DemoDataSeeder : IDemoDataSeeder
    {
        private readonly ILogger<DemoDataSeeder> _logger;
        private readonly ToDoListDbContext _context;

        public DemoDataSeeder(ILogger<DemoDataSeeder> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }       

        public void SeedDemoData()
        {
            SeedFAQ();
            SeedPersonsProjectsTasks();
        }

        public void SeedFAQ()
        {
            try
            {
                var question1 = new FAQ { Question = "What is ToDoList?", Answer = "ToDoList is a Web App for web site content and projects managing." };
                var question2 = new FAQ { Question = "Is this available to my country?", Answer = "You can use ToDoList in all countries around the world, at your own risk." };
                var question3 = new FAQ { Question = "How do I use the features of ToDoList App?", Answer = "If you want to use ToDoList, you need Azure or any other variant for SQL database and deploying. The code needed you can download from <a href=\"https://github.com/elvyra/ToDoList.git\" target=\"_blank\">GitHub repository</a>." };
                var question4 = new FAQ { Question = "How much do the ToDoList App cost?", Answer = "ToDoList is absolutely free for all purposes (personal, learning and comercial). It was created for learning purposes, so be aware of using it on big data projects. We will appreciate your feedback and error reports, if any occours."};
                var question5 = new FAQ { Question = "I have technical problem, who do I email?", Answer = "If you have any question, problem, offer or suggestion, please, contact us via facebook or linked (see the links at the bottom of the page)."};
                _context.AddRange(question1, question2, question3, question4, question5);

                _context.SaveChanges();
                _logger.LogInformation("Demo FAQ seeded to database.");
            }
            catch (Exception error)
            {
                _logger.LogError($"Demo FAQ seeding throw an exception. Error: ${error.Message}");
            }
        }

        public void SeedPersonsProjectsTasks()
        {
            try
            {
                // Persons demo data

                var person1 = new Person { Email = "jonas@jonauskas", Name = "Jonas", Surname = "Jonauskas", Notes = "The most important person in office - Head Manager :)", IsPublic = true };
                var person2 = new Person { Email = "petras@petrauskas", Name = "Petras", Surname = "Petrauskas" };
                var person3 = new Person { Email = "saulius@saulys", Name = "Saulius", Surname = "Šaulys", Notes = "What do you think developers do all day long? Google!", IsPublic = true };
                var person4 = new Person { Email = "maryte@mieliauskaite", Name = "Marytė", Surname = "Mieliauskaitė" };
                var person5 = new Person { Email = "jolanta@jolante", Name = "Jolanta", Surname = "Jolantė", Notes = "Everything will be done on time and with cup of perfect coffe", IsPublic = true };
                var person6 = new Person { Email = "rite@riciute", Name = "Rita", Surname = "Ričiūtė", Notes = "Don't worry, plan!", IsPublic = true };
                
                var person7 = new Person { Email = "client@localhost", Name = "Demo", Surname = "Client" };
                person7.Address = "Malibu, California";
                person7.Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam fermentum enim neque.";

                var person8 = new Person { Email = "user@localhost", Name = "Demo", Surname = "User" };
                person8.Education = "B.S. in Computer Science from the University of Tennessee at Knoxville";
                person8.Skills = "UI Design Coding Javascript PHP Node.js";

                var person9 = new Person { Email = "admin@localhost", Name = "Demo", Surname = "Admin" };

                _context.AddRange(person1, person2, person3, person4, person5, person6, person7, person8, person9);

                // Tasks demo data

                var task1 = new Task("Platform preparation", Evaluation.Five, Priority.Low, Status.Done, person1);
                var task2 = new Task("Design mixins", Evaluation.Two, Priority.Critical, Status.New);
                var task3 = new Task("Products descriptions", Evaluation.One, Priority.Low, Status.Done, person4);
                var task4 = new Task("Import photos", Evaluation.Three, Priority.Low, Status.New);

                var task5 = new Task("Joomla preparation", Evaluation.Two, Priority.Low, Status.InProgress, person6);
                var task6 = new Task("Redesigning", Evaluation.Two, Priority.Critical, Status.Done, person8);
                var task7 = new Task("Info into database", Evaluation.One, Priority.Low, Status.Done);

                var task8 = new Task("Wordpress preparation", Evaluation.Two, Priority.Low, Status.Done, person1);
                var task9 = new Task("Custom Theme creation", Evaluation.Two, Priority.Critical, Status.Done, person3);
                var task10 = new Task("Deploying", Evaluation.Two, Priority.High, Status.New, person8);

                _context.AddRange(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10);

                // Projects demo data

                var description1 = "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts.";
                var project1 = new Project("E-shop Beauty-Mall", description1, true, "UAB TrussKate", DateTime.Now.AddYears(-1), 3);
                var description2 = "The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs. Waltz, bad nymph, for quick jigs vex! Fox nymp.";
                var project2 = new Project("Tantra", description2, true, "We Care", DateTime.Now, 18);
                var description3 = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings.";
                var project3 = new Project("Website \"Wish You Merry Christmas\"", description3, true, "Aldo", DateTime.Now.AddMonths(-8), 24);
                var description4 = "The European languages are members of the same family. Their separate existence is a myth. For science, music, sport, etc, Europe uses the same vocabulary.";
                var project4 = new Project("You need", description4, true, "AB NoneOF", DateTime.Now.AddMonths(-3), 12);
                var description5 = "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed in his bed into a horrible vermin. He lay on his armour-like back, and if he lifted his head a little he could.";
                var project5 = new Project("E-shop MewMew", description5, true, "AB NoneOF", DateTime.Now.AddYears(-2), 12);
                var description6 = "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large.";
                var project6 = new Project("Well Beeing", description6, true, "Apap", DateTime.Now.AddDays(-23), 38);
                var project7 = new Project("Youtube channel lister");
                var project8 = new Project("Social metwork for \"Puppies\"");

                project1.ProjectOwner = person8;
                project1.ScrumMaster = person1;
                project1.ClientEmail = person7.Email;
                project1.Tasks.Add(task1);
                project1.Tasks.Add(task2);
                project1.Tasks.Add(task3);
                project1.Tasks.Add(task4);
                project1.Team.Add(new ProjectTeam { Project = project1, Person = person1 });
                project1.Team.Add(new ProjectTeam { Project = project1, Person = person4 });
                project1.Team.Add(new ProjectTeam { Project = project1, Person = person6 });
                project1.Team.Add(new ProjectTeam { Project = project1, Person = person8 });
                project1.Stage = Stage.InProgress;

                project2.ProjectOwner = person3;
                project2.ScrumMaster = person8;
                project2.ClientEmail = person7.Email;
                project2.Tasks.Add(task5);
                project2.Tasks.Add(task6);
                project2.Tasks.Add(task7);
                project2.Team.Add(new ProjectTeam { Project = project2, Person = person5 });
                project2.Team.Add(new ProjectTeam { Project = project2, Person = person6 });
                project2.Stage = Stage.InProgress;

                project3.ProjectOwner = person8;
                project3.ClientEmail = person4.Email;
                project3.ScrumMaster = person5;
                project3.Stage = Stage.New;

                project4.ProjectOwner = person5;
                project4.ScrumMaster = person6;
                project4.ClientEmail = person7.Email;
                project4.Tasks.Add(task8);
                project4.Tasks.Add(task9);
                project4.Tasks.Add(task10);
                project4.Team.Add(new ProjectTeam { Project = project4, Person = person1 });
                project4.Team.Add(new ProjectTeam { Project = project4, Person = person3 });
                project4.Team.Add(new ProjectTeam { Project = project4, Person = person8 });
                project4.Stage = Stage.Completed;

                project5.ProjectOwner = person8;
                project5.ClientEmail = person1.Email;
                project5.Stage = Stage.Unconfirmed;

                project6.ClientEmail = person7.Email;
                project6.Stage = Stage.Unconfirmed;

                project7.ClientEmail = person7.Email;
                project7.Stage = Stage.Unconfirmed;

                project8.ProjectOwner = person4;
                project8.ScrumMaster = person8;
                project8.ClientEmail = person7.Email;
                project8.Stage = Stage.New;

                _context.AddRange(project1, project2, project3, project4, project5, project6, project7, project8);

                _context.SaveChanges();
                _logger.LogInformation("Demo data seeded to database.");
            }
            catch (Exception error)
            {
                _logger.LogError($"Demo data seeding throw an exception. Error: ${error.Message}");
            }
        }

        public void RemoveAllData()
        {
            try
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM [Tasks]");
                _context.Database.ExecuteSqlCommand("DELETE FROM [ProjectTeam]");
                _context.Database.ExecuteSqlCommand("DELETE FROM [Projects]");
                _context.Database.ExecuteSqlCommand("DELETE FROM [Persons]");
                _context.Database.ExecuteSqlCommand("DELETE FROM [FAQ]");

                _logger.LogInformation("Database cleared (front-end public demo function).");
            }
            catch (Exception error)
            {
                _logger.LogError($"Database clear function failed. Error: ${error.Message}");
            }
           
        }
    }
}
