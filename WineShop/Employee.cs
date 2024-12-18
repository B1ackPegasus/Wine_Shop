using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class Employee
{

    private int _pesel;

    public int Pesel
    {
        get => _pesel;
        set
        {
            if (value < 0 || (value.ToString().Length < 9))

            {
                throw new ArgumentException("Invalid pesel.");
            }
            _pesel = value;
        }
    }

    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrEmpty(value) )
            {
                throw new ArgumentException("Invalid name.");
            }

            _name = value;
        }
    }

    private string _surname;

    public string Surname
    {
        get => _surname;
        set
        {
            if (String.IsNullOrEmpty(value) )
            {
                throw new ArgumentException("Invalid surname.");
            }

            _surname = value;
        }
    }

    private Address _address;

    public Address Address
    {
        get => _address;
        set
        {
            _address = value;
        }
    }
    private static int _workingHours = 8;

    public static int WorkingHours
    {
        get => _workingHours;
        set
        {
            _workingHours = value;
        }
    }

    private static List<Employee> _employeeExtent = [];

    public static List<Employee> EmployeeExtent
    {
        get => new List<Employee>(_employeeExtent);
    }

    private List<Employee> _employeesUnderThisManager = [];

    public List<Employee> EmployeesUnderThisManager
    {
        get => new List<Employee>(_employeesUnderThisManager);
    }
    
    private Employee? _manager = null;

    public Employee? Manager
    {
        get => _manager;
    }

    private Facility _facilityWhereEmployeeWorks;

    public Facility FacilityWhereEmployeeWorks
    {
        get => _facilityWhereEmployeeWorks;
    }

    public Employee(int pesel, string name, string surname, Address address, Facility facility)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        AddFacilityForEmployee(facility);
        
        AddToExtent(this);
    }
    
    public Employee(int pesel, string name, string surname, Address address, Facility facility, List<Employee> employees)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        AddFacilityForEmployee(facility);
        
        if (employees != null)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                AddEmployeeToManager(employees[i]);
            }
        }
        else
        {
            throw new ArgumentNullException();
        }
        
        AddToExtent(this);
    }
    
    public Employee(int pesel, string name, string surname, Address address, Facility facility, Employee manager)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        AddFacilityForEmployee(facility);
        
        AddManager(manager);
        
        AddToExtent(this);
    }

    public Employee()
    {
    }

    public void AddFacilityForEmployee(Facility facility)
    {
        if (facility == null)
        {
            throw new ArgumentNullException();
        }

        if (!facility.EmployeesWorkInThisFacility.Contains(this))
        {
            _facilityWhereEmployeeWorks = facility;
            facility.AddEmployeeToFacility(this);
        }
        else
        {
            _facilityWhereEmployeeWorks = facility;
        }
    }

    public void EditFacilityForEmployee(Facility facility)
    {
        if (facility == null)
        {
            throw new ArgumentNullException();
        }
        
        if (facility == FacilityWhereEmployeeWorks)
        {
            throw new ArgumentException("Employee already works in this facility");
        }
        else
        {
            FacilityWhereEmployeeWorks.RemoveEmployeeFromFacility(this);
            _facilityWhereEmployeeWorks = facility;
            facility.AddEmployeeToFacility(this);
        }
        
    }

    public void DeleteEmployee()
    {
        _employeeExtent.Remove(this);
        
        if (Manager != null)
        {
            RemoveManager();
        }

        if (EmployeesUnderThisManager.Count != 0)
        {
            for (int i = 0; i < EmployeesUnderThisManager.Count; i++)
            {
                RemoveEmployeeFromManager(EmployeesUnderThisManager[i]);
            }
        }
    }

    public void AddManager(Employee manager)
    {
        if (manager == null)
        {
            throw new ArgumentNullException();
        }
        
        if(Manager == null)
        {
            if (!manager._employeesUnderThisManager.Contains(this))
            {
                _manager = manager;
                manager.AddEmployeeToManager(this);
            }
            else
            {
                _manager = manager;
            }
        }
        else
        {
            throw new ArgumentException("This employee already has a manager!");
        }
    }

    public void AddEmployeeToManager(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException();
        }
        
        if(employee.Manager == null)
        {
            _employeesUnderThisManager.Add(employee);
            employee.AddManager(this);
        }
        else if (employee.Manager == this)
        {
            _employeesUnderThisManager.Add(employee);
        }
        else
        {
            throw new ArgumentException("This employee already has a manager!");
        }
    }

    public void RemoveManager()
    {
        if (Manager != null)
        {
            if (Manager._employeesUnderThisManager.Contains(this))
            {
                Employee tempManager = Manager;
                
                _manager = null;
                tempManager.RemoveEmployeeFromManager(this);
            }
            else
            {
                _manager = null;
            }
        }
        else
        {
            throw new ArgumentException("This employee does not have a manager!");
        }
        
    }

    public void RemoveEmployeeFromManager(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException();
        }

        if (!_employeesUnderThisManager.Contains(employee))
        {
            throw new ArgumentException("There is no such employee for this manager!");
        }
        
        _employeesUnderThisManager.Remove(employee);
        if (employee.Manager == this)
        {
            employee.RemoveManager();
        }
    }

    public void EditManager(Employee NewManager)
    {
        if (NewManager != null)
        {
            RemoveManager();
            AddManager(NewManager);
        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void EditEmployees(List<Employee> NewEmployees)
    {
        if (NewEmployees != null)
        {
            for (int i = 0; i < _employeesUnderThisManager.Count; i++)
            {
                RemoveEmployeeFromManager(_employeesUnderThisManager[i]);
            }

            for (int i = 0; i < NewEmployees.Count; i++)
            {
                AddEmployeeToManager(NewEmployees[i]);
            }
        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    private static void AddToExtent(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _employeeExtent.Add(employee);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _employeeExtent.RemoveAt(id);
    }
    
    public static void save(string path = "employee.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, EmployeeExtent);
        }
    }

    public static bool load(string path = "employee.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _employeeExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _employeeExtent = (List<Employee>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _employeeExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _employeeExtent.Clear();
                return false;
            }
        }
        return true;
    }
}