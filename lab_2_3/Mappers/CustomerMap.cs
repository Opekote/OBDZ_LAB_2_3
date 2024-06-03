using lab_2_3.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Mappers;

public class CustomerMap : ClassMapping<Customer>
{
    public CustomerMap()
    {
        Table("customer");
        Id(x => x.CustomerID, m => 
        {
            m.Column("customerid");
            m.Generator(Generators.Sequence,
                g => g.Params(new { sequence = "customer_customerid_seq", initial_value = 101 }));
            
            
        });
        Property(x => x.FirstName, m => m.NotNullable(true));
        Property(x => x.LastName, m => m.NotNullable(true));
        Property(x => x.Email);
        Property(x => x.Phone);

        Bag(x => x.Reviews, m =>
        {
            m.Cascade(Cascade.All | Cascade.DeleteOrphans); // Каскадне видалення
            m.Inverse(true);
            m.Key(k => k.Column("customerid"));
        }, r => r.OneToMany());
    }
}

