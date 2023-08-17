using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

using SignumReference;

namespace Keyfactor.Extensions.Orchestrator.Signum
{
    internal class SignumClient
    {
        private string EndpointURL { get; set; }
        private string UserId { get; set; }
        private string Password { get; set; }

        internal SignumClient(string endpointURL, string userId, string password)
        {

        }
    }
}
