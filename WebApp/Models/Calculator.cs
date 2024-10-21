using WebApp.Controllers;

namespace WebApp.Models;

public class Calculator
{
    
    public Operators? Operator { get; set; }  
    public double? X { get; set; }  
    public double? Y { get; set; }  

    
    public String Op
    {
        get
        {
            switch (Operator)
            {
                case Operators.Add:
                    return "+";
                case Operators.Subtract:
                    return "-";
                case Operators.Multiply:
                    return "*";
                case Operators.Divide:
                    return "/";
                default:
                    return "";
            }
        }
    }

    
    public bool IsValid()
    {
        
        return Operator != null && X != null && Y != null && (Operator != Operators.Divide || Y != 0);
    }

    
    public double Calculate()
    {
        switch (Operator)
        {
            case Operators.Add:
                return (double)(X + Y);
            case Operators.Subtract:
                return (double)(X - Y);
            case Operators.Multiply:
                return (double)(X * Y);
            case Operators.Divide:
                return (double)(X / Y);  
            default:
                return double.NaN;  
        }
    }
}

public enum Operators
{
    Add,        
    Subtract,   
    Multiply,   
    Divide      
}