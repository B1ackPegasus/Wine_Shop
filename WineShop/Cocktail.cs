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
    private List<KeyValuePair<int, Alcohol>> _alcoholUsedInCocktail = new List<KeyValuePair<int, Alcohol>>();

    public List<KeyValuePair<int, Alcohol>> AlcoholUsedInCocktail
    {
        get => new List<KeyValuePair<int, Alcohol>>(_alcoholUsedInCocktail);
    }
    
    private List<KeyValuePair<int, int>> _volumeOfAlcoholInCocktail = new List<KeyValuePair<int, int>>();
    
    public List<KeyValuePair<int, int>> VolumeOfAlcoholInCocktail
    {
        get => new List<KeyValuePair<int, int>>(_volumeOfAlcoholInCocktail);
    }
    
    public int Alcohol_Id = 0;

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

    public void AddAlcoholToCocktail( Alcohol alcohol, int Volume)
    {
        if ( alcohol == null)
        {
            throw new ArgumentNullException();
        }

        Alcohol_Id += 1;
        _alcoholUsedInCocktail.Add(new KeyValuePair<int, Alcohol>(Alcohol_Id, alcohol));
        _volumeOfAlcoholInCocktail.Add(new KeyValuePair<int, int>(Alcohol_Id, Volume));
        
        if (!alcohol.CocktailsWithThisAlcohol.Contains(this))
        {
            alcohol.AddCocktailWithAlcohol(this, Volume);
        }
    }

    public void RemoveAlcoholFromCocktail(Alcohol alcohol)
    {
        if (alcohol == null)
        {
            throw new ArgumentNullException();
        }

        if (AlcoholUsedInCocktail.FirstOrDefault(alc => alc.Value == alcohol, new KeyValuePair<int, Alcohol>(-1, new Alcohol())).Key != -1 )
        {
            var key = AlcoholUsedInCocktail.FirstOrDefault(alc => alc.Value == alcohol).Key;
            _alcoholUsedInCocktail.Remove(new KeyValuePair<int, Alcohol>(key, alcohol));
            
            var index = _volumeOfAlcoholInCocktail.FindIndex(alc => alc.Key == key);
            _volumeOfAlcoholInCocktail.RemoveAt(index);

            if (alcohol.CocktailsWithThisAlcohol.Contains(this))
            {
                alcohol.RemoveCocktailForAlcohol(this);
            }
        }
        else
        {
            throw new ArgumentException("There is no such alcohol!");
        }
    }

    public void EditAlcoholInCocktail(Alcohol alcohol1, Alcohol alcohol2, int Volume)
    {
        RemoveAlcoholFromCocktail(alcohol1);
        AddAlcoholToCocktail(alcohol2, Volume);
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