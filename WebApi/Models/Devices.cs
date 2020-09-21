using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{

  [Table("device")]
  public class Device
  {
    [Key]
    [Column("id")]
    public int id { get; set; }

    [Required(ErrorMessage = "Price é Obrigatório")]
    public decimal price { get; set; }

    [Required(ErrorMessage = "Model é Obrigatório")]
    [MaxLength(100, ErrorMessage = "Máximo de 50 caracteres")]
    public string model { get; set; }

    [Required(ErrorMessage = "Brand é Obrigatório")]
    [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
    public string brand { get; set; }

    public string photo { get; set; }

    public string date { get; set; }

  }
}