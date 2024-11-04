using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaGerenciamento.Models
{
    public class PrePedidoItemDoMenu
    {
        [Key]
        public int Id { get; set; }

        public int PrePedidoId { get; set; }
        public int ItemDoMenuId { get; set; }

        [ForeignKey("PrePedidoId")]
        public virtual PrePedido PrePedido { get; set; } // Adicione 'virtual' aqui

        [ForeignKey("ItemDoMenuId")]
        public virtual ItemDoMenu ItemDoMenu { get; set; } // Adicione 'virtual' aqui
    }
}
