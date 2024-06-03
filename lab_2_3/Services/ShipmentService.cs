using lab_2_3.Entities;
using NHibernate;

namespace lab_2_3.Services;
public class ShipmentService
{
    private readonly ISession _session;

    public ShipmentService(ISession session)
    {
        _session = session;
    }

    public void ListAll()
    {
        var shipments = _session.Query<Shipment>().ToList();
        foreach (var shipment in shipments)
        {
            Console.WriteLine($"{shipment.ShipmentID}: {shipment.TrackingNumber}");
        }
    }

    public void Create()
    {
        long senderId, recipientId;
        string input;
    do
    {
        Console.WriteLine("Enter sender ID:");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out senderId) || senderId < 0);
    
    do 
    {
        Console.WriteLine("Enter recipient ID:");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out recipientId) || recipientId < 0);
    
    
    var sender = _session.Get<Customer>(senderId);
    var recipient = _session.Get<Customer>(recipientId);
    if (sender != null && recipient != null)
    {
        string trackingNumber, dimensions, status, senderAddress, recipientAddress;
        DateTime deliveryDate;
        do
        {
            Console.WriteLine("Enter Tracking Number:");
            trackingNumber = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(trackingNumber));

        decimal weight;
        do
        {
            Console.WriteLine("Enter Weight:");
        } while (!decimal.TryParse(Console.ReadLine(), out weight) || weight < 0);

        do
        {
            Console.WriteLine("Enter Dimensions:");
            dimensions = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(dimensions));
        

        
        do
        {
            Console.WriteLine("Enter Delivery Date:");
        } while (!DateTime.TryParse(Console.ReadLine(), out deliveryDate));


        do
        {
            Console.WriteLine("Enter Status:");
            status = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(status));


        do
        {
            Console.WriteLine("Enter Sender Address:");
            senderAddress = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(senderAddress));
        

        do
        {
            Console.WriteLine("Enter Recipient Address:");
            recipientAddress = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(recipientAddress));
        

        var newShipment = new Shipment
        {
            TrackingNumber = trackingNumber,
            Sender = sender,
            Recipient = recipient,
            Weight = weight,
            Dimensions = dimensions,
            ShipmentDate = DateTime.Now,
            DeliveryDate = deliveryDate,
            Status = status,
            SenderAddress = senderAddress,
            RecipientAddress = recipientAddress
        };
        using (var transaction = _session.BeginTransaction())
        {
            try
            {
                _session.Save(newShipment);
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
        Console.WriteLine("Sender or Recipient not found.");
    }
}

public void Update()
{
    long shipmentId;
    string input;
    string trackingNumber, dimensions, status, senderAddress, recipientAddress;
    DateTime deliveryDate;
    decimal weight;

    do
    {
        Console.WriteLine("Enter sender ID:");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentId) || shipmentId < 0);
    
    var shipment = _session.Get<Shipment>(shipmentId);
    if (shipment != null)
    {
        Console.WriteLine("Select the property you want to update:");
        Console.WriteLine("1. Tracking Number");
        Console.WriteLine("2. Weight");
        Console.WriteLine("3. Dimensions");
        Console.WriteLine("4. Shipment Date");
        Console.WriteLine("5. Status");
        Console.WriteLine("6. Sender Address");
        Console.WriteLine("7. Recipient Address");
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
                        Console.WriteLine("Enter Tracking Number:");
                        trackingNumber = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(trackingNumber));
                    shipment.TrackingNumber = trackingNumber;
                    break;
                case "2":
                    do
                    {
                        Console.WriteLine("Enter new Weight:");
                    } while (!decimal.TryParse(Console.ReadLine(), out weight) || weight < 0);
                    shipment.Weight = weight;
                    break;
                case "3":
                    do
                    {
                        Console.WriteLine("Enter Dimensions:");
                        dimensions = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(dimensions));
                    shipment.Dimensions = dimensions;
                    break;
                case "4":
                    do
                    {
                        Console.WriteLine("Enter new Delivery Date:");
                    } while (!DateTime.TryParse(Console.ReadLine(), out deliveryDate));
                    shipment.ShipmentDate = deliveryDate;
                    break;
                case "5":
                    do
                    {
                        Console.WriteLine("Enter Status:");
                        status = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(status));
                    shipment.Status = status;
                    break;
                case "6":
                    do
                    {
                        Console.WriteLine("Enter Sender Address:");
                        senderAddress = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(senderAddress));
                    shipment.SenderAddress = senderAddress;
                    break;
                case "7":
                    do
                    {
                        Console.WriteLine("Enter Recipient Address:");
                        recipientAddress = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(recipientAddress));
                    shipment.RecipientAddress = recipientAddress;
                    break;
                case "0":
                    return; // Cancel update
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        } while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6" && choice != "7" && choice != "0");

        _session.Update(shipment);
        _session.Flush();
    }
    else
    {
        Console.WriteLine("Shipment not found.");
    }
}


    public void Delete()
    {
        long shipmentId;
        string input;
        do
        {
            Console.WriteLine("Enter sender ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentId) || shipmentId < 0);
        
        var shipment = _session.Get<Shipment>(shipmentId);
        if (shipment != null)
        {
            _session.Delete(shipment);
            _session.Flush();
        }
    }
}