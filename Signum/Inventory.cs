using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

using Keyfactor.Logging;
using Keyfactor.Orchestrators.Common.Enums;
using Keyfactor.Orchestrators.Extensions;
using Keyfactor.Orchestrators.Extensions.Interfaces;
using Microsoft.Extensions.Logging;
using SignumReference;

namespace Keyfactor.Extensions.Orchestrator.Signum
{
    public class Inventory : IInventoryJobExtension
    {
        public string ExtensionName => "";

        public IPAMSecretResolver _resolver;

        public JobResult ProcessJob(InventoryJobConfiguration config, SubmitInventoryUpdate submitInventory)
        {
            ILogger logger = LogHandler.GetClassLogger(this.GetType());
            logger.LogDebug($"Begin {config.Capability} for job id {config.JobId}...");
            logger.LogDebug($"Server: {config.CertificateStoreDetails.ClientMachine}");
            logger.LogDebug($"Store Path: {config.CertificateStoreDetails.StorePath}");
            logger.LogDebug($"Job Properties:");
            foreach (KeyValuePair<string, object> keyValue in config.JobProperties ?? new Dictionary<string, object>())
            {
                logger.LogDebug($"    {keyValue.Key}: {keyValue.Value}");
            }

            List<CurrentInventoryItem> inventoryItems = new List<CurrentInventoryItem>();

            try
            {
                string userName = PAMUtilities.ResolvePAMField(_resolver, logger, "Server User Name", config.ServerUsername);
                string userPassword = PAMUtilities.ResolvePAMField(_resolver, logger, "Server Password", config.ServerPassword);

                SignumClient client = new SignumClient(config.CertificateStoreDetails.ClientMachine, config.ServerUsername, config.ServerPassword);

                foreach (int id in client.GetCertificateIds())
                {
                    inventoryItems.Add(new CurrentInventoryItem()
                    {
                        ItemStatus = OrchestratorInventoryItemStatus.Unknown,
                        Alias = id.ToString(),
                        //PrivateKeyEntry = issuedCertificate.HasPrivateKey,
                        UseChainLevel = false,
                        Certificates = new List<string>() { Convert.ToBase64String(client.GetCertificate(id).Export(X509ContentType.Cert)) }
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception for {config.Capability}: {SignumException.FlattenExceptionMessages(ex, string.Empty)} for job id {config.JobId}");
                return new JobResult() { Result = Keyfactor.Orchestrators.Common.Enums.OrchestratorJobStatusJobResult.Failure, JobHistoryId = config.JobHistoryId, FailureMessage = "Custom message you want to show to show up as the error message in Job History in KF Command" };
            }

            try
            {
                submitInventory.Invoke(inventoryItems);
                return new JobResult() { Result = Keyfactor.Orchestrators.Common.Enums.OrchestratorJobStatusJobResult.Success, JobHistoryId = config.JobHistoryId };
            }
            catch (Exception ex)
            {
                return new JobResult() { Result = Keyfactor.Orchestrators.Common.Enums.OrchestratorJobStatusJobResult.Failure, JobHistoryId = config.JobHistoryId, FailureMessage = "Custom message you want to show to show up as the error message in Job History in KF Command" };
            }
        }
    }
}