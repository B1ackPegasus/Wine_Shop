namespace WineShop;

public abstract class Client
{
    private int _id;
    public int Id
    {
        get => _id;
        set 
        {
            if (value <=0)
            {
                throw new ArgumentException("Invalid Id.");
            }
            _id = value;
        }
        
    }

    private string _email;

    public string Email
    {
        get => _email;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid email.");
            }

            _email = value;
        }
    }

    private string _phone;

    public string Phone
    {
        get => _phone;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid phone.");
            }

            _phone = value;
        }
    }
    
    private List<KeyValuePair<int, Alcohol>> _alcoholInCart = new List<KeyValuePair<int, Alcohol>>();

    public List<KeyValuePair<int, Alcohol>> AlcoholInCart
    {
        get => new List<KeyValuePair<int, Alcohol>>(_alcoholInCart);
    }
    
    private List<KeyValuePair<int, int>> _quantityOfAlcoholInCart = new List<KeyValuePair<int, int>>();
    
    public List<KeyValuePair<int, int>> QuantityOfAlcoholInCart
    {
        get => new List<KeyValuePair<int, int>>(_quantityOfAlcoholInCart);
    }
    
    public int Alcohol_Id = 0;
    
    public void AddAlcoholToCart( Alcohol alcohol, int Quantity)
    {
        if ( alcohol == null)
        {
            throw new ArgumentNullException();
        }

        Alcohol_Id += 1;
        _alcoholInCart.Add(new KeyValuePair<int, Alcohol>(Alcohol_Id, alcohol));
        _quantityOfAlcoholInCart.Add(new KeyValuePair<int, int>(Alcohol_Id, Quantity));
        
        if (!alcohol.ClientsWithThisAlcoholInCart.Contains(this))
        {
            alcohol.AddClientWithAlcoholInCart(this, Quantity);
        }
    }

    public void RemoveAlcoholFromCart(Alcohol alcohol)
    {
        if (alcohol == null)
        {
            throw new ArgumentNullException();
        }

        if (AlcoholInCart.FirstOrDefault(alc => alc.Value == alcohol, new KeyValuePair<int, Alcohol>(-1, new Alcohol())).Key != -1 )
        {
            var key = AlcoholInCart.FirstOrDefault(alc => alc.Value == alcohol).Key;
            _alcoholInCart.Remove(new KeyValuePair<int, Alcohol>(key, alcohol));
            
            var index = _quantityOfAlcoholInCart.FindIndex(alc => alc.Key == key);
            _quantityOfAlcoholInCart.RemoveAt(index);

            if (alcohol.ClientsWithThisAlcoholInCart.Contains(this))
            {
                alcohol.RemoveClientWithAlcoholInCart(this);
            }
        }
        else
        {
            throw new ArgumentException("There is no such alcohol!");
        }
    }

    public void EditAlcoholInCart(Alcohol alcohol1, Alcohol alcohol2, int Quantity)
    {
        RemoveAlcoholFromCart(alcohol1);
        AddAlcoholToCart(alcohol2, Quantity);
    }
}