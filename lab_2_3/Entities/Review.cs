using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace lab_2_3.Entities
{
    public class Review
    {
        public virtual long ReviewID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual int Rating { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime ReviewDate { get; set; }
    }
    
}