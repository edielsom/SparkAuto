using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SparkAuto.Models
{
    public class ServiceType
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Atenção, o nome do serviço é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Atenção, o preço do serviço é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Preço")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
