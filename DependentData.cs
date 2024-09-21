namespace Issue;

public sealed class DependentData
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }

    /// <summary>
    /// Percentage on a base of 100
    /// </summary>
    public int Divider { get; set; }

    public MainData MainData { get; set; }
    public Guid MainDataId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Amount: {Amount}, Quantity: {Quantity}, Divider: {Divider}";
    }
}