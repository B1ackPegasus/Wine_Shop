using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class Store:Facility
{
    private Time _openingTime;

    public Time OpeningTime
    {
        get => _openingTime;
        set
        {
            _openingTime = value;
        }
    }

    private Time _closingTime;

    public Time ClosingTime
    {
        get => _closingTime;
        set
        {
            _closingTime = value;
        }
    }
    
    private static List<Store> _storeExtent = [];

    public static List<Store> StoreExtent
    {
        get => new List<Store>(_storeExtent);
    }
    
    public Store(int id,Address address, Time openingTime, Time closingTime)
    {
        OpeningTime = openingTime; 
        ClosingTime = closingTime; 
        Id = id; 
        Address = address;
        
       AddToExtent(this);
    }

    public Store()
    {
    }

    private static void AddToExtent(Store store)
    {
        if (store == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _storeExtent.Add(store);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _storeExtent.RemoveAt(id);
    }
    
    public static void save(string path = "store.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Store>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, StoreExtent);
        }
    }

    public static bool load(string path = "store.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _storeExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Store>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _storeExtent = (List<Store>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _storeExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _storeExtent.Clear();
                return false;
            }
        }
        return true;
    }
}