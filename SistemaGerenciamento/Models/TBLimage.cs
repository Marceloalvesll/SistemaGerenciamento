using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaGerenciamento.Models
{
    [Table("TBLimage")]
    public class TBLimage
    {

        public int Id { get; set; }         // Identificador único da imagem
    
        public string Title { get; set; }   // Título da imagem
        [DisplayName("Upload Image")]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        
    }
}