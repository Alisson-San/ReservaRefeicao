using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Model
{
    public class Refeicao
    {
        [Key]
        public int CodRefeicao { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Tipo { get; set; }

        public string? Cardapio { get; set; }

    }
}
