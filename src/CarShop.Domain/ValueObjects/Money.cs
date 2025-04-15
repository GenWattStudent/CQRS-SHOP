using System.Globalization;

namespace CarShop.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }

    public static Money Zero => new(0);

    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static Money Create(decimal amount)
    {
        return new Money(amount);
    }

    public static Money From(decimal amount)
    {
        return new Money(amount);
    }

    public Money Add(Money money)
    {
        return new Money(Amount + money.Amount);
    }

    public Money Subtract(Money money)
    {
        return new Money(Amount - money.Amount);
    }

    public bool IsGreaterThan(Money money)
    {
        return Amount > money.Amount;
    }

    public bool IsLessThan(Money money)
    {
        return !IsGreaterThan(money) && !Equals(money);
    }

    public override string ToString()
    {
        return $"{Amount.ToString("F2", CultureInfo.InvariantCulture)}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}