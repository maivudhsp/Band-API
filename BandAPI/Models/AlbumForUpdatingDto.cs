using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    public class AlbumForUpdatingDto : AlbumManipulation
    {

        // public string Title  { get; set; }
        // public string Description { get; set; }
        [Required(ErrorMessage = "Description needs to be filled in")]
        public override string Description { 
            get => base.Description; 
            set => base.Description = value; 
            }
    }
}