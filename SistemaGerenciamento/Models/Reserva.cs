using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGerenciamento.Models
{
    [Table("TBLreserva")]
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHoraReserva { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de pessoas deve ser maior que zero.")]
        public int QuantidadePessoas { get; set; }

        public int MesaId { get; set; }

        [ForeignKey("MesaId")]
        public Mesa Mesa { get; set; }
    }
}
