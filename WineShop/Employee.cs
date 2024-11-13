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

    public Employee(int pesel, string name, string surname, Address address)
    {
        Pesel = pesel;
        Name = name;
        Surname = surname;
        Address = address;
        
        AddToExtent(this);
    }

    public Employee()
    {
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