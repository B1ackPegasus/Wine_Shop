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
    
    private List<KeyValuePair<int, Alcohol>> _alcoholStoredAtFacility = new List<KeyValuePair<int, Alcohol>>();

    public List<KeyValuePair<int, Alcohol>> AlcoholStoredAtFacility
    {
        get => new List<KeyValuePair<int, Alcohol>>(_alcoholStoredAtFacility);
    }
    
    private List<KeyValuePair<int, int>> _quantityOfAlcoholAtFacility = new List<KeyValuePair<int, int>>();
    
    public List<KeyValuePair<int, int>> QuantityOfAlcoholAtFacility
    {
        get => new List<KeyValuePair<int, int>>(_quantityOfAlcoholAtFacility);
    }
    
    public int Alcohol_Id = 0;

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
    
    public void AddAlcoholToFacility( Alcohol alcohol, int Quantity)
    {
        if ( alcohol == null)
        {
            throw new ArgumentNullException();
        }

        Alcohol_Id += 1;
        _alcoholStoredAtFacility.Add(new KeyValuePair<int, Alcohol>(Alcohol_Id, alcohol));
        _quantityOfAlcoholAtFacility.Add(new KeyValuePair<int, int>(Alcohol_Id, Quantity));
        
        if (!alcohol.FacilitiesWithThisAlcohol.Contains(this))
        {
            alcohol.AddFacilityWithAlcohol(this, Quantity);
        }
    }

    public void RemoveAlcoholFromFacility(Alcohol alcohol)
    {
        if (alcohol == null)
        {
            throw new ArgumentNullException();
        }

        if (AlcoholStoredAtFacility.FirstOrDefault(alc => alc.Value == alcohol, new KeyValuePair<int, Alcohol>(-1, new Alcohol())).Key != -1 )
        {
            var key = AlcoholStoredAtFacility.FirstOrDefault(alc => alc.Value == alcohol).Key;
            _alcoholStoredAtFacility.Remove(new KeyValuePair<int, Alcohol>(key, alcohol));
            
            var index = _quantityOfAlcoholAtFacility.FindIndex(alc => alc.Key == key);
            _quantityOfAlcoholAtFacility.RemoveAt(index);

            if (alcohol.FacilitiesWithThisAlcohol.Contains(this))
            {
                alcohol.RemoveFacilityWithAlcohol(this);
            }
        }
        else
        {
            throw new ArgumentException("There is no such alcohol!");
        }
    }

    public void EditAlcoholInFacility(Alcohol alcohol1, Alcohol alcohol2, int Quantity)
    {
        RemoveAlcoholFromFacility(alcohol1);
        AddAlcoholToFacility(alcohol2, Quantity);
    }
}

