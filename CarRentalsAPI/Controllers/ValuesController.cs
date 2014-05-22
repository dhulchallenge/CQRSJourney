using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Booking.Command;
using Booking.Readmodel;
using Infrastructure.Messaging;

namespace CarRentalsAPI.Controllers
{
    public class ValuesController : ApiController
    {
       
         private readonly IBookingDao orderDao;
        private readonly ICommandBus bus;

        static ValuesController()
        {
            Mapper.CreateMap<CarRentals.Booking, Booking.Booking>();
        }

        public ValuesController(IBookingDao orderDao, ICommandBus bus)
        {
            this.orderDao = orderDao;
            this.bus = bus;
        }
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]CarRentals.Booking value)
        {
            ICommand bookCar = new BookCar(){CarId = 1,BookingStartDate = value.BookingStartDate,BookingEndDate = value.BookingEndDate};
            this.bus.Send(bookCar);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
