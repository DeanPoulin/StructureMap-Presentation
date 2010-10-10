using System;
using System.ComponentModel.DataAnnotations;

namespace DeanIS.YBSquare.Api.Models
{
    public class CheckInModel
    {
        [Required]
        [Display(Name = "UserId")]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "PlaceId")]
        public long PlaceId { get; set; }
    }
}