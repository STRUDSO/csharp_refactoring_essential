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

public class DisplayName
{
    private readonly string _firstName;
    private readonly string _lastName;

    public DisplayName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    public string FormatDisplayName()
    {
        return _firstName.Trim() + " " + _lastName.Trim().ToUpper();
    }
}

public class NumberOf
{
    public NumberOf(int numberOfPurchases)
    {
        NumberOfPurchases = numberOfPurchases;
    }

    public int NumberOfPurchases { get; }

    public int CalculateLoyaltyPoints()
    {
        var numberOfPurchases = NumberOfPurchases;
        return numberOfPurchases * 10;
    }
}

public class LoyaltyCalculator
{
}

public class AccountStatusCalculator
{
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

public class CustomerService
{
    private readonly AccountStatusCalculator _accountStatusCalculator = new();

    public AccountStatusCalculator AccountStatusCalculator => _accountStatusCalculator;

    public bool IsValidEmail(string email)
    {
        return EmailValueObject.Parse(email) != null;
    }
}