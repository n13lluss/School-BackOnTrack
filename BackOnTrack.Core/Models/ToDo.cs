using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; } = "";
        [Required]
        public DateTime PlannedDate { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int Status { get; set; }

        public string GetStatusAsString()
        {
            return Status switch
            {
                0 => "Not Yet Started",
                1 => "In Progress",
                2 => "Completed",
                _ => "Unknown",
            };
        }
    }
}
