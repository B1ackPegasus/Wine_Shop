using System.Xml;
using System.Xml.Serialization;

namespace WineShop;

[Serializable]
public class Alcohol
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

   private string _brand;

   public string Brand
   {
       get => _brand;
       set
       {
           if (String.IsNullOrEmpty(value))
           {
               throw new ArgumentException("Invalid brand.");
           }

           _brand = value;
           
       }
   }

   private double _price;

   public double Price
   {
       get => _price;
       set
       {
           if (value<= 0)
           {
               throw new ArgumentException("Invalid price.");
           }

           _price = value;
       }
   }

   private Type _type;

   public Type Type
   {
       get => _type;
       set
       {
           _type = value;
       }
   }

   private int _yearOfManufacture;

   public int YearOfManufacture
   {
       get => _yearOfManufacture;
       set
       {
           if (value <= 0 ||
               value > DateTime.Now.Year)
           {
               throw new ArgumentException("Invalid year.");
           }

           _yearOfManufacture = value;
       }
   }

   private int _age;

   public int Age
   {
       get => _age;
   }
    
    private static List<Alcohol> _alcoholExtent = [];

    public static List<Alcohol> AlcoholExtent
    {
        get => new List<Alcohol>(_alcoholExtent);
    }

    private List<Cocktail> _cocktailsWithThisAlcohol = [];

    public List<Cocktail> CocktailsWithThisAlcohol
    {
        get => new List<Cocktail>(_cocktailsWithThisAlcohol);
    }
    
    public Alcohol(string name, string brand, double price, Type type, int yearOfManufacture)
    {
        Name = name;
        Brand = brand;
        Price = price;
        Type = type;
        YearOfManufacture = yearOfManufacture;
        if (type == Type.Wine || type == Type.Spirit)
        {
            _age = DateTime.Now.Year - yearOfManufacture;
        }
        AddToExtent(this);
    }

    public Alcohol()
    {
    }

    public void AddCocktailWithAlcohol(Cocktail cocktail, int Volume)
    {
        if (cocktail == null)
        {
            throw new ArgumentNullException();
        }
        
        _cocktailsWithThisAlcohol.Add(cocktail);
        
        if (cocktail.AlcoholUsedInCocktail.FirstOrDefault(alc => alc.Value == this, new KeyValuePair<int, Alcohol>(-1, new Alcohol())).Key == -1)
        {
            cocktail.AddAlcoholToCocktail(this, Volume);
        }
    }

    public void RemoveCocktailForAlcohol(Cocktail cocktail)
    {
        if (cocktail == null)
        {
            throw new ArgumentNullException();
        }

        if (CocktailsWithThisAlcohol.Contains(cocktail))
        {
            _cocktailsWithThisAlcohol.Remove(cocktail);
            
            if (cocktail.AlcoholUsedInCocktail.FirstOrDefault(alc => alc.Value == this, new KeyValuePair<int, Alcohol>(-1, new Alcohol())).Key != -1)
            {
                cocktail.RemoveAlcoholFromCocktail(this);
            }
        }
        else
        {
            throw new ArgumentException("There is no such cocktail for this alcohol!");
        }
    }

    public void EditCocktailForAlcohol(Cocktail cocktail1, Cocktail cocktail2, int Volume)
    {
        RemoveCocktailForAlcohol(cocktail1);
        AddCocktailWithAlcohol(cocktail2, Volume);
    }

    private static void AddToExtent(Alcohol alcohol)
    {
        if (alcohol == null)
        {
            throw new ArgumentException("Cannot be null!");
        }
        _alcoholExtent.Add(alcohol);
    }
    
    private static void DeleteFromExtent(int id)
    {
        _alcoholExtent.RemoveAt(id);
    }

    public static void save(string path = "alcohol.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Alcohol>));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, AlcoholExtent);
        }
    }

    public static bool load(string path = "alcohol.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch(FileNotFoundException)
        {
            _alcoholExtent.Clear();
            return false;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Alcohol>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _alcoholExtent = (List<Alcohol>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _alcoholExtent.Clear();
                return false;
            }
            catch (Exception)
            {
                _alcoholExtent.Clear();
                return false;
            }
        }
        return true;
    }

}