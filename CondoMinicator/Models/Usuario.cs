using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondoMinicator.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int CondominioID { get; set; }
        public int Usuario_TipoID { get; set; }
        [ForeignKey("CondominioID")]
        public virtual Condominio Condominio { get; set; }
        [ForeignKey("Usuario_TipoID")]
        public virtual Usuario_Tipo Usuario_Tipo { get; set; }
    }
}