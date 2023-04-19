using System;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DesignPatterns
{
    public class Person
    {
        // address
        public string StreetAddress, PostCode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string? ToString()
        {
            return $"{StreetAddress} {PostCode} {City} {CompanyName} {Position} {AnnualIncome}";
        }
    }

    public class PersonBuilderFacade
    {
        protected Person person = new();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilderFacade person) { return person.person; }
    }

    public class PersonJobBuilder : PersonBuilderFacade
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string CompanyName)
        {
            person.CompanyName = CompanyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilderFacade
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder AtStreet(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder AtPin(string postCode)
        {
            person.PostCode = postCode;
            return this;
        }

        public PersonAddressBuilder AtCity(string city)
        {
            person.City = city;
            return this;
        }
    }

    class Demo
    {
        static void Main()
        {
            PersonBuilderFacade personBuilderFacade = new PersonBuilderFacade();

            Person person = personBuilderFacade.
                Works.At("SBI")
                     .AsA("Bank Clerk")
                     .Earning(123000).
                Lives.AtStreet("Downing")
                     .AtPin("500089")
                     .AtCity("Hyderabad");

            Console.WriteLine(person.ToString());
        }
    }
}

