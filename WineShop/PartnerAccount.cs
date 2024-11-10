using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class PartnerAccount : Client
{
    private string _companyName;

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid company name.");
            }
            _companyName = value;
        }
    }
    
    private static List<PartnerAccount> _partnerAccountExtent = [];

    public static List<PartnerAccount> PartnerAccountExtent
    {
        get => new List<PartnerAccount>(_partnerAccountExtent);
    }

    public PartnerAccount(int id, string email, string phone, string companyName)
    {
        Id = id;
        Email = email;
        Phone = phone;
        CompanyName = companyName;
        AddToExtent(this);
    }

    public PartnerAccount()
    {
    }

    private static void AddToExtent(PartnerAccount partnerAccount)
    {
        if (partnerAccount == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _partnerAccountExtent.Add(partnerAccount);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _partnerAccountExtent.RemoveAt(id);
    }
    
    public static void save(string path = "partnerAccount.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PartnerAccount>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, PartnerAccountExtent);
        }
    }

    public static bool load(string path = "partnerAccount.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _partnerAccountExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PartnerAccount>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _partnerAccountExtent = (List<PartnerAccount>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _partnerAccountExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _partnerAccountExtent.Clear();
                return false;
            }
        }
        return true;
    }
}