using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace CondoMinicator.Models
{
    public class Comunicado
    {
        public int ID { get; set; }
        [Required, Display(Name = "De")]
        public int Usuario_de_ID { get; set; }
        [Required]
        public int Usuario_para_ID { get; set; }
        [Required]
        public int CondominioID { get; set; }
        [Required, Display(Name = "Assunto")]
        public int AssuntoID { get; set; }

        [ForeignKey("Usuario_de_ID")]
        public virtual Usuario De { get; set; }

        [ForeignKey("Usuario_para_ID")]
        public virtual Usuario Para { get; set; }

        [ForeignKey("CondominioID")]
        public virtual Administradora Condominio { get; set; }

        [ForeignKey("AssuntoID"), Display(Name = "Mensagem")]
        public virtual Assunto Assunto { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "Mensagem")]
        [Required]
        public string Mensagem { get; set; }
    }
}