using System.ComponentModel.DataAnnotations;

namespace CardComSite1
{
    public enum Gender
    {
        female = 0,
        male = 1,
        other = 2
    }

    public class Person
    {
        public int Id { get; set; }
        public string citizenId { get; set; }
        public string name { get; set; }
        public string? email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public Gender? gender { get; set; }
        public string? phone { get; set; }
    }
}
