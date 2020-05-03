using System;
using System.ComponentModel.DataAnnotations;

namespace SparkAuto.Models
{
    public class ServiceType
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Atenção, o nome do serviço é obrigatório", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Atenção, o preço do serviço é obrigatório", AllowEmptyStrings = false)]
        public double Price { get; set; }
    }
}
