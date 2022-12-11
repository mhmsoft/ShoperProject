using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class ProductComment
    {
        [Display(Name="Yorum No")]
        public int CommentId { get; set; }
        [Display(Name = "Yorum")]
        public string Comment { get; set; }
        [Display(Name = "Yorum Tarihi")]
        public DateTime? CommentDate { get; set; }
        [Display(Name = "Yorum Sahibi")]
        public string? Writer { get; set; }
        [Display(Name = "Eposta")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Display(Name = "Onay")]
        public bool isVerified { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
