using SOCIALNETWORK.API.Models.Common;
using SOCIALNETWORK.REPOSITORY.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SOCIALNETWORK.API.Controllers
{
    [RoutePrefix("api")]
    public class StudyCenterController : ApiController
    {
        [HttpGet]
        [Route("listar-centro-estudios")]
        public async Task<IHttpActionResult> GetStudyCenters()
        {
            using (var _context = new DatabaseContext())
            {
                var studyCenters = await _context.StudyCenters
                    .Select(x => new DropdownModel
                    {
                        Id = x.Id,
                        Text = x.Name
                    }).ToListAsync();

                return Ok(studyCenters);
            }
        }

        [HttpGet]
        [Route("listar-carreras")]
        public async Task<IHttpActionResult> GetDegreesByStudyCenter(Guid studyCenterId)
        {
            using (var _context = new DatabaseContext())
            {
                var degrees = await _context.Degrees
                    .Where(x=>x.StudyCenterId == studyCenterId)
                    .Select(x => new DropdownModel
                    {
                        Id = x.Id,
                        Text = x.Name
                    }).ToListAsync();

                return Ok(degrees);
            }
        }

    }
}