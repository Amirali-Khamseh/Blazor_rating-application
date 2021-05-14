using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Shared.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Like { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Category Category { get; set; }
        
    }
}
