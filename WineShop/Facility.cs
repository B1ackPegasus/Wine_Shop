namespace WineShop;
using System.Xml;
using System.Xml.Serialization;


public class Facility
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
    
    private static List<Facility> _facilityExtent = [];

    public static List<Facility> StoreExtent
    {
        get => new List<Facility>(_facilityExtent);
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
    
    
    private FacilityType _typeOfFacility;

    public FacilityType TypeOfFacility
    {
        get => _typeOfFacility;
    }
    
    
    
    //Store
    private Time _openingTime;

    public Time OpeningTime
    {
        get{
            if (TypeOfFacility == FacilityType.Warehouse)
            {
                throw new Exception("You cannot access this variable!");
            }

            return _openingTime;
        }
        set
        {
            if (TypeOfFacility == FacilityType.Warehouse)
            {
                throw new Exception("You cannot access this variable!");
            }
            
            _openingTime = value;
        }
    }

    private Time _closingTime;

    public Time ClosingTime
    {
        get{
            if (TypeOfFacility == FacilityType.Warehouse)
            {
                throw new Exception("You cannot access this variable!");
            }

            return _closingTime;
        }
        set
        {
            if (TypeOfFacility == FacilityType.Warehouse)
            {
                throw new Exception("You cannot access this variable!");
            }
            
            _closingTime = value;
        }
    }
    
    
    
    //Warehouse
    private int _storageLeft;

    public int StorageLeft
    {
        get{
            if (TypeOfFacility == FacilityType.Store)
            {
                throw new Exception("You cannot access this variable!");
            }

            return _storageLeft;
        }
        set
        {
            if (TypeOfFacility == FacilityType.Store)
            {
                throw new Exception("You cannot access this variable!");
            }
            
            if (value < 0)
            {
                throw new ArgumentException("Invalid storage number.");
            }

            _storageLeft = value;
        }
    }
    
    public Facility(int id,Address address, Time openingTime, Time closingTime)
    {
        OpeningTime = openingTime; 
        ClosingTime = closingTime; 
        Id = id; 
        Address = address;
        _typeOfFacility = FacilityType.Store;
        
        AddToExtent(this);
    }
    
    public Facility(int storageLeft, int id, Address address)
    {
        StorageLeft = storageLeft;
        Id = id;
        Address = address;
        _typeOfFacility = FacilityType.Warehouse;
        
        AddToExtent(this);
    }
    
    public Facility(int id,Address address, Time openingTime, Time closingTime,int storageLeft)
    {
        OpeningTime = openingTime; 
        ClosingTime = closingTime; 
        Id = id; 
        Address = address;
        StorageLeft = storageLeft;
        _typeOfFacility = FacilityType.WarehouseStore;
        
        AddToExtent(this);
    }

    public Facility()
    {
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
    
    public void DeleteFacility()
    {
        _facilityExtent.Remove(this);
        
        for (int i = 0; i < EmployeesWorkInThisFacility.Count; i++)
        {
            EmployeesWorkInThisFacility[i].DeleteEmployee();
        }
    }
    
    private static void AddToExtent(Facility facility)
    {
        if (facility == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _facilityExtent.Add(facility);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _facilityExtent.RemoveAt(id);
    }
    
    public static void save(string path = "faclity.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Facility>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, StoreExtent);
        }
    }

    public static bool load(string path = "faclity.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _facilityExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Facility>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _facilityExtent = (List<Facility>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _facilityExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _facilityExtent.Clear();
                return false;
            }
        }
        return true;
    }
}

