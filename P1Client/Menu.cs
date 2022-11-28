namespace P1Client
{
    public class Menu
    {
        public Menu() { }

        public void Title()
        {
            Console.Clear();
            Console.WriteLine("Employee Reembursment System \n\n");

        }
        public int StartMenu()
        {
            Title();

            Console.WriteLine(" 1) Employee Login ");
            Console.WriteLine(" 2) Manager Login ");
            Console.WriteLine(" 3) New Employee ");
            Console.WriteLine(" 4) End Session");

            bool choosing = true;
            int choice = 0;

            while (choosing)
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 4))
                {
                    Console.WriteLine("Invalid input, Please enter the number by your choice \n");
                    continue;

                }
                choosing = false;
            }
            return choice;
        }

        public User UserLoginMenu()
        {
            Title();
            User temp;

            Console.WriteLine("Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            temp = new User(email, password);


            return temp;


        }

        public Manager ManagerLoginMenu()
        {
            Title();
            Manager temp;

            Console.WriteLine("Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            temp = new Manager(email, password);

            return temp;
        }

        public void LogInFailMessage()
        {
            Title();
            Console.WriteLine("Incorrect Email or Password");
            Console.WriteLine("Returning to Start Menu ");
            Thread.Sleep(3000);
        }

        public User NewEmployeeMenu()
        {
            Title();

            User u;

            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your email: ");

            string email = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Choose a password: ");

            string password = Console.ReadLine();

            u = new User(name, email, password);

            return u;
        }

        public int UserChoiceMenu(User u)
        {
            bool choosing = true;
            Title();
            Console.WriteLine("Hello " + u.userName);
            Console.WriteLine();

            int choice = 0;
            while (choosing)
            {
                Console.WriteLine("1) Submit new ticket ");
                Console.WriteLine("2) View all of my tickets ");
                Console.WriteLine("3) Filter my tickets by type");
                Console.WriteLine("4) End Session ");

                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 4))
                {
                    Console.WriteLine("Invalid Choice");
                    continue;
                }

                choosing = false;

            }
            return choice;
        }

        public Ticket NewTicketMenu(User u)
        {
            Title();
            Console.WriteLine();
            Console.WriteLine("Ticket submission for " + u.userName);
            Console.WriteLine();

            Ticket ticket = new Ticket();

            ticket.employee = u.empId;
            ticket.status = TicketStatus.Pending;

            Console.WriteLine("Ticket type: ");
            Console.WriteLine("1) Travel");
            Console.WriteLine("2) Food");
            Console.WriteLine("3) Lodging");
            Console.WriteLine("0) Other");

            bool choosingType = true;
            int choice = 4;
            double amount = 0.00;

            while (choosingType)
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 3))
                {
                    Console.WriteLine("Invalid Choice");
                    continue;
                }
                choosingType = false;
            }

            ticket.type = (TicketType)choice;

            Title();
            Console.WriteLine();

            Console.WriteLine("Amount: ");

            bool amountInput = true;

            while (amountInput)
            {

                if (!Double.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Invalid input, (do not type '$') ");
                    continue;
                }
                amountInput = false;
            }
            ticket.amount = amount;

            bool inputData = true;

            Title();
            Console.WriteLine();
            Console.WriteLine("What is the ticket for?  :");
            string data;
            while (inputData)
            {
                data = Console.ReadLine();

                if (data != null)
                {
                    ticket.data = data;
                    inputData = false;
                }
                else
                {
                    Console.WriteLine("This may not be left blank");
                }
            }

            return ticket;
        }

        public void ViewTicketList(List<Ticket> tickets)
        {
            Title();
            Console.WriteLine();

            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine(ticket);
            }

            Console.WriteLine("Press Enter to Continue");

            Console.ReadLine();

        }

        public void ViewUserTicketsMenu(User u, List<Ticket> tickets)
        {
            Title();
            Console.WriteLine();

            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine(ticket);
            }

            Console.WriteLine("Press Enter to Continue");

            Console.ReadLine();

        }

        public void UserTicketsByTypeMenu(User u)
        {
            Title();
            Console.WriteLine();

            bool choosing = true;

            int choice;

            Console.WriteLine("Which type? :");
            Console.WriteLine();
            Console.WriteLine("0) Other");
            Console.WriteLine("1) Travel");
            Console.WriteLine("2) Food");
            Console.WriteLine("3) Lodging");

            while (choosing)
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 3))
                {
                    Console.WriteLine("Invalid choice");
                    continue;
                }
                u.requestInt = choice;
                choosing = false;
            }


        }



        public int ManagerChoiceMenu(Manager m)
        {
            Title();
            Console.WriteLine("Hello, " + m.userName);
            Console.WriteLine();

            int choice = 0;
            bool choosing = true;

            Console.WriteLine("1) View all pending tickets ");
            Console.WriteLine("2) Approve or Deny a ticket");
            Console.WriteLine("3) Promote an Employee to Manager ");
            Console.WriteLine("4) End Session");

            while (choosing)
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 4))
                {
                    Console.WriteLine("Invalid Choice");
                    continue;
                }

                choosing = false;

            }
            return choice;
        }

        public Ticket ApproveOrDeny(List<Ticket> tickets)
        {
            Title();
            Console.WriteLine();
            Ticket t = new Ticket();

            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine(ticket);
            }

            int idNum=0;

            Console.WriteLine();
            Console.WriteLine("Enter the ID Number of the Ticket and Press Enter");
            Int32.TryParse(Console.ReadLine(), out idNum);

            foreach(Ticket ticket in tickets)
            {
                if (idNum == ticket.id)
                {
                     t =ticket;
                }
            }

            bool approving = true;
            int choice = 0;
            Console.WriteLine();
            Console.WriteLine("1) Approve");
            Console.WriteLine("2) Deny");
            while (approving)
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 2))
                {
                    Console.WriteLine("Invalid Choice");
                    continue;
                }
                approving = false;

                t.requestInt = choice;
            }

            return t;
        }

        public User PromoteEmployeeMenu(List<User> employees)
        {
            User u = new User();

            Title();

            foreach(User user in employees)
            {
                Console.WriteLine(user);
            }
            Console.WriteLine();
            Console.WriteLine("Enter the Employee ID of the person being promoted");

            bool choosing = true;
            int choice = 0;

            while (choosing)
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid entry: just type in the ID number");
                    continue ;
                }

                foreach (User user in employees)
                {
                    if(user.empId == choice)
                    {
                        u = user;
                    }

                }
                if (u.empId == null)
                {
                    Console.WriteLine("That is not an Employee ID number");
                    continue;
                }



                choosing = false;
            }
            
            
            return u;
        }






    }
}

