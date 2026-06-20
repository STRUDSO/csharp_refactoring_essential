namespace DivergentChange.Test;

using NUnit.Framework;

[TestFixture]
public class CustomerServiceTest
{
    private readonly CustomerService service = new CustomerService();

    // -------------------------
    // isValidEmail tests
    // -------------------------

    [Test]
    public void IsValidEmail_shouldReturnFalse_whenEmailIsNull()
    {
        Assert.IsFalse(service.IsValidEmail(null));
    }

    [Test]
    public void IsValidEmail_shouldReturnFalse_whenEmailIsEmpty()
    {
        Assert.IsFalse(service.IsValidEmail(""));
    }

    [Test]
    public void IsValidEmail_shouldReturnFalse_whenMissingAtSymbol()
    {
        Assert.IsFalse(service.IsValidEmail("invalid.email.com"));
    }

    [Test]
    public void IsValidEmail_shouldReturnFalse_whenMissingLocalPart()
    {
        Assert.IsFalse(service.IsValidEmail("@domain.com"));
    }

    [Test]
    public void IsValidEmail_shouldReturnFalse_whenMissingDomain()
    {
        Assert.IsFalse(service.IsValidEmail("user@"));
    }

    [Test]
    public void IsValidEmail_shouldReturnTrue_whenEmailIsValid()
    {
        Assert.IsTrue(service.IsValidEmail("user.name+tag@example.com"));
    }

    [Test]
    public void IsValidEmail_shouldReturnTrue_whenSimpleValidEmail()
    {
        Assert.IsTrue(service.IsValidEmail("user@example.com"));
    }

    // -------------------------
    // formatDisplayName tests
    // -------------------------

    [Test]
    public void FormatDisplayName_shouldTrimAndUppercaseLastName()
    {
        string result = service.FormatDisplayName.FormatDisplayName(" John ", " smith ");
        Assert.AreEqual("John SMITH", result);
    }

    [Test]
    public void FormatDisplayName_shouldHandleEmptyStrings()
    {
        string result = service.FormatDisplayName.FormatDisplayName("", "");
        Assert.AreEqual(" ", result);
    }

    [Test]
    public void FormatDisplayName_shouldHandleSingleCharacterNames()
    {
        string result = service.FormatDisplayName.FormatDisplayName("A", "b");
        Assert.AreEqual("A B", result);
    }

    // -------------------------
    // calculateLoyaltyPoints tests
    // -------------------------

    [Test]
    public void CalculateLoyaltyPoints_shouldReturnZero_whenNoPurchases()
    {
        Assert.AreEqual(0, service.LoyaltyCalculator.CalculateLoyaltyPoints(0));
    }

    [Test]
    public void CalculateLoyaltyPoints_shouldCalculateCorrectly_forPositiveValues()
    {
        Assert.AreEqual(50, service.LoyaltyCalculator.CalculateLoyaltyPoints(5));
    }

    [Test]
    public void CalculateLoyaltyPoints_shouldHandleLargeNumbers()
    {
        Assert.AreEqual(100_000, service.LoyaltyCalculator.CalculateLoyaltyPoints(10_000));
    }

    [Test]
    public void CalculateLoyaltyPoints_shouldAllowNegativeValues_butStillMultiply()
    {
        Assert.AreEqual(-50, service.LoyaltyCalculator.CalculateLoyaltyPoints(-5));
    }

    // -------------------------
    // determineAccountStatus tests
    // -------------------------

    [Test]
    public void DetermineAccountStatus_shouldReturnInactive_whenDaysOver365()
    {
        Assert.AreEqual("INACTIVE", service.AccountStatusCalculator.DetermineAccountStatus(366));
    }

    [Test]
    public void DetermineAccountStatus_shouldReturnDormant_whenBetween31And365()
    {
        Assert.AreEqual("DORMANT", service.AccountStatusCalculator.DetermineAccountStatus(100));
    }

    [Test]
    public void DetermineAccountStatus_shouldReturnActive_when30DaysOrLess()
    {
        Assert.AreEqual("ACTIVE", service.AccountStatusCalculator.DetermineAccountStatus(30));
        Assert.AreEqual("ACTIVE", service.AccountStatusCalculator.DetermineAccountStatus(0));
    }

    [Test]
    public void DetermineAccountStatus_shouldTreatNegativeDaysAsActive()
    {
        Assert.AreEqual("ACTIVE", service.AccountStatusCalculator.DetermineAccountStatus(-10));
    }
}