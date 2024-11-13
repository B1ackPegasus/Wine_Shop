namespace WineShop;

public class MyDate
{
    private int _month;

    public int month
    {
        get => _month;
        set
        {
            if(value > 12 || value<0 )
            {
                throw new ArgumentException("Invalid month specified");
            }

            _month = value;
        }
    }

    private int _year;

    public int year
    {
        get => _year;
        set
        {
            if(value <0)
            {
                throw new ArgumentException("Invalid year specified");
            }

            _year = value;
        }
    }
    
    private int _days;

    public int days
    {
        get => _days;
        set
        {
            int daysInMonth;
            if (month == 2) 
            {
                bool isLeapYear = (year%4 == 0 && year%100 != 0) || (year%400 == 0);
                daysInMonth = isLeapYear ? 29 : 28;
            }
            else if (month==4 || month==6 || month==9 || month==11)
            {
                daysInMonth = 30;
            }
            else 
            {
                daysInMonth = 31;
            }

            _days = value;
        }
    }

    public MyDate(int d, int m, int y)
    {
        days = d;
        month = m;
        year = y;
        
    }
    
    public bool CompareTo(MyDate other)
    {
        if (year != other.year)
            return year > other.year;
        if (month != other.month)
            return month > other.month;
        return days > other.days;
    }

    public MyDate()
    {
    }
}