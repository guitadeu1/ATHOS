using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondoMinicator.Models
{
    public class Condominio
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int AdministradoraID { get; set; }
        public int Usuario_TipoID { get; set; }
        [ForeignKey("AdministradoraID")]
        public virtual Administradora Administradora { get; set; }
        [ForeignKey("Usuario_TipoID")]
        public virtual Usuario_Tipo Responsavel { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}