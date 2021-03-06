﻿using System.ComponentModel.DataAnnotations;
using SIGCOMT.DTO.Core;
using SIGCOMT.Resources;
using SIGCOMT.Resources.CustomModelMetadata;

namespace SIGCOMT.DTO
{
    [MetadataConventions(ResourceType = typeof(Usuario))]
    public class UsuarioDto : EntityDto<int>
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [Display]
        [EmailAddress(ErrorMessage = null)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 0)]
        [Display]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 0)]
        [Display]
        public string Apellido { get; set; }

        [Required]
        [Display]
        public int RolId { get; set; }

        public string NombreCompleto { get; set; }
        public string RolNombre { get; set; }
        public int Estado { get; set; }
    }
}