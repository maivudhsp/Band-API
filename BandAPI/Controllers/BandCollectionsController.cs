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
    [Route("api/bandcollections")]
    public class BandCollectionsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        public BandCollectionsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? 
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet("{ids}", Name="GetBandCollection")]
        public IActionResult GetBandCollection([FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if(ids == null)
                return BadRequest();
            var bandEntities = _bandAlbumRepository.GetBands(ids);
            if(ids.Count() != bandEntities.Count())
                return NotFound();

            var bandToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);
            return Ok(bandToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<BandDto>> CreateBandCollection([FromBody]
        IEnumerable<BandForCreatingDto> bandCollection)
        {
            var bandEntities = _mapper.Map<IEnumerable<Band>>(bandCollection);
            foreach(var band in bandEntities) {
                _bandAlbumRepository.Addband(band);
            }

            _bandAlbumRepository.Save();
            var bandCollectiontoReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);
            var IdsString = string.Join(",", bandCollectiontoReturn.Select(a => a.Id));
            return CreatedAtRoute("GetBandCollection", new  { ids = IdsString}, bandCollectiontoReturn);
        }
    

    }
}