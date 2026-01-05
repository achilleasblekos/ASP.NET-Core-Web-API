using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot exceed 10 characters.")]
        public string? Symbol { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public string? CompanyName { get; set; }
        [Required]
        [Range(1, 100000000)]
        public decimal? PurchasePrice { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal? LastDiv { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Industry cannot exceed 50 characters.")]
        public string? Industry { get; set; }
        [Required]
        [Range(1, long.MaxValue)]
        public long MarketCap { get; set; }
    }
}