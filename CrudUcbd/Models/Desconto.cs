using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudUcbd.Models
{
    [Table("desconto")]
    public class Desconto
    {
        [Key]
        [Required]
        [Column("descontoId")]
        public int DescontoId { get; set; }

        [Column("pedido_id")]
        public int PedidoId { get; set; }

        [Column("valorDesconto")]
        public double ValorDesconto { get; set; }
    }
}
