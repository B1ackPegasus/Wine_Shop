namespace WineShop;

[Serializable]
public class Address
{
    private string _street;

    public string Street
    {
        get => _street;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid street.");
            }

            _street = value;
        }
    }

    private int _streetNumber;

    public int StreetNumber
    {
        get => _streetNumber;
        set
        {
            if (value<=0)
            {
                throw new ArgumentException("Invalid street number.");
            }

            _streetNumber = value;
        }
    }

    private int _apartmentNumber;

    public int ApartmentNumber
    {
        get => _apartmentNumber;
        set
        {
            if (value<=0)
            {
                throw new ArgumentException("Invalid apartment number.");
            }

            _apartmentNumber = value;
        }
    }

    private string _zipCode;

    public string ZipCode
    {
        get => _zipCode;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid zip code.");
            }

            _zipCode = value;
        }
    }

    public Address(string street, int streetNumber, int apartmentNumber, string zipCode)
    {
        Street = street;
        StreetNumber = streetNumber;
        ApartmentNumber = apartmentNumber;
        ZipCode = zipCode;
    }
}