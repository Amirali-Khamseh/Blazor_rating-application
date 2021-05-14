using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Server.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Like { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual CategoryModel Category { get; set; }
    }
}
