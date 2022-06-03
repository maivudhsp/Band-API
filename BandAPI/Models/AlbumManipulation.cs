using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BandAPI.ValidationAttributes;

namespace BandAPI.Models
{
    [TitleAndDescriptionAtrribute(ErrorMessage = "Title Must be Different From Sescrtiption")]
    public abstract class AlbumManipulation
    {
        [Required(ErrorMessage = "Title needs to be filled in")]
        [MaxLength(200, ErrorMessage = "Title needs to be up to 200 characters")]
        public string Title  { get; set; }

        [MaxLength(400,  ErrorMessage = "Title needs to be up to 400 characters")]
        public virtual string Description { get; set; }
    }
}