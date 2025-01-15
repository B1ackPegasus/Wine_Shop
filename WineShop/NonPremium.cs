/*using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class NonPremium : Client
{
    private static List<NonPremium> _nonPremiumExtent = [];

    public static List<NonPremium> NonPremiumExtent
    {
        get => new List<NonPremium>(_nonPremiumExtent);
    }
    
    public NonPremium(int id, string email, string phone)
    {
        Id = id;
        Email = email;
        Phone = phone;
        
        AddToExtent(this);
    }

    public NonPremium()
    {
    }

    private static void AddToExtent(NonPremium nonPremium)
    {
        if (nonPremium == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _nonPremiumExtent.Add(nonPremium);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _nonPremiumExtent.RemoveAt(id);
    }
    
    public static void save(string path = "nonPremium.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<NonPremium>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, NonPremiumExtent);
        }
    }

    public static bool load(string path = "nonPremium.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _nonPremiumExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<NonPremium>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _nonPremiumExtent = (List<NonPremium>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _nonPremiumExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _nonPremiumExtent.Clear();
                return false;
            }
        }
        return true;
    }

}*/