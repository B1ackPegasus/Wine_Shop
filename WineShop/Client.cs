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
}