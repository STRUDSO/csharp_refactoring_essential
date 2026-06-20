namespace DivergentChange;

using System;
using System.Text.RegularExpressions;

public class EmailValueObject
{
    public static EmailValueObject? Parse(string email)
    {
        var isValidEmail = IsValidEmail(email);
        if (isValidEmail)
            return new EmailValueObject();
        return null;
    }

    private static bool IsValidEmail(string email)
    {
        if (email == null)
        {
            return false;
        }

        return Regex.IsMatch(
            email,
            "^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+$");
    }
}

public class FormatDisplay
{
    public string FormatDisplayName(string firstName, string lastName)
    {
        return firstName.Trim() + " " + lastName.Trim().ToUpper();
    }
}

public class CustomerService
{
    private readonly FormatDisplay _formatDisplay = new FormatDisplay();

    public FormatDisplay FormatDisplayName1 => _formatDisplay;

    public bool IsValidEmail(string email)
    {
        return EmailValueObject.Parse(email) != null;
    }

    public int CalculateLoyaltyPoints(int numberOfPurchases)
    {
        return numberOfPurchases * 10;
    }

    public string DetermineAccountStatus(int daysSinceLastLogin)
    {
        if (daysSinceLastLogin > 365)
        {
            return "INACTIVE";
        }
        else if (daysSinceLastLogin > 30)
        {
            return "DORMANT";
        }

        return "ACTIVE";
    }
}