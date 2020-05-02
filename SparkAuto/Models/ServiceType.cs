using System.ComponentModel.DataAnnotations;

namespace SparkAuto.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
