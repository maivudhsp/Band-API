using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BandAPI.Helps;
using BandAPI.Models;
using BandAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? 
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<BandDto>> GetBands()
        {
            var bandsFromRepo =_bandAlbumRepository.GetBands();
            var bandsDto = new List<BandDto>();
            // foreach(var band in bandsFromRepo)
            // {
            //     bandsDto.Add(new BandDto()
            //     {
            //         Id = band.Id,
            //         Name = band.Name,
            //         MainGenre = band.MainGenre, 
            //         FoundYearsAgo = $"{band.Founded.ToString()} ({band.Founded.GetYearsAgo()})"

            //     });
            // }
            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
        }
        [HttpGet("{bandId}")]
        public IActionResult GetBand(Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }
            var bandFromRepo = _bandAlbumRepository.GetBand(bandId);
            if (bandFromRepo == null)
                return NotFound();
            return new JsonResult(bandFromRepo);
        }
    }
}