using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Application.Dtos
{   [Table("Eventos")]
    public class EventoDto
    {   [Key]
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório."),
         StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre 3 e 50 caracteres.")]
        public string Tema { get; set; }
        [Range(2, 120000, ErrorMessage = "O campo {0} deve ter entre 2 e 120000 pessoas.")]
        public int QtdPessoas { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)")]
        public string ImageUrl { get; set; }
        [Phone(ErrorMessage = "Telefone em formato inválido.")]
        public string Telefone { get; set; }
        [Display(Name = "E-mail"),
         Required(ErrorMessage = "O campo {0} é obrigatório."),
         RegularExpression(".+\\@.+\\..+", ErrorMessage = "E-mail em formato inválido."),
         StringLength(50, MinimumLength = 5, ErrorMessage = "O campo {0} deve ter entre 5 e 50 caracteres."),]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteEventoDto> PalestrantesEventos { get; set; }      
    }
}