using KAW.Application.Interfaces;
using KAW.Application.Services;
using KAW.Domain.Models;
using KAW.Infrastructure.Repository;


Console.WriteLine("Har du også forvildet dig ind i Nordjylland? " +
    "Frygt ikke, her er en liste over mærkelige udtryk og deres forklaringer. ");






IUserExpressionRepo repo = new ExpressionRepo();
IExpressionService serviceExpression = new ExpressionService(repo);

bool stay = true;

while (stay)
{
    Console.WriteLine("Menu: \nTast 1 for at tilføje udtryk: \nTast 2 for at søge efter udtryk: " +
"\nVælg 3 for at se alle udtryk: \nTast 4 for fuld panik");
    int choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            Console.WriteLine("Tilføj udtrykkets navn efterfulgt af enter: ");
            string name = Console.ReadLine();
            Console.WriteLine("Skriv en beskrivelse af udtrykket efterfulgt af enter: ");
            string description = Console.ReadLine();

            var exp = new UserExpression
            {
                Name = name,
                Description = description
            };
            try
            {
                serviceExpression.AddExpression(exp);
                Console.WriteLine($"Tak for hjælpen. {name} er nu tilføjet til ordbogen med beskrivelsen: {description}");
                choice = 5;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 2:
            Console.WriteLine("Søg efter udtryk: ");
            break;
        case 3:
            try
            {
                Console.WriteLine("Se alle udtryk: ");
                List<UserExpression> userExpressions = new List<UserExpression>();
                userExpressions = serviceExpression.GetAllExpressions();
                foreach (var userExpression in userExpressions)
                {
                    Console.WriteLine(userExpression);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 4:
            Console.WriteLine("Luk programmet, jeg kører hjem igen.");
            stay = false;
            break;

    }

}
