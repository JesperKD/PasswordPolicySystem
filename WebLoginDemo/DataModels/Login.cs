namespace WebLoginDemo.DataModels
{
    public class Login
    {
        private readonly string _username;
        private readonly string _password;
        private readonly int _attempts;

        public Login(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public Login(string username, string password, int attempts)
        {
            _username = username;
            _password = password;
            _attempts = attempts;
        }

        public string Username { get { return _username; } }
        public string Password { get { return _password; } }
        public int Attempts { get { return _attempts; } }
    }
}
