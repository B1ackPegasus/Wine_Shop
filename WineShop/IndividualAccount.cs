using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class IndividualAccount : Client
{
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrEmpty(value))
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
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid surname.");
            }

            _surname = value;
        }
    }

    private string _username;

    public string Username
    {
        get => _username;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid username.");
            }

            _username = value;
        }
    }

    private int _age;

    public int Age
    {
        get => _age;
        set
        {
            if (value<18)
            {
                throw new ArgumentException("Invalid age.");
            }

            _age = value;
        }
    }
    
    private static List<IndividualAccount> _individualAccountExtent = [];
    public static List<IndividualAccount> IndividualAccountExtent 
    {
        get => new List<IndividualAccount>(_individualAccountExtent);
    }
    
    public IndividualAccount(int id, string email, string phone, string name, string surname, string username, int age)
    {
        Id = id;
        Email = email;
        Phone = phone;
        Name = name;
        Surname = surname;
        Username = username;
        Age = age;
        TypeOfClient = ClientType.NonPremium;
        
        AddToExtent(this);
    }
    
    public IndividualAccount(int id, string email, string phone, string name, string surname, string username, int age, MyDate startDate, MyDate endDate, List<string> benefits)
    {
        Id = id;
        Email = email;
        Phone = phone;
        Name = name;
        Surname = surname;
        Username = username;
        Age = age;
        
        StartDate = startDate;
        EndDate = endDate;
        Benefits = benefits;
        TypeOfClient = ClientType.Premium;
        
        AddToExtent(this);
    }

    public IndividualAccount()
    {
    }

    private static void AddToExtent(IndividualAccount individualAccount)
    {
        if (individualAccount == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _individualAccountExtent.Add(individualAccount);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _individualAccountExtent.RemoveAt(id);
    }
    
    public static void save(string path = "individualAccount.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IndividualAccount>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, IndividualAccountExtent);
        }
    }

    public static bool load(string path = "individualAccount.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _individualAccountExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IndividualAccount>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _individualAccountExtent = (List<IndividualAccount>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _individualAccountExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _individualAccountExtent.Clear();
                return false;
            }
        }
        return true;
    }
    
}