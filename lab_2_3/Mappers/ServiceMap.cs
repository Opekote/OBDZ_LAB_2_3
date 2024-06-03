using lab_2_3.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Mappers;

public class ServiceMap : ClassMapping<Service>
{
    public ServiceMap()
    {
        Table("service");
        Id(x => x.ServiceID, m =>
        {
            m.Column("serviceid");
            m.Generator(Generators.Sequence,
                g => g.Params(new { sequence = "service_serviceid_seq", initial_value = 101 }));
        });
        Property(x => x.ServiceName, m =>
        {
            m.Column("servicename");
            m.NotNullable(true);
            m.Length(50);
        });
        Property(x => x.Description);
        Property(x => x.Price, m =>
        {
            m.Column("price");
            m.NotNullable(true);
        });
    }
}