using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class Cocktail
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
    
    private List<string> _listOfIngredients = [];
    
    public List<string> ListOfIngredients
    {
        get => new List<string>(_listOfIngredients);
        set
        {
            if (value.Contains(""))
            {
                throw new ArgumentException("Empty strings are not allowed.");
            }

            _listOfIngredients = value;
        }
    }
    
    private static List<Cocktail> _cocktailExtent = [];

    public static List<Cocktail> CocktailExtent
    {
        get => new List<Cocktail>(_cocktailExtent);
    }
    
    private Dictionary<int, Alcohol> _alcoholUsedInCocktail = new Dictionary<int, Alcohol>();

    public Dictionary<int, Alcohol> AlcoholUsedInCocktail
    {
        get => new Dictionary<int, Alcohol>(_alcoholUsedInCocktail);
    }
        
    private Dictionary<int, int> _volumeOfAlcoholInCocktail = new Dictionary<int, int>();
    
    public Dictionary<int, int> VolumeOfAlcoholInCocktail
    {
        get => new Dictionary<int, int>(_volumeOfAlcoholInCocktail);
    }
    
    public static int Alcohol_Id = 0;

    public Cocktail(int id, string name, List<string> listOfIngredients)
    {
        Id = id;
        Name = name;
        ListOfIngredients = listOfIngredients;
        AddToExtent(this);
    }

    public Cocktail()
    {
    }

    public void AddAlcoholToCocktail( Alcohol alcohol)
    {
        if ( alcohol == null)
        {
            throw new ArgumentNullException();
        }
        
        AlcoholUsedInCocktail.Add(Alcohol_Id+1, alcohol);
        
        if (!alcohol.CocktailsWithThisAlcohol.Contains(this))
        {
            alcohol.AddCocktailWithAlcohol(this);
        }
    }

    public void AddIngredient(string ingredient)
    {
        if (String.IsNullOrEmpty(ingredient))
        {
            throw new ArgumentException("Cannot be null!");
        }
        _listOfIngredients.Add(ingredient);
    }
    
    public void RemoveIngredient(int id)
    {
        _listOfIngredients.RemoveAt(id);
    }

    private static void AddToExtent(Cocktail cocktail)
    {
        if (cocktail == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _cocktailExtent.Add(cocktail);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _cocktailExtent.RemoveAt(id);
    }
    
    public static void save(string path = "cocktail.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Cocktail>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, CocktailExtent);
        }
    }

    public static bool load(string path = "cocktail.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _cocktailExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Cocktail>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _cocktailExtent = (List<Cocktail>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _cocktailExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _cocktailExtent.Clear();
                return false;
            }
        }
        return true;
    }
}
//test :getters seters ,exeptions add thing added properly 