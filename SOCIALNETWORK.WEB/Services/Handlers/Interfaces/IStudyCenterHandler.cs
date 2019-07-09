using SOCIALNETWORK.WEB.Services.Handlers.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.WEB.Services.Handlers.Interfaces
{
    public interface IStudyCenterHandler
    {
        Task<List<DropdownModel>> GetStudyCentersToListAsync();
        Task<List<DropdownModel>> GetDegreesByStudyCenterToListAsync(Guid studyCenterId);
    }
}
