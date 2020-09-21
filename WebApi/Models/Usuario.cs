using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{

  [Table("Usuario")]
  public class Usuario
  {
    [Key]
    [Column("ID_Usuario")]
    public int id { get; set; }

    [Required(ErrorMessage = "Login é Obrigatório")]
    [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres")]
    public string Login { get; set; }

    public int? Perfil { get; set; }

    public int? ID_Mobile { get; set; }

    public int? Status { get; set; }

    public int? id_cliente { get; set; }

    [Required(ErrorMessage = "Senha é Obrigatório")]
    public string Senha { get; set; }

  }
}