using KafeinPortal.Data.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafeinPortal.Core.Helpers
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(UserLogin user);
        
    }
}
