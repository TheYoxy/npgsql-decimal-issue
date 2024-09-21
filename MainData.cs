namespace Issue;

public sealed class MainData
{
    public Guid Id { get; set; }
    public ICollection<DependentData> DependentData { get; set; } = new HashSet<DependentData>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"DependentData: {string.Join("\n", DependentData.Select(d => d.ToString()))}";
    }
}