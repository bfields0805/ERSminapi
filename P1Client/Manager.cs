using System.Text;

namespace P1Client
{
    public class Manager
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public int empId { get; set; }

        public string requestData { get; set; }
        public int requestInt { get; set; }

        public Manager() { }
        public Manager(string email, string password)
        {
            this.email = email;
            this.password = password;
            this.userName = "";
        }

        public Manager(string userName, string password, string email, int empId)
        {
            this.email = email;
            this.userName = userName;
            this.password = password;
            this.empId = empId;

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
