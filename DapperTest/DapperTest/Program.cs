using DapperTest;

OwnerRepo repo = new();
var owners = repo.GetAll();
foreach (Owner o in owners)
{
    Console.WriteLine(o.ToString());
}