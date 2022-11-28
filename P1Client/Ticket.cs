using System;

namespace P1Client
{ 
public class Ticket
{


    public int id { get; set; }
    public string data { get; set; }
    public double amount { get; set; }
    public int employee { get; set; }

    public TicketStatus status { get; set; }





    public Ticket() { }

    public Ticket(int empId, double amt, string data)
    {
        employee = empId;
        amount = amt;
        this.data = data;
        status = TicketStatus.Pending;
    }

    public Ticket(int id, string data, double amount, int employee, TicketStatus status)
    {
        this.id = id;
        this.data = data;
        this.amount = amount;
        this.employee = employee;
        this.status = status;
    }
    public Ticket(int employee, double amount, string data, int status)
    {
        this.employee = employee;
        this.amount = amount;
        this.data = data;
        this.status = (TicketStatus)status;


    }


    public void changeStatus(TicketStatus s)
    {

        status = s;
    }






    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("ID: " + id + " Created by: " + employee + " Amount: " + amount +
         " For: " + data + " Status: " + status);

        return sb.ToString();
    }


  }
}

