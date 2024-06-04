namespace lab_2_3.Entities
{
    public class Customer
    {
        public virtual long CustomerID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual IList<Review> Reviews { get; set; }
    }

}