using System.Text.RegularExpressions;

namespace KAW.Domain.Models
{
    //private setters ensures that the properties can only be set within the class,
    //enforcing encapsulation and data integrity.
    public class UserExpression
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; }

        private static readonly Regex AllowedCharacters =
            new Regex(@"[^\p{L}\p{N} \-']", RegexOptions.Compiled);


        public UserExpression(string name, string description)
        {
            Update(name, description);
        }
        public override string ToString()
        {
            return String.Format($"Navn: {Name} \nBeskrivelse: {Description}");
        }

        public void Update(string name, string description)
        {
            SetName(name);
            SetDescription(description);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty");

            Name = Clean(name);
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Description cannot be empty");

            Description = Clean(description);
        }

        private string Clean(string value)
        {
            var cleaned = AllowedCharacters.Replace(value, "")
                .Trim();

            return cleaned;
        }

        public class DomainException : Exception
        {
            public DomainException(string message) : base(message) { }
        }
    }
}



