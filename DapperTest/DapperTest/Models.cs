using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTest
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();

        public override string ToString()
        {
            return $"{OwnerId}  -  {FirstName} {LastName} \n" +
                $"Email: {Email}";
        }
    }

    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath {  get; set; }
        public Owner Owner { get; set; }
    }
}
