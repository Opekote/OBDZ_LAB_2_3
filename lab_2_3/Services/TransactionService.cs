using lab_2_3.Entities;
using NHibernate;

namespace lab_2_3.Services;


public class TransactionService
{
    private readonly ISession _session;

    public TransactionService(ISession session)
    {
        _session = session;
    }

    public void ListAll()
    {
        var transactions = _session.Query<Transaction>().ToList();
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"{transaction.TransactionID}: {transaction.Amount}");
        }
    }

    public void Create()
    {
        long shipmentId, serviceId;
        string input;
        do
        {
            Console.WriteLine("Enter shipment ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentId) || shipmentId < 0);
    
        do 
        {
            Console.WriteLine("Enter service ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out serviceId) || serviceId < 0);
        
        var shipment = _session.Get<Shipment>(shipmentId);
        var service = _session.Get<Service>(serviceId);
        
        if (shipment != null && service != null)
        {
            decimal amount;
            do
            {
                Console.WriteLine("Enter Amount:");
            } while (!decimal.TryParse(Console.ReadLine(), out amount));

            var newTransaction = new Transaction
            {
                Shipment = shipment,
                Service = service,
                TransactionDate = DateTime.Now,
                Amount = amount
            };
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(newTransaction);
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

    public void Update()
    {
        long transactionId;
        string input;
        do
        {
            Console.WriteLine("Enter transaction ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out transactionId) || transactionId < 0);
        
        var transaction = _session.Get<Transaction>(transactionId);
        if (transaction != null)
        {
            Console.WriteLine("Select the property you want to update:");
            Console.WriteLine("1. Amount");
            Console.WriteLine("0. Cancel");

            string choice;
            do
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        decimal amount;
                        do
                        {
                            Console.WriteLine("Enter new Amount:");
                        } while (!decimal.TryParse(Console.ReadLine(), out amount));
                        transaction.Amount = amount;
                        break;
                    case "0":
                        return; // Cancel update
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != "1" && choice != "0");

            _session.Update(transaction);
            _session.Flush();
        }
        else
        {
            Console.WriteLine("Transaction not found.");
        }
    }


    public void Delete()
    {
        long transactionId;
        string input;
        do
        {
            Console.WriteLine("Enter transaction ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out transactionId) || transactionId < 0);
        
        var transaction = _session.Get<Transaction>(transactionId);
        if (transaction != null)
        {
            _session.Delete(transaction);
            _session.Flush();
        }
    }
}
