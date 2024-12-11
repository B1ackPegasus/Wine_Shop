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

    private List<Employee> _employees = [];

    public List<Employee> Employees
    {
        get => new List<Employee>(_employees);
    }
    
    private Employee? _manager = null;

    public Employee? Manager
    {
        get => _manager;
    } 

    public Employee(int pesel, string name, string surname, Address address)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        AddToExtent(this);
    }
    
    public Employee(int pesel, string name, string surname, Address address, List<Employee> employees)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        if (employees != null)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                AddEmployee(employees[i]);
            }
        }
        else
        {
            throw new ArgumentNullException();
        }
        
        AddToExtent(this);
    }
    
    public Employee(int pesel, string name, string surname, Address address, Employee manager)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        AddManager(manager);
        
        AddToExtent(this);
    }

    public Employee()
    {
    }

    public void AddManager(Employee manager)
    {
        if (manager == null)
        {
            throw new ArgumentNullException();
        }
        
        if(Manager == null)
        {
            if (!manager._employees.Contains(this))
            {
                _manager = manager;
                manager.AddEmployee(this);
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

    public void AddEmployee(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException();
        }
        
        if(employee.Manager == null)
        {
            _employees.Add(employee);
            employee.AddManager(this);
        }
        else if (employee.Manager == this)
        {
            _employees.Add(employee);
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
            if (Manager._employees.Contains(this))
            {
                Employee tempManager = Manager;
                
                _manager = null;
                tempManager.RemoveEmployee(this);
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

    public void RemoveEmployee(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException();
        }

        if (!_employees.Contains(employee))
        {
            throw new ArgumentException("There is no such employee for this manager!");
        }
        
        _employees.Remove(employee);
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
            for (int i = 0; i < _employees.Count; i++)
            {
                RemoveEmployee(_employees[i]);
            }

            for (int i = 0; i < NewEmployees.Count; i++)
            {
                AddEmployee(NewEmployees[i]);
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