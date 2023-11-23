namespace Domain
{
    public class Customer
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}