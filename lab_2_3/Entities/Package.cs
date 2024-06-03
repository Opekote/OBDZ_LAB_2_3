using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace lab_2_3.Entities
{
    public class Package
    {
        public virtual long PackageID { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual string PackageType { get; set; }
        public virtual string ContentDescription { get; set; }
        public virtual decimal Value { get; set; }
    }

    
}