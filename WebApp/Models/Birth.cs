namespace WebApp.Models;

public class Birth
{
    public DateTime? BirthDate { get; set; }  
    public string Name { get; set; }  

    
    public bool IsValid()
    {
       
        return BirthDate != null && BirthDate < DateTime.Now && !string.IsNullOrEmpty(Name);
    }

   
    public string GetAge()
    {
        var age = CalculateAge(BirthDate.Value);
        return $"Cześć {Name}, masz {age.Years} lat, {age.Months} miesięcy oraz {age.Days} dni.";
    }

    
    public (int Years, int Months, int Days) CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        int years = today.Year - birthDate.Year;
        int months = today.Month - birthDate.Month;
        int days = today.Day - birthDate.Day;

   
        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(today.Year, today.Month);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }
        
        return (years, months, days);
    }
}