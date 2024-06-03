using FluentMigrator.Runner;
using lab_2_3.Migrations;
using lab_2_3.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lab_2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateMigrationServices();
            using (var scope = serviceProvider.CreateScope())
            {
                Console.WriteLine("Do you want to update the database, rollback, or do nothing? (update/rollback/nothing)");
                var choice = Console.ReadLine();

                if (choice == "update")
                {
                    UpdateDatabase(scope.ServiceProvider);
                }
                else if (choice == "rollback")
                {
                    Console.WriteLine("Enter the migration version to rollback to:");
                    var version = long.Parse(Console.ReadLine());
                    RollbackDatabase(scope.ServiceProvider, version);
                }
                else if (choice == "nothing")
                {
                    Console.WriteLine("No database actions will be performed.");
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                    return;
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var customerService = new CustomerService(session);
                var shipmentService = new ShipmentService(session);
                var packageService = new PackageService(session);
                var reviewService = new ReviewService(session);
                var serviceService = new ServiceService(session);
                var transactionService = new TransactionService(session);

                string mainOption;
                do
                {
                    Console.WriteLine("Select a service category:");
                    Console.WriteLine("1. Customer");
                    Console.WriteLine("2. Shipment");
                    Console.WriteLine("3. Package");
                    Console.WriteLine("4. Review");
                    Console.WriteLine("5. Service");
                    Console.WriteLine("6. Transaction");
                    Console.WriteLine("0. Exit");

                    mainOption = Console.ReadLine();

                    switch (mainOption)
                    {
                        case "1":
                            CustomerMenu(customerService);
                            break;
                        case "2":
                            ShipmentMenu(shipmentService);
                            break;
                        case "3":
                            PackageMenu(packageService);
                            break;
                        case "4":
                            ReviewMenu(reviewService);
                            break;
                        case "5":
                            ServiceMenu(serviceService);
                            break;
                        case "6":
                            TransactionMenu(transactionService);
                            break;
                        case "0":
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }

                    Console.WriteLine();

                } while (mainOption != "0");

                transaction.Commit();
            }
        }

        private static void CustomerMenu(CustomerService customerService)
        {
            string option;
            do
            {
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. List all Customers");
                Console.WriteLine("2. Create a new Customer");
                Console.WriteLine("3. Update an existing Customer");
                Console.WriteLine("4. Delete a Customer");
                Console.WriteLine("0. Back to main menu");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        customerService.ListAll();
                        break;
                    case "2":
                        customerService.Create();
                        break;
                    case "3":
                        customerService.Update();
                        break;
                    case "4":
                        customerService.Delete();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (option != "0");
        }

        private static void ShipmentMenu(ShipmentService shipmentService)
        {
            string option;
            do
            {
                Console.WriteLine("Shipment Menu:");
                Console.WriteLine("1. List all Shipments");
                Console.WriteLine("2. Create a new Shipment");
                Console.WriteLine("3. Update an existing Shipment");
                Console.WriteLine("4. Delete a Shipment");
                Console.WriteLine("0. Back to main menu");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        shipmentService.ListAll();
                        break;
                    case "2":
                        shipmentService.Create();
                        break;
                    case "3":
                        shipmentService.Update();
                        break;
                    case "4":
                        shipmentService.Delete();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (option != "0");
        }
        
        private static void PackageMenu(PackageService packageService)
        {
            string option;
            do
            {
                Console.WriteLine("Package Menu:");
                Console.WriteLine("1. List all Packages");
                Console.WriteLine("2. Create a new Package");
                Console.WriteLine("3. Update an existing Package");
                Console.WriteLine("4. Delete a Package");
                Console.WriteLine("0. Back to main menu");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        packageService.ListAll();
                        break;
                    case "2":
                        packageService.Create();
                        break;
                    case "3":
                        packageService.Update();
                        break;
                    case "4":
                        packageService.Delete();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (option != "0");
        }

        private static void ReviewMenu(ReviewService reviewService)
        {
            string option;
            do
            {
                Console.WriteLine("Review Menu:");
                Console.WriteLine("1. List all Reviews");
                Console.WriteLine("2. Create a new Review");
                Console.WriteLine("3. Update an existing Review");
                Console.WriteLine("4. Delete a Review");
                Console.WriteLine("0. Back to main menu");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        reviewService.ListAll();
                        break;
                    case "2":
                        reviewService.Create();
                        break;
                    case "3":
                        Console.WriteLine("Enter ReviewID to update:");
                        reviewService.Update();
                        break;
                    case "4":
                        reviewService.Delete();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (option != "0");
        }

        private static void ServiceMenu(ServiceService serviceService)
        {
            string option;
            do
            {
                Console.WriteLine("Service Menu:");
                Console.WriteLine("1. List all Services");
                Console.WriteLine("2. Create a new Service");
                Console.WriteLine("3. Update an existing Service");
                Console.WriteLine("4. Delete a Service");
                Console.WriteLine("0. Back to main menu");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        serviceService.ListAll();
                        break;
                    case "2":
                        serviceService.Create();
                        break;
                    case "3":
                        serviceService.Update();
                        break;
                    case "4":
                        serviceService.Delete();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (option != "0");
        }

        private static void TransactionMenu(TransactionService transactionService)
        {
            string option;
            do
            {
                Console.WriteLine("Transaction Menu:");
                Console.WriteLine("1. List all Transactions");
                Console.WriteLine("2. Create a new Transaction");
                Console.WriteLine("3. Update an existing Transaction");
                Console.WriteLine("4. Delete a Transaction");
                Console.WriteLine("0. Back to main menu");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        transactionService.ListAll();
                        break;
                    case "2":
                        transactionService.Create();
                        break;
                    case "3":
                        transactionService.Update();
                        break;
                    case "4":
                        transactionService.Delete();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (option != "0");
        }

        private static IServiceProvider CreateMigrationServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString("Server=localhost;Port=5432;Database=post_service_lb_three;User Id=postgres;Password=password;")
                    .ScanIn(typeof(CreateInitialTables).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        private static void RollbackDatabase(IServiceProvider serviceProvider, long version)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateDown(version);
        }
    }
}
