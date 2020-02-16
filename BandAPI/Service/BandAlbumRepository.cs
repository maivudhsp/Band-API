using BandAPI.DbContexts;
using BandAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Service
{
    public class BandAlbumRepository : IBandAlbumRepository
    {
        private readonly BandAlbumContext _context;
        public BandAlbumRepository(BandAlbumContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddAlbum(Guid bandId, Album album)
        {
            if(bandId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bandId));
            }
            if(album == null)
                throw new ArgumentNullException(nameof(album));
            album.BandId = bandId;
            _context.Albums.Add(album);
        }

        public void Addband(Band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));
            _context.Bands.Add(band);
        }

        public bool AlbumExists(Guid albumId)
        {
            if (albumId == Guid.Empty)
                throw new ArgumentNullException(nameof(albumId));
            return _context.Albums.Any(a => a.Id == albumId);
        }

        public bool BandExists(Guid bandId)
        {
            if (bandId == Guid.Empty)
                throw new ArgumentNullException(nameof(bandId));
            return _context.Bands.Any(b => b.Id == bandId);
        }

        public void DeleteAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));
            _context.Albums.Remove(album);
        }

        public void DeleteBand(Band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));
            _context.Bands.Remove(band);
        }

        public Album GetAlbum(Guid bandId, Guid albumId)
        {
            if (bandId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bandId));
            }
            if (albumId == null)
                throw new ArgumentNullException(nameof(albumId));
            return _context.Albums.Where(a => a.BandId == bandId && a.Id == albumId).FirstOrDefault();

        }

        public IEnumerable<Album> GetAlbums(Guid bandId)
        {
            throw new NotImplementedException();
        }

        public Band GetBand(Guid bandId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Band> GetBands()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Band> GetBands(IEnumerable<Guid> bandId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateAlbum(Album album)
        {
            throw new NotImplementedException();
        }

        public void UpdateBand(Band band)
        {
            throw new NotImplementedException();
        }
    }
}
