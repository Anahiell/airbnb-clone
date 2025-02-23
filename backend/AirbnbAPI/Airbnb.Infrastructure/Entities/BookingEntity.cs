using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Infrastructure.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime StartDate { get;set; }

        public DateTime EndDate { get; set; }
        public string Status { get; set; } //pending,confirmed,canceled

    }
}
