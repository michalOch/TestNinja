using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        //private EmployeeContext _db;
        private IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage employeeStorage)
        {
            //_db = new EmployeeContext();
            _employeeStorage = employeeStorage;
        }

        public ActionResult DeleteEmployee(int id)
        {
            //var employee = _db.Employees.Find(id);
            //_db.Employees.Remove(employee);
            //_db.SaveChanges();
            _employeeStorage.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }   

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}