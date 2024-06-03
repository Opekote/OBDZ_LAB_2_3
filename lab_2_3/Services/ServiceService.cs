using lab_2_3.Entities;
using NHibernate;

namespace lab_2_3.Services;

public class ServiceService
{
    private readonly ISession _session;

    public ServiceService(ISession session)
    {
        _session = session;
    }

    public void ListAll()
    {
        var services = _session.Query<Service>().ToList();
        foreach (var service in services)
        {
            Console.WriteLine($"{service.ServiceID}: {service.ServiceName}");
        }
    }

    public void Create()
{
    string serviceName;
    decimal price;
    string description;
    do
    {
        Console.WriteLine("Enter Service Name:");
        serviceName = Console.ReadLine();
    } while (string.IsNullOrEmpty(serviceName));

    do
    {
        Console.WriteLine("Enter content description:");
        description = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(description));

    
    do
    {
        Console.WriteLine("Enter Price:");
    } while (!decimal.TryParse(Console.ReadLine(), out price));

    var newService = new Service
    {
        ServiceName = serviceName,
        Description = description,
        Price = price
    };
    using (var transaction = _session.BeginTransaction())
    {
        try
        {
            _session.Save(newService);
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}

public void Update()
{
    
    
    long serviceId;
    string input, serviceName, description;
    decimal price;
    
    do
    {
        Console.WriteLine("Enter service ID:");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out serviceId) || serviceId < 0);
    
    
    var service = _session.Get<Service>(serviceId);
    if (service != null)
    {
        Console.WriteLine("Select the property you want to update:");
        Console.WriteLine("1. Service Name");
        Console.WriteLine("2. Description");
        Console.WriteLine("3. Price");
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
                        Console.WriteLine("Enter Service Name:");
                        serviceName = Console.ReadLine();
                    } while (string.IsNullOrEmpty(serviceName));
                    service.ServiceName = serviceName;
                    break;
                case "2":
                    do
                    {
                        Console.WriteLine("Enter content description:");
                        description = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(description));
                    break;
                case "3":
                    do
                    {
                        Console.WriteLine("Enter new Price:");
                    } while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0);
                    service.Price = price;
                    break;
                case "0":
                    return; // Cancel update
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        } while (choice != "1" && choice != "2" && choice != "3" && choice != "0");

        _session.Update(service);
        _session.Flush();
    }
    else
    {
        Console.WriteLine("Service not found.");
    }
}



    public void Delete()
    {
        
        long serviceId;
        string input;
        do
        {
            Console.WriteLine("Enter service ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out serviceId) || serviceId < 0);
        
        var service = _session.Get<Service>(serviceId);
        if (service != null)
        {
            _session.Delete(service);
            _session.Flush();
        }
    }
}
