/*using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class Warehouse:Facility
{
    private int _storageLeft;

    public int StorageLeft
    {
        get => _storageLeft;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Invalid storage number.");
            }

            _storageLeft = value;
        }
    }
    
    private static List<Warehouse> _warehouseExtent = [];

    public static List<Warehouse> WarehouseExtent
    {
        get => new List<Warehouse>(_warehouseExtent);
    }

    public Warehouse(int storageLeft, int id, Address address)
    {
        StorageLeft = storageLeft;
        Id = id;
        Address = address;
        
        AddToExtent(this);
    }

    public Warehouse()
    {
    }

    private static void AddToExtent(Warehouse warehouse)
    {
        if (warehouse == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _warehouseExtent.Add(warehouse);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _warehouseExtent.RemoveAt(id);
    }
    
    public static void save(string path = "warehouse.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Warehouse>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, WarehouseExtent);
        }
    }

    public static bool load(string path = "warehouse.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _warehouseExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Warehouse>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _warehouseExtent = (List<Warehouse>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _warehouseExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _warehouseExtent.Clear();
                return false;
            }
        }
        return true;
    }

    public void DeleteFacility()
    {
        _warehouseExtent.Remove(this);
        
        for (int i = 0; i < EmployeesWorkInThisFacility.Count; i++)
        {
            EmployeesWorkInThisFacility[i].DeleteEmployee();
        }
    }
}*/