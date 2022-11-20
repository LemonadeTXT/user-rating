using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRating.Common.Models
{
    public class Appraiser
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AppraiserId { get; set; }

        public int Liked { get; set; }
    }
}
