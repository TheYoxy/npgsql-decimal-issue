using Bogus;

namespace Issue;

public static class Seeding
{
    private const int Seed = 123;

    public static List<MainData> GetMainData()
    {
        var dependent = new Faker<DependentData>().UseSeed(Seed)
            .RuleFor(x => x.Amount, f => Math.Round(f.Random.Decimal(1, 1000), 2))
            .RuleFor(x => x.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(x => x.Divider, f => f.Random.Number(1, 100));
        var main = new Faker<MainData>().UseSeed(Seed)
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.DependentData, f => dependent.Generate(f.Random.Number(1, 10)));
        return main.GenerateBetween(1, 500);
    }
}