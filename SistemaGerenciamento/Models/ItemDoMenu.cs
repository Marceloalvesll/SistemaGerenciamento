using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SistemaGerenciamento.Models
{
    [Table("TBLitemdomenu")]
    public class ItemDoMenu
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        [NotMapped]
        public string PrecoString { get; set; } // Propriedade auxiliar para manipulação na view

        public decimal Preco { get; set; } // Campo real

        public bool Disponivel { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public CategoriaDoMenu Categoria { get; set; }

        [Display(Name = "Upload da Imagem")]
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string Image { get; set; }
    }
}
