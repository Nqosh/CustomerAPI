using System;

namespace Customer_API.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string InvoiceTotal { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
