namespace WineShop;

public abstract class Facility
{
    private int _id;

    public int Id
    {
        get => _id;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            _id = value;
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
    
    private List<Employee> _employeesWorkInThisFacility = [];

    public List<Employee> EmployeesWorkInThisFacility
    {
        get => new List<Employee>(_employeesWorkInThisFacility);
    }

    public void AddEmployeeToFacility(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException();
        }

        if (employee.FacilityWhereEmployeeWorks == null)
        {
            _employeesWorkInThisFacility.Add(employee);
            employee.AddFacilityForEmployee(this);
        }
        else if (employee.FacilityWhereEmployeeWorks == this)
        {
            _employeesWorkInThisFacility.Add(employee);
        }
        else
        {
            throw new ArgumentException("This employee is already assigned to another facility!");
        }
    }

    public void RemoveEmployeeFromFacility(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException();
        }

        if (_employeesWorkInThisFacility.Contains(employee))
        {
            _employeesWorkInThisFacility.Remove(employee);
        }
        else
        {
            throw new ArgumentException("No such employee at this facility!");
        }
    }

    public abstract void DeleteFacility();
}

