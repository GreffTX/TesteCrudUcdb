using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudUcbd.Models
{
    [Table ("pedido")]
    public class Pedido
    {
        [Key]
        [Required]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("nome_produto")]
        public string NomeProduto { get; set; }
        
        [Column("valor")]       
        public double Valor { get; set; }

        [Column("data_vencimento")]       
        public DateTime DataVencimento { get; set; }
    }
}
