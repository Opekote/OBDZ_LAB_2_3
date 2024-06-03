using lab_2_3.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Mappers;

public class ReviewMap : ClassMapping<Review>
{
    public ReviewMap()
    {
        Table("review");
        Id(x => x.ReviewID, m => 
        {
            m.Column("reviewid");
            m.Generator(Generators.Sequence, g => g.Params(new { sequence = "review_reviewid_seq" }));
        });
        ManyToOne(x => x.Customer, m =>
        {
            m.Column("customerid");
            m.NotNullable(true);
            m.Class(typeof(Customer));
        });
        ManyToOne(x => x.Shipment, m =>
        {
            m.Column("shipmentid");
            m.NotNullable(true);
            m.Class(typeof(Shipment));
            m.Cascade(Cascade.All); // Ensure cascading delete
        });
        Property(x => x.Rating, m => m.NotNullable(true));
        Property(x => x.Comment, m => m.Column("comment"));
        Property(x => x.ReviewDate, m => m.NotNullable(true));
    }
}