using System.Linq;
using AutoMapper;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.TableSettings;

namespace WEBAPI.Services.Implementations
{
    public class TableSettingsService : ITableSettingsService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;

        public TableSettingsService(ApplicationDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TableSettingsViewModel Get(int userId)
        {
            var model = _context.TableSettings.FirstOrDefault(x => x.UserId == userId);
            if (model == null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                user.TableSettings = new TableSettings();

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return _mapper.Map<TableSettings, TableSettingsViewModel>(model);
        }

        public void Update(UpdateTableSettingsViewModel model)
        {
            var tableSettings = _context.TableSettings.FirstOrDefault(x => x.UserId == model.UserId);
            var updatedModel = _mapper.Map<UpdateTableSettingsViewModel, TableSettings>(model, tableSettings);

            _context.TableSettings.Update(updatedModel);
            _context.SaveChanges();
        }
    }
}
