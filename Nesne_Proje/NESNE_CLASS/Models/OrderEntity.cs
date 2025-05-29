using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nesne_Proje.NESNE_CLASS.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } 
    }
}
