namespace IBA.Task3.DAL.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }


        public User(string firstName, string lastName, string surName, string login, string password):this()
        {
            FirstName = firstName;
            LastName = lastName;
            SurName = surName;
            Login = login;
            Password = password;
        }

        public User() : base() { }
    }
}