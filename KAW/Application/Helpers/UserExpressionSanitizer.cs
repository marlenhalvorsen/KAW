using KAW.Domain.Models;
using System.Text.RegularExpressions;

namespace KAW.Application.Helpers
{
    public static class UserExpressionSanitizer
    {
        public static void CleanExpression(UserExpression input)
        {
            var cleanedName = Regex.Replace(input.Name, @"[^\p{L}\p{N} \-']", "")
                .Trim();
            var cleanedDescription = string.IsNullOrWhiteSpace(input.Description)
                ? null
                :Regex.Replace(input.Description, @"[^\p{L}\p{N} \-']", "")
                .Trim();
            input.Name = cleanedName;
            input.Description = cleanedDescription;
        }
    }
}
