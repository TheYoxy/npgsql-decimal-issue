// See https://aka.ms/new-console-template for more information

using Issue;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Starting db");
var db = new AppDbContext();
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();
if (await db.Set<MainData>().AnyAsync())
{
    Console.WriteLine("Removing existing data");
    await db.Set<MainData>().ExecuteDeleteAsync();
}

Console.WriteLine("Inserting data");
var data = Seeding.GetMainData();
foreach (var mainData in data)
{
    Console.WriteLine(mainData);
}

db.Set<MainData>().AddRange(data);
await db.SaveChangesAsync();

// using another context to avoid caching or any other side effect
var ctx = new AppDbContext();
Console.WriteLine("Calculating sum");
//
var result = await ctx.Set<MainData>()
    .Select(mainData => mainData.DependentData.Sum(dependentData =>
        dependentData.Amount * dependentData.Quantity * (1 + dependentData.Divider / 100m)))
    .ToListAsync();
var sum = result.Sum();
Console.WriteLine($"Sum (client side|linq): {sum}");

var sum2 = 0m;
await foreach (var amount in ctx.Set<MainData>()
                   .Select(mainData => mainData.DependentData.Sum(dependentData =>
                       dependentData.Amount * dependentData.Quantity * (1 + dependentData.Divider / 100m)))
                   .AsAsyncEnumerable())
{
    sum2 += amount;
}

Console.WriteLine($"Sum (client side|async enumerable): {sum2}");

// throw OverflowException
var response = await ctx.Set<MainData>()
    .SumAsync(mainData => mainData.DependentData.Sum(dependentData =>
        dependentData.Amount * dependentData.Quantity * (1 + dependentData.Divider / 100m)));
Console.WriteLine($"Sum: {response}");
