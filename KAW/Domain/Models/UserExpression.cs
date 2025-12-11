using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace KAW.Domain.Models
{
    public class UserExpression
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        private static readonly Regex AllowedCharacters =
            new Regex(@"[^\p{L}\p{N} \-']", RegexOptions.Compiled);


        public UserExpression(string name, string description)
        {
            Name = name;
            Description = description;
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
            if (name.IsNullOrEmpty())
                throw new DomainException("Name cannot be empty");

            Name = Clean(name);
        }

        private void SetDescription(string description)
        {
            if (description.IsNullOrEmpty())
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



