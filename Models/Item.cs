using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HierarchicalItemProcessingSystem.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        public int? ParentId { get; set; }

        public string Status { get; set; } = "Unprocessed"; // Processed / Unprocessed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("ParentId")]
        public Item? Parent { get; set; }

        public ICollection<Item> Children { get; set; } = new List<Item>();
    }
}
