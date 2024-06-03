using lab_2_3.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Mappers;

public class TransactionMap : ClassMapping<Transaction>
{
    public TransactionMap()
    {
        Table("transaction");
        Id(x => x.TransactionID, m =>
        {
            m.Column("transactionid");
            m.Generator(Generators.Sequence,
                g => g.Params(new { sequence = "transaction_transactionid_seq", initial_value = 101 }));
        });
        ManyToOne(x => x.Shipment, m =>
        {
            m.Column("shipmentid");
            m.NotNullable(true);
            m.Class(typeof(Shipment));
        });
        ManyToOne(x => x.Service, m =>
        {
            m.Column("serviceid");
            m.NotNullable(true);
            m.Class(typeof(Service));
        });
        Property(x => x.TransactionDate, m => m.NotNullable(true));
        Property(x => x.Amount, m => m.NotNullable(true));
    }
}