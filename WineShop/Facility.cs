namespace WineShop;

public abstract class Facility
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

    private Address _address;

    public Address Address
    {
        get => _address;
        set
        {
            _address = value;
        }
    }
}