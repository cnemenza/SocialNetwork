using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.WEB.Services.Handlers
{
    public interface IApiHandler
    {
        Task<T> GetAsync<T>(string uri);
        Task<T2> PostAsync<T1, T2>(T1 data, string uri);
    }
}
