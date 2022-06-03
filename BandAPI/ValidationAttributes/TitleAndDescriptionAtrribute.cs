using System.ComponentModel.DataAnnotations;
using BandAPI.Models;

namespace BandAPI.ValidationAttributes
{
    public class TitleAndDescriptionAtrribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            var album = (AlbumManipulation)validationContext.ObjectInstance;
            if(album.Title == album.Description)
            {
                return  new ValidationResult("The title and the description need to be different",
                new[] {"AlbumManipulation"});
            }

            return ValidationResult.Success;

        }
    }
}