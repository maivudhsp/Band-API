
using System.ComponentModel.DataAnnotations;
using BandAPI.ValidationAttributes;

namespace BandAPI.Models
{
    public class AlbumForCreatingDto : AlbumManipulation
    {

        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //     if(Title == Description)
        //     {
        //         yield return new ValidationResult("The title and description need to be different",
        //         new[] {"AlbumForCreatingDto"});
        //     }
        // }

    
    }
}