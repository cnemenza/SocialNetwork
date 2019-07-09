using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using SOCIALNETWORK.WEB.Services.Handlers.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers.Implementations
{
    public class StudyCenterHandler : IStudyCenterHandler
    {
        private readonly IApiHandler _apiHandler;

        public StudyCenterHandler(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public async Task<List<DropdownModel>> GetDegreesByStudyCenterToListAsync(Guid studyCenterId)
            => await _apiHandler.GetAsync<List<DropdownModel>>(String.Format(ConstantsHelpers.Api.StudyCenter.DEGREELIST, studyCenterId));

        public async Task<List<DropdownModel>> GetStudyCentersToListAsync()
            => await _apiHandler.GetAsync<List<DropdownModel>>(ConstantsHelpers.Api.StudyCenter.STUDYCENTERLIST);
    }
}