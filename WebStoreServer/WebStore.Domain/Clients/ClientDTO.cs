

using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Orders;

namespace WebStore.Domain.Clients
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public string? FullName { get; set; }
        public List<OrderDTO>? Orders { get; set; }


        public ClientDTO() { }

        public ClientDTO(Client client)
        {
            Id = client.Id;
            Mail = client.Mail;
            FullName = client.FullName;
            Orders = new List<OrderDTO>();

            foreach (var order in client.Orders)
            {
                var o = new OrderDTO()
                {
                    Id = order.Id,
                    Date = order.Date,
                    Time = order.Time,
                };

                if (order.OrderType != null) o.OrderType = order.OrderType.Description;

                Orders.Add(o);
            }
        }

        public Client ToClient()
        {
            var cl = new Client();

            cl.Id = Id;
            cl.Mail = Mail;
            cl.FullName = FullName;

            cl.Orders = new List<Order>();

            foreach (var order in Orders)
            {
                var o = new Order()
                {
                    Id = order.Id,
                    Date = order.Date,
                    Time = order.Time,
                    OrderType = new OrderType() { Description = order.OrderType }
                };

                cl.Orders.Add(o);
            }

            return cl;
        }
    }
}
