using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PequeñoCRUD.Data
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "* Correo incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Obligatorio")]
        public string Phone { get; set; }

        public DateTime DateTall { get; set; }
    }
}
