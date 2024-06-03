using lab_2_3.Entities;
using NHibernate;

namespace lab_2_3.Services;

public class PackageService
{
    private readonly ISession _session;

    public PackageService(ISession session)
    {
        _session = session;
    }

    public void ListAll()
    {
        var packages = _session.Query<Package>().ToList();
        foreach (var package in packages)
        {
            Console.WriteLine($"{package.PackageID}: {package.PackageType}");
        }
    }

    public void Create()
    {
        string packageType, contentDescription;
        long shipmentId;
        decimal value;
        string input;

        do
        {
            Console.WriteLine("Enter Shipment ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentId) || shipmentId < 0);

        var shipment = _session.Get<Shipment>(shipmentId);
        if (shipment != null)
        {
            do
            {
                Console.WriteLine("Enter Package Type:");
                packageType = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(packageType));

            do
            {
                Console.WriteLine("Enter content description:");
                contentDescription = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(contentDescription));

            do
            {
                Console.WriteLine("Enter Value:");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input) || !decimal.TryParse(input, out value) || value < 0);

            var newPackage = new Package
            {
                Shipment = shipment,
                PackageType = packageType,
                ContentDescription = contentDescription,
                Value = value
            };

            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(newPackage);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        else
        {
            Console.WriteLine("Shipment not found.");
        }
        
        
    }
    
    

    public void Update()
    {

        string input;
        long packageId, shipmentId;
        string packageType, contentDescription;
        decimal value;
        do
        {
            Console.WriteLine("Enter Shipment ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out packageId) || packageId < 0);
        
        
        var package = _session.Get<Package>(packageId);
        if (package != null)
        {
            Console.WriteLine("Select the property you want to update:");
            Console.WriteLine("1. Package Type");
            Console.WriteLine("2. Content Description");
            Console.WriteLine("3. Value");
            Console.WriteLine("0. Cancel");

            string choice;
            do
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        do
                        {
                            Console.WriteLine("Enter Package Type:");
                            packageType = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(packageType));
                        package.PackageType = packageType;
                        break;
                    case "2":
                        do
                        {
                            Console.WriteLine("Enter content description:");
                            contentDescription = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(contentDescription));
                        package.ContentDescription = contentDescription;
                        break;
                    case "3":
                        do
                        {
                            Console.WriteLine("Enter Value:");
                            input = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(input) || !decimal.TryParse(input, out value) || value < 0);
                        package.Value = value;
                        break;
                    case "0":
                        return; // Cancel update
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != "1" && choice != "2" && choice != "3" && choice != "0");

            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(package);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        else
        {
            Console.WriteLine("Package not found.");
        }
    }

    public void Delete()
    {
        
        string input;
        long packageId;
        do
        {
            Console.WriteLine("Enter Shipment ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out packageId) || packageId < 0);
        
        var package = _session.Get<Package>(packageId);
        if (package != null)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(package);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
