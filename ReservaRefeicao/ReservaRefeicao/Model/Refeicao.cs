﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Model
{
    [Table("TBRefeicao", Schema = "Refeicao")]
    public class Refeicao
    {
        [Key]
        public int CodRefeicao { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Tipo { get; set; }

        public string? Cardapio { get; set; }

        public string? CardapioFormatado {
            get {
                if (Cardapio == null)
                    return null;

                return Cardapio.Replace(";", "\n").Replace(" \n", "\n").Replace("\n ", "\n");
            }
        }
    }
}
