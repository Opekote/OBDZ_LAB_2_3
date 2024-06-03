using FluentMigrator;

namespace lab_2_3.Migrations
{
    [Migration(20240603)]
    public class CreateInitialTables : Migration
    {
        public override void Up()
        {
            Create.Sequence("customer_customerid_seq")
                .StartWith(101)
                .IncrementBy(1)
                .MinValue(101)
                .Cache(1);

            Create.Sequence("shipment_shipmentid_seq")
                .StartWith(101)
                .IncrementBy(1)
                .MinValue(101)
                .Cache(1);

            Create.Sequence("package_packageid_seq")
                .StartWith(1)
                .IncrementBy(1)
                .MinValue(1)
                .Cache(1);

            Create.Sequence("review_reviewid_seq")
                .StartWith(1)
                .IncrementBy(1)
                .MinValue(1)
                .Cache(1);

            Create.Sequence("service_serviceid_seq")
                .StartWith(1)
                .IncrementBy(1)
                .MinValue(1)
                .Cache(1);

            Create.Sequence("transaction_transactionid_seq")
                .StartWith(1)
                .IncrementBy(1)
                .MinValue(1)
                .Cache(1);

            Create.Table("customer")
                .WithColumn("customerid").AsInt64().PrimaryKey().Identity()
                .WithColumn("firstname").AsString(50).NotNullable()
                .WithColumn("lastname").AsString(50).NotNullable()
                .WithColumn("email").AsString(100).Nullable()
                .WithColumn("phone").AsString(20).Nullable();

            Create.Table("shipment")
                .WithColumn("shipmentid").AsInt64().PrimaryKey().Identity()
                .WithColumn("trackingnumber").AsString(50).NotNullable().Unique()
                .WithColumn("senderid").AsInt64().NotNullable()
                .WithColumn("recipientid").AsInt64().NotNullable()
                .WithColumn("weight").AsDecimal().Nullable()
                .WithColumn("dimensions").AsString(50).Nullable()
                .WithColumn("shipmentdate").AsDateTime().Nullable()
                .WithColumn("deliverydate").AsDateTime().Nullable()
                .WithColumn("status").AsString(50).Nullable()
                .WithColumn("senderaddress").AsString(255).Nullable()
                .WithColumn("recipientaddress").AsString(255).Nullable();

            Create.Table("package")
                .WithColumn("packageid").AsInt64().PrimaryKey().Identity()
                .WithColumn("shipmentid").AsInt64().NotNullable()
                .WithColumn("packagetype").AsString(50).Nullable()
                .WithColumn("contentdescription").AsString().Nullable()
                .WithColumn("value").AsDecimal().Nullable();

            Create.Table("review")
                .WithColumn("reviewid").AsInt64().PrimaryKey().Identity()
                .WithColumn("customerid").AsInt64().NotNullable()
                .WithColumn("shipmentid").AsInt64().NotNullable()
                .WithColumn("rating").AsInt32().NotNullable()
                .WithColumn("comment").AsString().Nullable()
                .WithColumn("reviewdate").AsDateTime().NotNullable();

            Create.Table("service")
                .WithColumn("serviceid").AsInt64().PrimaryKey().Identity()
                .WithColumn("servicename").AsString(50).NotNullable()
                .WithColumn("description").AsString().Nullable()
                .WithColumn("price").AsDecimal().NotNullable();

            Create.Table("transaction")
                .WithColumn("transactionid").AsInt64().PrimaryKey().Identity()
                .WithColumn("shipmentid").AsInt64().NotNullable()
                .WithColumn("serviceid").AsInt64().NotNullable()
                .WithColumn("transactiondate").AsDateTime().NotNullable()
                .WithColumn("amount").AsDecimal().NotNullable();

            // Add foreign keys with cascade delete
            Create.ForeignKey("fk_review_customer")
                .FromTable("review").ForeignColumn("customerid")
                .ToTable("customer").PrimaryColumn("customerid")
                .OnDelete(System.Data.Rule.Cascade);

            Create.ForeignKey("fk_review_shipment")
                .FromTable("review").ForeignColumn("shipmentid")
                .ToTable("shipment").PrimaryColumn("shipmentid")
                .OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey("fk_review_customer").OnTable("review");
            Delete.ForeignKey("fk_review_shipment").OnTable("review");

            Delete.Table("transaction");
            Delete.Table("service");
            Delete.Table("review");
            Delete.Table("package");
            Delete.Table("shipment");
            Delete.Table("customer");

            Delete.Sequence("customer_customerid_seq");
            Delete.Sequence("shipment_shipmentid_seq");
            Delete.Sequence("package_packageid_seq");
            Delete.Sequence("review_reviewid_seq");
            Delete.Sequence("service_serviceid_seq");
            Delete.Sequence("transaction_transactionid_seq");
        }
    }
}
