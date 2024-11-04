using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGerenciamento.Models
{
    public class ItemDoMenuViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Image { get; set; }
    }
}