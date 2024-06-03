using lab_2_3.Entities;
using NHibernate;

namespace lab_2_3.Services;


public class ReviewService
{
    private readonly ISession _session;

    public ReviewService(ISession session)
    {
        _session = session;
    }

    public void ListAll()
    {
        var reviews = _session.Query<Review>().ToList();
        foreach (var review in reviews)
        {
            Console.WriteLine($"{review.ReviewID}: {review.Rating}");
        }
    }

    public void Create(){
        long customerId, shipmentId;
        string input;

        do
        {
            Console.WriteLine("Enter Customer ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out customerId) || customerId < 0);
    
        do 
        {
            Console.WriteLine("Enter Shipment ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out shipmentId) || shipmentId < 0);
    
        var customer = _session.Get<Customer>(customerId);
        var shipment = _session.Get<Shipment>(shipmentId);
        if (customer != null && shipment != null)
        {
            int rating;
            string comment;
            do
            {
                Console.WriteLine("Enter Rating (1-5):");
            } while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5);

            do
            {
                Console.WriteLine("Enter comment:");
                comment = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(comment));

            var newReview = new Review
            {
                Customer = customer,
                Shipment = shipment,
                Rating = rating,
                Comment = comment,
                ReviewDate = DateTime.Now
            };
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(newReview);
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
            Console.WriteLine("Customer or Shipment not found.");
        }
    }

public void Update()
{
    long reviewId;
    string input, comment;
    do
    {
        Console.WriteLine("Enter review ID:");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out reviewId) || reviewId < 0);
    
    
    
    var review = _session.Get<Review>(reviewId);
    if (review != null)
    {
        Console.WriteLine("Select the property you want to update:");
        Console.WriteLine("1. Rating");
        Console.WriteLine("2. Comment");
        Console.WriteLine("0. Cancel");

        string choice;
        do
        {
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    int rating;
                    do
                    {
                        Console.WriteLine("Enter new Rating (1-5):");
                    } while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5);
                    review.Rating = rating;
                    break;
                case "2":
                    do
                    {
                        Console.WriteLine("Enter comment:");
                        comment = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(comment));
                    review.Comment = comment;
                    break;
                case "0":
                    return; // Cancel update
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        } while (choice != "1" && choice != "2" && choice != "0");

        _session.Update(review);
        _session.Flush();
    }
    else
    {
        Console.WriteLine("Review not found.");
    }
}



    public void Delete()
    {
        long reviewId;
        string input;
        do
        {
            Console.WriteLine("Enter review ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out reviewId) || reviewId < 0);
        
        var review = _session.Get<Review>(reviewId);
        if (review != null)
        {
            _session.Delete(review);
            _session.Flush();
        }
    }
}
