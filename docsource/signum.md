## Overview

The Signum Certificate Store Type in Keyfactor Command is designed to facilitate the management and automation of cryptographic certificates within the Signum platform. Specifically, it defines how certificates are stored, managed, and retrieved from Signum endpoints using the Keyfactor Universal Orchestrator extension.

### What Does the Certificate Store Type Do?
The Certificate Store Type enables Keyfactor Command to interface with the Signum platform, allowing users to perform various certificate management tasks such as inventory, adding, and removing certificates. It streamlines the process of handling certificates, ensuring consistency and reliability in certificate operations.

### What Does It Represent?
In this context, the Certificate Store Type represents a configuration that maps to endpoints within the Signum platform. These endpoints are defined to handle certificates and integrate with the Signum SOAP API for secure communications and operations.

### Caveats
One significant caveat to note is that the integration utilizes Basic Authentication when consuming the Signum SOAP API library. Therefore, it's crucial to ensure that the credentials used possess the appropriate authorization to execute the required Signum SOAP endpoints.

### SDK Usage
The Signum Certificate Store Type does not rely on an SDK. Instead, it uses direct SOAP API calls for its operations. This simplifies the integration but also underscores the importance of accurate endpoint configuration.

### Significant Limitations or Areas for Confusion
There are a few key areas to be aware of:
- The `Store Path` parameter is fixed and hardcoded to `NA`, representing "not applicable."
- The Store relies on a base URL configuration, which typically follows the pattern `https://{base url for your signum install}:8888/rtadminservice.svc/basic`, and uses Basic Authentication only.
- Custom capability is not enabled, limiting flexibility for advanced customizations without altering the base configuration significantly.

Understanding these key points and configuring the Certificate Store Type accordingly will help avoid potential pitfalls and ensure smooth operation within the Signum platform.

## Requirements

### Signum Orchestrator Extension Installation
1. Create the Signum certificate store type manually in Keyfactor Command by clicking on Settings (the gear icon on the top right) => Certificate Store Types => Add and then entering the settings described in the next section - Certificate Store Type Settings, OR by utilizing the CURL script found under the Certificate Store Type CURL Script folder in this repo. 
2. Stop the Keyfactor Universal Orchestrator Service for the orchestrator you plan to install this extension to run on.
3. In the Keyfactor Orchestrator installation folder (by convention usually C:\Program Files\Keyfactor\Keyfactor Orchestrator), find the "Extensions" folder. Underneath that, create a new folder named "Signum" (you may choose to use a different name if you wish).
4. Download the latest version of the Signum Orchestrator Extension from [GitHub](https://github.com/Keyfactor/signum-orchestrator).  Click on the "Latest" release link on the right hand side of the main page and download the first zip file.
5. Copy the contents of the download installation zip file to the folder created in Step 3.
6. (Optional) If you decide to create the certificate store type with a short name different than the suggested value of "Signum", edit the manifest.json file in the folder you created in step 3, and modify each "ShortName" in each "Certstores.{ShortName}.{Operation}" line with the ShortName you used to create the certificate store type in Keyfactor Command.  If you created it with the suggested value, this step can be skipped.
7. Start the Keyfactor Universal Orchestrator Service.
8. In Keyfactor Command, go to Orchestrators => Management and approve the Keyfactor Orchestrator containing the Signum capability that you just installed by selecting the orchestrator and clicking APPROVE.
&nbsp;  
&nbsp;

### Certificate Store Type Settings
Below are the values you need to enter if you choose to manually create the Signum certificate store type in the Keyfactor Command UI (related to Step 1 of Signum Orchestrator Extension Installation above).  

*Basic Tab:*
- **Name** – Required. The display name you wish to use for the new certificate store type.  Suggested value - Signum
- **ShortName** - Required. Suggested value - Signum.  If you choose to use a different value, please refer to Step 6 under Signum Orchestrator Extension Installation above.
- **Custom Capability** - Unchecked
- **Supported Job Types** - Inventory is the only one that should be checked.
- **Needs Server** - Checked
- **Blueprint Allowed** - Checked if you wish to make use of blueprinting.  Please refer to the Keyfactor Command Reference Guide for more details on this feature.
- **Uses PoserShell** - Unchecked
- **Requires Store Password** - Unchecked.
- **Supports Entry Password** - Unchecked.  

*Advanced Tab:*  
- **Store Path Type** - Fixed (with a value of NA underneath to represent "not applicable")
- **Supports Custom Alias** - Required
- **Private Key Handling** - Required
- **PFX Password Style** - Default  

*Custom Fields Tab:*
None

*Entry Parameters:*
None
&nbsp;  
&nbsp;

