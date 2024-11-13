using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class Premium: Client
{
    private MyDate _startDate;

    public MyDate StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
        }
    }
    
    private MyDate _endDate;

    public MyDate EndDate
    {
        get => _endDate;
        set
        {
            if (StartDate.CompareTo(value))

            {
                throw new ArgumentException("Invalid end date.");
            }

            _endDate = value;
        }
    }

    
    private List<string> _benefits = [];

    public List<string> Benefits
    {
        get => new List<string>(_benefits);
        set
        {
            if (value.Count < 1)
            {
                throw new ArgumentException("Invalid benefits list.");
            }

            if (value.Contains(""))
            {
                throw new ArgumentException("Empty strings are not allowed.");
            }

            _benefits = value;
        }
    }
    
    private static List<Premium> _premiumExtent = [];

    public static List<Premium> PremiumExtent
    {
        get => new List<Premium>(_premiumExtent);
    }
    
    public Premium(int id, string email, string phone, MyDate startDate, MyDate endDate, List<string> benefits)
    {
        Id = id;
        Email = email;
        Phone = phone; 
        StartDate = startDate;
        EndDate = endDate;
        Benefits = benefits;
        AddToExtent(this);
    }

    public Premium()
    {
    }

    public void AddBenefit(string benefit)
    {
        if (String.IsNullOrEmpty(benefit))
        {
            throw new ArgumentException("Cannot be null!");
        }
        _benefits.Add(benefit);
    }
    
    public void RemoveBenefit(int id)
    {
        _benefits.RemoveAt(id);
    }
    
    private static void AddToExtent(Premium premium)
    {
        if (premium == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _premiumExtent.Add(premium);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _premiumExtent.RemoveAt(id);
    }
    
    public static void save(string path = "premium.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Premium>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, PremiumExtent);
        }
    }

    public static bool load(string path = "premium.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _premiumExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Premium>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _premiumExtent = (List<Premium>)xmlSerializer.Deserialize(reader);
            }

            catch (InvalidCastException)
            {
                _premiumExtent.Clear();
                return false;
            }
            catch (Exception)
            {

                _premiumExtent.Clear();
                return false;
            }
        }
        return true;
    }

}