using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BandAPI.Entities;
using BandAPI.Models;
using BandAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BandAPI.Controllers
{
    [ApiController]
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController : ControllerBase
    {
        
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? 
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> GetAlbumForBand(Guid bandId) 
        {
            if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound("Band ko ton tai");
            var albumsFromRepo = _bandAlbumRepository.GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albumsFromRepo));
        }
        [HttpGet("{albumId}")]
        public ActionResult<AlbumDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumsFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if(albumsFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<AlbumDto>(albumsFromRepo));
        }
    }
}