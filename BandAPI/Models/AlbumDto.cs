using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BandAPI.Entities;

namespace BandAPI.Models
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string Title  { get; set; }
        public string Description { get; set; }  
        public Guid BandId { get; set; }
    }
}