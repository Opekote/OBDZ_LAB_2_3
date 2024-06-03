using lab_2_3.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Mappers;

public class PackageMap : ClassMapping<Package>
{
    public PackageMap()
    {
        Table("package");
        Id(x => x.PackageID, m =>
        {
            m.Column("packageid");
            m.Generator(Generators.Sequence,
                g => g.Params(new { sequence = "package_packageid_seq", initial_value = 101 }));
        });
        ManyToOne(x => x.Shipment, m =>
        {
            m.Column("shipmentid");
            m.NotNullable(true);
            m.Class(typeof(Shipment));
        });
        Property(x => x.PackageType, m => m.Length(50));
        Property(x => x.ContentDescription);
        Property(x => x.Value);
    }
}