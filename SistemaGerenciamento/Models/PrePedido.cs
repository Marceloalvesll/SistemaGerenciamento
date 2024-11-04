using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaGerenciamento.Models
{
    [Table("TBLprepedido")]
    public class PrePedido
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pendente";

        public decimal Total { get; set; }


        // Relacionamento com Itens através da tabela de junção
        public List<PrePedidoItemDoMenu> PrePedidoItens { get; set; } = new List<PrePedidoItemDoMenu>();
    }


}