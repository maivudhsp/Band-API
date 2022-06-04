using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BandAPI.Entities;
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
        private IPropertyMappingService _propertyMappingService;
        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _bandAlbumRepository = bandAlbumRepository ?? 
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        [HttpGet(Name = "GetBands")]
        [HttpHead]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandResourceParameters bandResourceParameters)
        {
            if(!_propertyMappingService.ValidMappingExists<BandDto, Band>(bandResourceParameters.OrderBy));
            var bandsFromRepo =_bandAlbumRepository.GetBands(bandResourceParameters);
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
            var previousPageLink = bandsFromRepo.HasPrevious ? CreateBandsUri(bandResourceParameters, UriType.PreviousPage) : null;
            var nextPageLink = bandsFromRepo.HasNext ? CreateBandsUri(bandResourceParameters, UriType.NextPage) : null;
            var metaData = new 
            {
                totalCount = bandsFromRepo.TotalCount,
                pageSize = bandsFromRepo.PageSize,
                currentPage = bandsFromRepo.CurrentPage,
                totalPages = bandsFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("pagination", JsonSerializer.Serialize(metaData));
            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
        }
        [HttpGet("{bandId}", Name="GetBand")]
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
        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody]BandForCreatingDto band)
        {
            var bandEntity = _mapper.Map<Band>(band);
            _bandAlbumRepository.Addband(bandEntity);

            _bandAlbumRepository.Save();
            var bandToReturn = _mapper.Map<BandDto>(bandEntity);

            return CreatedAtRoute("GetBand", new { bandId = bandToReturn.Id }, bandToReturn);
        }

        [HttpOptions]
        public IActionResult getBandsOptions()
        {
            Response.Headers.Add("Allow", "Get, Post, Delete, Head, Options");
            return Ok();
        }

        [HttpDelete("{bandId}")]
        public ActionResult DeleteBand(Guid bandId)
        {
            var bandFromRepo = _bandAlbumRepository.GetBand(bandId);
             if(bandFromRepo == null)
                return NotFound();

            _bandAlbumRepository.DeleteBand(bandFromRepo);

            return NoContent();
        }

        private string CreateBandsUri(BandResourceParameters bandResourceParameters, UriType uriType)
        {
            switch(uriType)
            {
                case UriType.PreviousPage:
                    return Url.Link("GetBands", new
                    {
                        pageNumber = bandResourceParameters.PageNumber - 1,
                        pageSize = bandResourceParameters.PageSize,
                        mainGenre = bandResourceParameters.MainGenre,
                        searchQuery = bandResourceParameters.SearchQuery,
                        orderBy = bandResourceParameters.OrderBy
                    });
                case UriType.NextPage:
                    return Url.Link("GetBands", new
                    {
                        pageNumber = bandResourceParameters.PageNumber + 1,
                        pageSize = bandResourceParameters.PageSize,
                        mainGenre = bandResourceParameters.MainGenre,
                        searchQuery = bandResourceParameters.SearchQuery,
                        orderBy = bandResourceParameters.OrderBy
                    });
                default:
                    return Url.Link("GetBands", new
                    {
                        pageNumber = bandResourceParameters.PageNumber,
                        pageSize = bandResourceParameters.PageSize,
                        mainGenre = bandResourceParameters.MainGenre,
                        searchQuery = bandResourceParameters.SearchQuery,
                        orderBy = bandResourceParameters.OrderBy
                    });
            }
        }

    }
}