using System.Net.Http.Headers;

namespace P1Client
{
    public class Program

    {
        public static User currentUser; //= new User();
        public static Manager currentManager;//= new Manager();
        public static HttpClient client = new HttpClient();
        public static List<User> users;
        public static List<Ticket> tickets;
        public static List<Ticket> pendingTickets;
        public static void Main(string[] args)
        {
            RunAsync();

            Menu menu = new Menu();
            bool choosingStart = true;

            while (choosingStart)
            {
                currentUser = new User();
                currentManager = new Manager();

                int choice = (menu.StartMenu());
                switch (choice)
                {
                    case 1:

                        User tempU = menu.UserLoginMenu();

                        currentManager = null;
                        Task.WaitAll(LogInUser(tempU));

                        if (currentUser == null)
                        {
                            menu.LogInFailMessage();
                            continue;
                        }

                        break;
                    case 2:
                        currentUser = null;
                        Manager tempM = menu.ManagerLoginMenu();

                        Task.WaitAll(LogInManager(tempM));

                        if (currentManager == null)
                        {
                            menu.LogInFailMessage();
                            continue;
                        }

                        break;
                    case 3:
                        User u = menu.NewEmployeeMenu();

                        Task.WaitAll(CreateNewUser(u));

                        break;

                    case 4:
                        System.Environment.Exit(0);
                        break;
                }

                choosingStart = false;
            }

            if (currentUser != null)
            {
                bool userChoosing = true;

                while (userChoosing)
                {
                    int userMenuChoice = menu.UserChoiceMenu(currentUser);

                    switch (userMenuChoice)
                    {
                        case 1:
                            SubmitNewTicket(menu.NewTicketMenu(currentUser));
                            continue;

                        case 2:
                            Task.WaitAll(GetUserTickets(currentUser));
                            menu.ViewUserTicketsMenu(currentUser, tickets);
                            continue;
                        case 3:
                            menu.UserTicketsByTypeMenu(currentUser);
                            Task.WaitAll(GetUserTicketsByType(currentUser));
                            menu.ViewTicketList(tickets);
                            continue;
                        case 4:
                            System.Environment.Exit(0);
                            break;
                    }
                    userChoosing = false;
                }
            }
            else if (currentUser == null && currentManager != null)
            {
                bool managerChoosing = true;
                while (managerChoosing)
                {
                    int managerChoice = menu.ManagerChoiceMenu(currentManager);

                    switch (managerChoice)
                    {
                        case 1:
                            Task.WaitAll(GetPendingTickets(currentManager));
                            menu.ViewTicketList(tickets);
                            continue;
                        case 2:
                            Task.WaitAll(GetPendingTickets(currentManager));
                            ChangeTicketStatus(menu.ApproveOrDeny(tickets));
                            continue;
                        case 3:
                            //menu.PromoteEmployeeMenu();
                            break;
                        case 4:
                            System.Environment.Exit(0);
                            break;
                    }
                    managerChoosing = false;
                }
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }


        }



        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:7103/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));



        }

        public static async Task<List<User>> GetAllUsers()
        {
            users = new List<User>();

            var path = "/employees";
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<User>>();
            }
            return users;
        }

        public static async Task<List<Ticket>> GetAllTickets()
        {
            tickets = new List<Ticket>();

            var path = "/tickets";
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                tickets = await response.Content.ReadAsAsync<List<Ticket>>();
            }
            return tickets;

        }

        public static async Task<List<Ticket>> GetUserTickets(User u)
        {
            tickets = new List<Ticket>();
            var path = "/tickets/employee";

            HttpResponseMessage response = await client.PostAsJsonAsync(path, u);
            response.EnsureSuccessStatusCode();

            tickets = await response.Content.ReadAsAsync<List<Ticket>>();


            return tickets;
        }

        public static async Task<List<Ticket>> GetUserTicketsByType(User u)
        {
            tickets = new List<Ticket>();

            var path = "/tickets/employee/type";
            HttpResponseMessage response = await client.PostAsJsonAsync(path, u);
            response.EnsureSuccessStatusCode();

            tickets = await response.Content.ReadAsAsync<List<Ticket>>();

            return tickets;
        }

        public static async Task<List<Ticket>> GetPendingTickets(Manager m)
        {
            tickets = new List<Ticket>();
            var path = "/tickets/pending";
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();

           tickets = await response.Content.ReadAsAsync<List<Ticket>>();

            return tickets;
        }

        public static async void SubmitNewTicket(Ticket t)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/tickets", t);
            response.EnsureSuccessStatusCode();
        }

        public static async Task<User> CreateNewUser(User u)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/employees", u);
            response.EnsureSuccessStatusCode();
            currentUser = await response.Content.ReadAsAsync<User>();
            return currentUser;
        }
        public static async Task<User> LogInUser(User temp)
        {

            var path = "/employees/login";
            HttpResponseMessage response = await client.PostAsJsonAsync(path, temp);
            response.EnsureSuccessStatusCode();
            currentUser = await response.Content.ReadAsAsync<User>();

            return currentUser;
        }

        public static async Task<Manager> LogInManager(Manager temp)
        {
            var path = "/managers/login";
            HttpResponseMessage request = await client.PostAsJsonAsync(path, temp);
            request.EnsureSuccessStatusCode();
            currentManager = await request.Content.ReadAsAsync<Manager>();

            return currentManager;
        }

        public static async void ChangeTicketStatus(Ticket t)
        {
            var path = "/tickets/{id}";

            HttpResponseMessage response = await client.PutAsJsonAsync(path, t);
            response.EnsureSuccessStatusCode();
        }


    }
}