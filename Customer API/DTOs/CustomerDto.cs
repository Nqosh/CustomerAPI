namespace Customer_API.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string InvoiceTotal { get; set; }
    }
}
