using System;
using System.Linq;
using AutoMapper;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Location;

namespace WEBAPI.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;

        public LocationService(IMapper mapper, ApplicationDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public LocationViewModel Get(int id)
        {
            var location = _context.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null) throw new Exception("Координаты не найдены.");

            return _mapper.Map<Location, LocationViewModel>(location);
        }
        
        public void Create(CreateLocationViewModel model)
        {
            var location = _mapper.Map<CreateLocationViewModel, Location>(model);

            _context.Locations.Add(location);
            _context.SaveChanges();
        }
    }
}
