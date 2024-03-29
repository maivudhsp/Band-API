using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BandAPI.Entities;
using BandAPI.Models;
using BandAPI.Service;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{albumId}", Name="GetAlbumForBand")]
        public ActionResult<AlbumDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumsFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if(albumsFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<AlbumDto>(albumsFromRepo));
        }

        [HttpPost]
        public ActionResult<AlbumDto> CreateAlbumForBand(Guid bandId, [FromBody] AlbumForCreatingDto album)  
        {
            if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumEntity = _mapper.Map<Album>(album);
            _bandAlbumRepository.AddAlbum(bandId, albumEntity);
            _bandAlbumRepository.Save();

            var albumToReturn = _mapper.Map<AlbumDto>(albumEntity);

            return CreatedAtRoute("GetAlbumForBand", new {bandId = bandId, albumId = albumToReturn.Id}, albumToReturn);
        }
        [HttpPut("{albumId}")]
        public ActionResult UpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] AlbumForUpdatingDto album)
        {
            if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumsFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if(albumsFromRepo == null)
            {
                var albumToAdd = _mapper.Map<Album>(album);
                albumToAdd.Id = albumId;
                _bandAlbumRepository.AddAlbum(bandId, albumToAdd);
                _bandAlbumRepository.Save();

                var albumToReturn = _mapper.Map<AlbumDto>(albumToAdd);
                return CreatedAtRoute("GetAlbumForBand", new  {bandId = bandId, albumId = albumToReturn.Id}, albumToReturn);
                
            }

            _mapper.Map(album, albumsFromRepo);
            _bandAlbumRepository.UpdateAlbum(albumsFromRepo);
            _bandAlbumRepository.Save();
            return NoContent();
        }

        [HttpPatch("{albumId}")]
        public ActionResult PartiallyUpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] JsonPatchDocument<AlbumForUpdatingDto> patchDocument)
        {
             if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumsFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if(albumsFromRepo == null)
            {
                var albumDto = new AlbumForUpdatingDto();
                patchDocument.ApplyTo(albumDto);
                var albumToAdd = _mapper.Map<Album>(albumDto);
                albumToAdd.Id = albumId;

                _bandAlbumRepository.AddAlbum(bandId, albumToAdd);
                _bandAlbumRepository.Save();

                var albumToReturn = _mapper.Map<AlbumDto>(albumToAdd);

                return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id}, albumToReturn);
            }

            var albumToPatch = _mapper.Map<AlbumForUpdatingDto>(albumsFromRepo);
            patchDocument.ApplyTo(albumToPatch, ModelState);

            if(!TryValidateModel(albumToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(albumToPatch, albumsFromRepo);
            _bandAlbumRepository.UpdateAlbum(albumsFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }
        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbumForBand(Guid bandId, Guid albumId)
        {
            if(!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            
            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if(albumFromRepo == null)
                return NotFound();
            _bandAlbumRepository.DeleteAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }
    }
}