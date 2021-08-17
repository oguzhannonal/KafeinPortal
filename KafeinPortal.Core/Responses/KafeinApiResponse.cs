using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace KafeinPortal.Core.Responses
{
    public class KafeinApiResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string organization { get; set; }
        public bool first_login { get; set; }
        public string jti { get; set; }
    }
}
