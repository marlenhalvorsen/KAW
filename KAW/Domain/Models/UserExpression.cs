namespace KAW.Domain.Models
{
    public class UserExpression
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return String.Format($"Navn: {Name} \nBeskrivelse: {Description}");
        }
    }
}



