using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGerenciamento.Models
{
    [Table("TBLmesa")]
    public class Mesa
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int Numero { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser maior que zero.")]
        public int Capacidade { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
}
