using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Models
{
    public class User
    {
        public UserTypeEnum UserType { get; set; }
        public string UserId { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }
    }
}
