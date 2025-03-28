using System.ComponentModel;

namespace EmployeeManagementSystem.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public DateTime BirthDay { get; set; }

        public string BirthPlace { get; set; }

        public void ChangeEmployee(Employee employee)
        {
            if (Id == employee.Id)
            {
                Name = employee.Name;
                Role = employee.Role;
                Email = employee.Email;
                BirthDay = employee.BirthDay;
                BirthPlace = employee.BirthPlace;
            }
        }

        public void ChangeEmployee(InputHelper helper)
        {
            if (Id == helper.Id)
            {
                Name = helper.Name;
                Role = helper.SelectedRole;
                Email = helper.Email;
                BirthDay = helper.BirthDay ?? DateTime.MinValue;
                BirthPlace = helper.BirthPlace;
            }
        }
    }
}
