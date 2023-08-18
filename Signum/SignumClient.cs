﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

using SignumReference;
using System.ServiceModel.Dispatcher;

namespace Keyfactor.Extensions.Orchestrator.Signum
{
    internal class SignumClient
    {
        private string EndpointURL { get; set; }
        private string UserId { get; set; }
        private string Password { get; set; }

        private RTAdminServiceClient Client { get; set; }

        internal SignumClient(string endpointURL, string userId, string password)
        {
            EndpointURL = endpointURL;
            UserId = userId;
            Password = password;

            InitializeSoapClient();
        }

        private List<int> GetCertificateIds()
        {
            List<int> certificateIds = new List<int>();
            var result = Task.Run(async () => await Client.ListCertificateIdsAsync(CertificatesViewType.ViewAll, String.Empty, null, null)).Result;
            if (result.ResultStatus != ResultStatus.ERROR)
            {
                certificateIds = new List<int>((int[])result.ResultData);
            }
            else
            {
                throw new SignumException(result.ResultMessage)
            }

            return certificateIds;
        }

        private void InitializeSoapClient()
        {
            CustomBinding binding = new CustomBinding();

            binding.Name = "basic";
            var sbe = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            sbe.SecurityHeaderLayout = SecurityHeaderLayout.Strict;
            sbe.IncludeTimestamp = false;
            sbe.KeyEntropyMode = System.ServiceModel.Security.SecurityKeyEntropyMode.ServerEntropy;
            binding.Elements.Add(sbe);

            var msgEnc = new MtomMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, System.Text.Encoding.UTF8);
            msgEnc.ReaderQuotas.MaxBytesPerRead = 2147483647;
            msgEnc.ReaderQuotas.MaxStringContentLength = 2147483647;
            msgEnc.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            msgEnc.ReaderQuotas.MaxArrayLength = 2147483647;
            msgEnc.MaxBufferSize = 2147483647;
            binding.Elements.Add(msgEnc);

            var httpsTrans = new HttpsTransportBindingElement();
            httpsTrans.MaxReceivedMessageSize = 2147483647L;
            httpsTrans.TransferMode = TransferMode.Streamed;
            binding.Elements.Add(httpsTrans);

            Client = new RTAdminServiceClient(binding, new EndpointAddress(EndpointURL));
        }
    }
}
