using System.Text;

namespace P1Client
{
    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public string requestData { get; set; }
        public int requestInt { get; set; }


        public int empId { get; set; }




        public User()
        { }
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
            userName = "";

        }
        public User(string name, string email, string password, int empId)
        {
            this.userName = name;
            this.email = email;
            this.password = password;
            this.empId = empId;

        }

        public User(string name, string email, string password)
        {
            this.userName = name;
            this.email = email;
            this.password = password;

        }









        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(empId + " ");
            sb.Append(userName + " ");
            sb.Append(email + " ");


            return sb.ToString();

        }

    }
}

