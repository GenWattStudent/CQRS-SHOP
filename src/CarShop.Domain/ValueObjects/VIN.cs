using System.Text.RegularExpressions;

namespace CarShop.Domain.ValueObjects;

public class VIN : ValueObject
{
    public string Value { get; }

    public VIN() { }

    // Standard VIN format is 17 characters
    private static readonly Regex VinRegex = new(@"^[A-HJ-NPR-Z0-9]{17}$", RegexOptions.Compiled);

    private VIN(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("VIN cannot be empty", nameof(value));

        if (!VinRegex.IsMatch(value))
            throw new ArgumentException("VIN must be exactly 17 characters and contain valid alphanumeric characters (excluding I, O, and Q)", nameof(value));

        Value = value;
    }

    public static VIN Create(string vin)
    {
        return new VIN(vin);
    }

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}