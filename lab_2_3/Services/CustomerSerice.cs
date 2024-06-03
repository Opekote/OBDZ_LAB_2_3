using System.Text.RegularExpressions;
using lab_2_3.Entities;
using NHibernate;

namespace lab_2_3.Services;

public class CustomerService
{
    private readonly ISession _session;

    public CustomerService(ISession session)
    {
        _session = session;
    }

    public void ListAll()
    {
        var customers = _session.Query<Customer>().ToList();
        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.CustomerID}: {customer.FirstName} {customer.LastName}");
        }
    }

    public void Create()
    {
        string firstName, lastName, email, phone;

        do
        {
            Console.WriteLine("Enter First Name:");
            firstName = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(firstName));

        do
        {
            Console.WriteLine("Enter Last Name:");
            lastName = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(lastName));

        do
        {
            Console.WriteLine("Enter Email:");
            email = Console.ReadLine();
        } while (!IsValidEmail(email));

        do
        {
            Console.WriteLine("Enter Phone Number:");
            phone = Console.ReadLine();
        } while (!IsValidPhoneNumber(phone));

        var newCustomer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone
        };

        using (var transaction = _session.BeginTransaction())
        {
            try
            {
                _session.Save(newCustomer);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(email);
    }

    private bool IsValidPhoneNumber(string phone)
    {
        return phone.All(char.IsDigit);
    }

    public void Update()
    {


        long customerId;
        string input, firstName, lastName, email, phone;
        
        do
        {
            Console.WriteLine("Enter Shipment ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out customerId) || customerId < 0);
        
        
        var customer = _session.Get<Customer>(customerId);
        if (customer != null)
        {
            Console.WriteLine("Select the property you want to update:");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Email");
            Console.WriteLine("4. Phone");
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
                            Console.WriteLine("Enter First Name:");
                            firstName = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(firstName));
                        customer.FirstName = firstName;
                        break;
                    case "2":
                        do
                        {
                            Console.WriteLine("Enter Last Name:");
                            lastName = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(lastName));
                        customer.LastName = lastName;
                        break;
                    case "3":
                        do
                        {
                            Console.WriteLine("Enter Email:");
                            email = Console.ReadLine();
                        } while (!IsValidEmail(email));
                        customer.Email = email;
                        break;
                    case "4":
                        do
                        {
                            Console.WriteLine("Enter Phone Number");
                            phone = Console.ReadLine();          
                        } while (!IsValidPhoneNumber(phone));    
                        customer.Phone = phone;
                        break;
                    case "0":
                        return; // Cancel update
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "0");

            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(customer);
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
            Console.WriteLine("Customer not found.");
        }
    }

    public void Delete()
    {
        long customerId;
        string input;
        
        do
        {
            Console.WriteLine("Enter customer ID:");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || !long.TryParse(input, out customerId) || customerId < 0);
        
        var customer = _session.Get<Customer>(customerId);
        if (customer != null)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(customer);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            ListAll();
            ListAllReviews();
            ListAllShipments();

        }
    }
    public void ListAllReviews()
    {
        var reviews = _session.Query<Review>().ToList();
        foreach (var review in reviews)
        {
            Console.WriteLine($"{review.ReviewID}: {review.Comment} (Customer ID: {review.Customer.CustomerID})");
        }
    }

    public void ListAllShipments()
    {
        var shipments = _session.Query<Shipment>().ToList();
        foreach (var shipment in shipments)
        {
            Console.WriteLine($"{shipment.ShipmentID}: {shipment.TrackingNumber}");
        }
    }
}
