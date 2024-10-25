using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ReservaRefeicao.Model
{

    [Table("TBReservas", Schema = "Refeicao")]
    public class Reserva
    {
        [Key]
        public required int CodReserva { get; set; }

        [Required]
        public required int Repreg { get; set; }
        //[ForeignKey("Repreg")]
        //public virtual Funcionario? Funcionario { get; set; }

        //[ForeignKey("CodRefeicao")]
        [Required]
        public required int CodRefeicao { get; set; }
        //public required virtual Refeicao Refeicao { get; set; }

        [Required]
        public DateTime DataReserva { get; set; }
    }
}