using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var builder = new DbContextOptionsBuilder<ConsoleApp06.Data.PubsDbContext>();
builder.UseSqlServer("Server=localhost,1433;Database=pubs;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True");

using (var pubs = new ConsoleApp06.Data.PubsDbContext(builder.Options))
{
    var query1 = pubs.Authors
        .Where(a => a.State == "CA");
    var result1 = query1.ToList();

    var query2 = pubs.Authors
                    .Where(a => a.AuthorId == "172-32-1176")
                    .Select(a => new { a.AuthorId, a.AuthorFirstName, a.AuthorLastName });
    var result2 = query2.FirstOrDefault();

    foreach(var author in result1)
    {
        Console.WriteLine($"{author.AuthorId}: {author.AuthorFirstName} {author.AuthorLastName} ({author.State})");
    }

    Console.WriteLine($"One: {result2?.AuthorId}: {result2?.AuthorFirstName} {result2?.AuthorLastName}");

}

using (var pubs = new ConsoleApp06.Data.PubsDbContext(builder.Options))
{
    var query = pubs.Stores.Select(st => new
    {
        st.StoreId,
        st.StoreName,
        TotalSales = st.Sales.Sum(sales => sales.Title.Price * sales.Quantity)
    });
    foreach (var store in query)
    {
        Console.WriteLine($"{store.StoreId}: {store.StoreName} - Total Sales: {store.TotalSales:C}");
    }
}
