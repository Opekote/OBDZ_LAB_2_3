using lab_2_3.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Mappers;


    public class ShipmentMap : ClassMapping<Shipment>
        {
            public ShipmentMap()
            {
                Table("shipment");
                Id(x => x.ShipmentID, m => 
                {
                    m.Column("shipmentid");
                    m.Generator(Generators.Sequence, g => g.Params(new { sequence = "shipment_shipmentid_seq" }));
                });
                Property(x => x.TrackingNumber, m =>
                {
                    m.Column("trackingnumber");
                    m.NotNullable(true);
                    m.Unique(true);
                    m.Length(50);
                });
                ManyToOne(x => x.Sender, m =>
                {
                    m.Column("senderid");
                    m.NotNullable(true);
                    m.Class(typeof(Customer));
                });
                ManyToOne(x => x.Recipient, m =>
                {
                    m.Column("recipientid");
                    m.NotNullable(true);
                    m.Class(typeof(Customer));
                });
                Property(x => x.Weight);
                Property(x => x.Dimensions, m => m.Length(50));
                Property(x => x.ShipmentDate);
                Property(x => x.DeliveryDate);
                Property(x => x.Status, m => m.Length(50));
                Property(x => x.SenderAddress, m => m.Length(255));
                Property(x => x.RecipientAddress, m => m.Length(255));
    
                Bag(x => x.Reviews, cm =>
                {
                    cm.Key(k => k.Column("shipmentid"));
                    cm.Inverse(true);
                    cm.Cascade(Cascade.All | Cascade.DeleteOrphans); // Каскадне видалення
                }, m => m.OneToMany());
            }
        }
