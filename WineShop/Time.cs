namespace WineShop;

[Serializable]
public class Time
{
    private int _hours;

    public int hours
    {
        get => _hours;
        set
        {
            if (value > 23 || value < 0)
            {
                throw new ArgumentException("Invalid time specified");
            }

            _hours = value;
        }
    }

    private int _minutes;

    public int minutes
    {
        get => _minutes;
        set
        {
            if(value > 59 || value<0 )
            {
                throw new ArgumentException("Invalid time specified");
            }

            _minutes = value;
        }
    }

    private int _seconds;

    public int seconds
    {
        get => _seconds;
        set
        {
            if( value > 59 || value <0)
            {
                throw new ArgumentException("Invalid time specified");
            }

            _seconds = value;
        }
    }

    public Time(int h, int m, int s)
    {
        hours = h;
        minutes = m;
        seconds = s;
        
    }

    
    public override string ToString()
    {  
        return String.Format(
            "{0:00}:{1:00}:{2:00}",
            hours, minutes, seconds);
    }
}