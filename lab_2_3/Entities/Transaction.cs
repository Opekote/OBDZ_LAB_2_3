namespace lab_2_3.Entities
{
    public class Transaction
    {
        public virtual long TransactionID { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual Service Service { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual decimal Amount { get; set; }
    }

    
}