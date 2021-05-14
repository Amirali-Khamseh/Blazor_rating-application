using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is required ")]
        public string Name { get; set; }
    }
}
