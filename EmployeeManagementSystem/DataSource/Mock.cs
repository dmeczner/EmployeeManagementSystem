using EmployeeManagementSystem.Model;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManagementSystem.DataSource
{
    public static class Mock
    {
        public static List<Role> Roles { get; set; }
        public static List<Employee> Employees { get; set; }
        static Mock()
        {
            Roles = [
                new Role{ Id = 1, Name = "CEO" },
                new Role{ Id = 2, Name = "HR" },
                new Role{ Id = 3, Name = "Software Developer" },
                new Role{ Id = 4, Name = "System Administrator" },
                new Role{ Id = 5, Name = "Back Office" },
                new Role{ Id = 6, Name = "Dispatcher" },
                new Role{ Id = 7, Name = "Project Manager" },
                new Role{ Id = 8, Name = "Business Analyst" },
                new Role{ Id = 9, Name = "Test Analist" }
                ];

            Employees = [
                new Employee{ Id = 1, Name = "Gábor Kovács", Role = Roles[2], Email = "gabor.kovacs@example.com", BirthDay = new (1995, 5, 15 ), BirthPlace = "Budapest" },
                new Employee{ Id = 2, Name = "Eszter Szabó", Role = Roles[7], Email = "eszter.szabo@example.com", BirthDay = new(1997, 3, 22), BirthPlace = "Debrecen" },
                new Employee{ Id = 3, Name = "László Tóth", Role = Roles[3], Email = "laszlo.toth@example.com", BirthDay = new(1990, 8, 10), BirthPlace = "Szeged" },
                new Employee{ Id = 4, Name = "Zsófia Nagy", Role = Roles[1], Email = "zsofia.nagy@example.com", BirthDay = new(1993, 12, 5), BirthPlace = "Miskolc" },
                new Employee{ Id = 5, Name = "Bence Horváth", Role = Roles[4], Email = "bence.horvath@example.com", BirthDay = new(1996, 7, 18), BirthPlace = "Pécs" },
                new Employee{ Id = 6, Name = "Anna Varga", Role = Roles[5], Email = "anna.varga@example.com", BirthDay = new(1998, 11, 30), BirthPlace = "Győr" },
                new Employee{ Id = 7, Name = "Tamás Kiss", Role = Roles[6], Email = "tamas.kiss@example.com", BirthDay = new(1985, 2, 25), BirthPlace = "Kecskemét" },
                new Employee{ Id = 8, Name = "Judit Molnár", Role = Roles[8], Email = "judit.molnar@example.com", BirthDay = new(1992, 9, 14), BirthPlace = "Nyíregyháza" },
                new Employee{ Id = 9, Name = "Péter Németh", Role = Roles[0], Email = "peter.nemeth@example.com", BirthDay = new(1980, 1, 20), BirthPlace = "Székesfehérvár" },
                new Employee{ Id = 10, Name = "Katalin Farkas", Role = Roles[2], Email = "katalin.farkas@example.com", BirthDay = new DateTime(1994, 4, 12), BirthPlace = "Budapest" },
                new Employee{ Id = 11, Name = "István Balogh", Role = Roles[7], Email = "istvan.balogh@example.com", BirthDay = new DateTime(1989, 6, 25), BirthPlace = "Debrecen" },
                new Employee{ Id = 12, Name = "Mária Szűcs", Role = Roles[3], Email = "maria.szucs@example.com", BirthDay = new DateTime(1991, 11, 3), BirthPlace = "Szeged" },
                new Employee{ Id = 13, Name = "Ferenc Varga", Role = Roles[1], Email = "ferenc.varga@example.com", BirthDay = new DateTime(1987, 2, 14), BirthPlace = "Miskolc" },
                new Employee{ Id = 14, Name = "Lilla Tóth", Role = Roles[4], Email = "lilla.toth@example.com", BirthDay = new DateTime(1999, 8, 19), BirthPlace = "Pécs" },
                new Employee{ Id = 15, Name = "Zoltán Kovács", Role = Roles[5], Email = "zoltan.kovacs@example.com", BirthDay = new DateTime(1996, 10, 7), BirthPlace = "Győr" },
                new Employee{ Id = 16, Name = "Erika Nagy", Role = Roles[6], Email = "erika.nagy@example.com", BirthDay = new DateTime(1984, 1, 30), BirthPlace = "Kecskemét" },
                new Employee{ Id = 17, Name = "Gergely Horváth", Role = Roles[8], Email = "gergely.horvath@example.com", BirthDay = new DateTime(1993, 5, 21), BirthPlace = "Nyíregyháza" },
                new Employee{ Id = 18, Name = "Ágnes Kiss", Role = Roles[2], Email = "agnes.kiss@example.com", BirthDay = new DateTime(1981, 12, 11), BirthPlace = "Székesfehérvár" }
                ];

        }

        public static Employee AddEmployee(InputHelper currentInputHelper)
        {
            currentInputHelper.Id = GetNextId();
            Employee employee = new()
            {
                Id = currentInputHelper.Id
            };
            employee.ChangeEmployee(currentInputHelper);
            Employees.Add(employee);
            return employee;
        }

        private static int GetNextId()
        {
            var nextId = Employees.Max(x => x.Id);
            return ++nextId;
        }

        public static bool UpdateEmployee(Employee employee)
        {
            var selectedEmployee = Employees.Single(x => x.Id == employee.Id);
            selectedEmployee.ChangeEmployee(employee);
            return true;
        }

        public static bool DeleteEmployee(int id)
        {
            var selectedEmployee = Employees.Single(x => x.Id == id);
            Employees.Remove(selectedEmployee);
            return true;
        }

        public static async Task SaveData()
        {
            await JsonHelper.SerializeToFileAsync(Employees, "json");
        }

        public static async Task<List<Employee>?> LoadData()
        {
            var employeesFromJson = await JsonHelper.DeserializeFromFileAsync<List<Employee>>("json");

            var addedEmployeesToSource = new List<Employee>();
            // if not exists add in memory
            if (employeesFromJson != null)
            {
                foreach (var employee in employeesFromJson)
                {
                    if (Employees.FirstOrDefault(x => x.Id == employee.Id) is null)
                    {
                        Employees.Add(employee);
                        addedEmployeesToSource.Add(employee);
                    }
                }
                return addedEmployeesToSource;
            }

            return null;

        }
    }
}
