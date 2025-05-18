using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Interfaces
{
    public interface INavigationService
    {

        Task NavigateToAsync(string route);
        Task NavigateToAsync<T>(string route, IDictionary<string,object> parameters);
        Task GoBackAsync();
        
    }
}
