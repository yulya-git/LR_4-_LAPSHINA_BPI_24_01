using System;

namespace LR_4_LAPSHINA_BPI_24_01.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public Person() { }
        public Person(int id, int roleId, string firstName, string lastName, DateTime birthday)
        {
            this.Id = id;
            this.RoleId = roleId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
        }

        public Person CopyFromPersonDpo(PersonDpo p)
        {
            Person person = new Person();
            person.Id = p.Id;
            person.FirstName = p.FirstName;
            person.LastName = p.LastName;
            person.Birthday = p.Birthday;
            return person;
        }
    }
}