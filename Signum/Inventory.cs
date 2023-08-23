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

        public Inventory(IPAMSecretResolver resolver)
        {
            _resolver = resolver;
        }

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
                        PrivateKeyEntry = true,
                        UseChainLevel = false,
                        Certificates = new List<string>() { Convert.ToBase64String(client.GetCertificate(id).Export(X509ContentType.Cert)) }
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception for {config.Capability}: {SignumException.FlattenExceptionMessages(ex, string.Empty)} for job id {config.JobId}");
                return new JobResult() { Result = OrchestratorJobStatusJobResult.Failure, JobHistoryId = config.JobHistoryId, FailureMessage = SignumException.FlattenExceptionMessages(ex, $"Server {config.CertificateStoreDetails.ClientMachine}:") };
            }

            try
            {
                submitInventory.Invoke(inventoryItems);
                logger.LogDebug($"...End {config.Capability} job for job id {config.JobId}");
                return new JobResult() { Result = OrchestratorJobStatusJobResult.Success, JobHistoryId = config.JobHistoryId };
            }
            catch (Exception ex)
            {
                string errorMessage = SignumException.FlattenExceptionMessages(ex, string.Empty);
                logger.LogError($"Exception returning certificates for {config.Capability}: {errorMessage} for job id {config.JobId}");
                return new JobResult() { Result = OrchestratorJobStatusJobResult.Failure, JobHistoryId = config.JobHistoryId, FailureMessage = SignumException.FlattenExceptionMessages(ex, $"Server {config.CertificateStoreDetails.ClientMachine}:") };
            }
        }
    }
}