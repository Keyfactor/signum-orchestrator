<h1 align="center" style="border-bottom: none">
    Signum Universal Orchestrator Extension
</h1>

<p align="center">
  <!-- Badges -->
<img src="https://img.shields.io/badge/integration_status-production-3D1973?style=flat-square" alt="Integration Status: production" />
<a href="https://github.com/Keyfactor/signum-orchestrator/releases"><img src="https://img.shields.io/github/v/release/Keyfactor/signum-orchestrator?style=flat-square" alt="Release" /></a>
<img src="https://img.shields.io/github/issues/Keyfactor/signum-orchestrator?style=flat-square" alt="Issues" />
<img src="https://img.shields.io/github/downloads/Keyfactor/signum-orchestrator/total?style=flat-square&label=downloads&color=28B905" alt="GitHub Downloads (all assets, all releases)" />
</p>

<p align="center">
  <!-- TOC -->
  <a href="#support">
    <b>Support</b>
  </a>
  ·
  <a href="#installation">
    <b>Installation</b>
  </a>
  ·
  <a href="#license">
    <b>License</b>
  </a>
  ·
  <a href="https://github.com/orgs/Keyfactor/repositories?q=orchestrator">
    <b>Related Integrations</b>
  </a>
</p>


## Overview

The Signum Universal Orchestrator extension enables seamless integration between Keyfactor Command and the Signum platform for remote management of cryptographic certificates. Signum uses certificates for securing communications and ensuring the authenticity and integrity of data exchanged within its ecosystem.

In Keyfactor Command, defined Certificate Stores of the Certificate Store Type represent collections of certificates or a single certificate stored in a remote location. For the Signum integration, these Certificate Stores refer to endpoints within the Signum platform that handle certificates as specified in the integration configuration. This setup allows for efficient management, inventory, and automation of certificate-related operations within the Signum environment.

## Compatibility

This integration is compatible with Keyfactor Universal Orchestrator version 10.4.1 and later.

## Support
The Signum Universal Orchestrator extension is supported by Keyfactor for Keyfactor customers. If you have a support issue, please open a support ticket with your Keyfactor representative. If you have a support issue, please open a support ticket via the Keyfactor Support Portal at https://support.keyfactor.com. 
 
> To report a problem or suggest a new feature, use the **[Issues](../../issues)** tab. If you want to contribute actual bug fixes or proposed enhancements, use the **[Pull requests](../../pulls)** tab.

## Installation
Before installing the Signum Universal Orchestrator extension, it's recommended to install [kfutil](https://github.com/Keyfactor/kfutil). Kfutil is a command-line tool that simplifies the process of creating store types, installing extensions, and instantiating certificate stores in Keyfactor Command.


1. Follow the [requirements section](docs/signum.md#requirements) to configure a Service Account and grant necessary API permissions.

    <details><summary>Requirements</summary>

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



    </details>

2. Create Certificate Store Types for the Signum Orchestrator extension. 

    * **Using kfutil**:

        ```shell
        # Signum
        kfutil store-types create Signum
        ```

    * **Manually**:
        * [Signum](docs/signum.md#certificate-store-type-configuration)

3. Install the Signum Universal Orchestrator extension.
    
    * **Using kfutil**: On the server that that hosts the Universal Orchestrator, run the following command:

        ```shell
        # Windows Server
        kfutil orchestrator extension -e signum-orchestrator@latest --out "C:\Program Files\Keyfactor\Keyfactor Orchestrator\extensions"

        # Linux
        kfutil orchestrator extension -e signum-orchestrator@latest --out "/opt/keyfactor/orchestrator/extensions"
        ```

    * **Manually**: Follow the [official Command documentation](https://software.keyfactor.com/Core-OnPrem/Current/Content/InstallingAgents/NetCoreOrchestrator/CustomExtensions.htm?Highlight=extensions) to install the latest [Signum Universal Orchestrator extension](https://github.com/Keyfactor/signum-orchestrator/releases/latest).

4. Create new certificate stores in Keyfactor Command for the Sample Universal Orchestrator extension.

    * [Signum](docs/signum.md#certificate-store-configuration)



## License

Apache License 2.0, see [LICENSE](LICENSE).

## Related Integrations

See all [Keyfactor Universal Orchestrator extensions](https://github.com/orgs/Keyfactor/repositories?q=orchestrator).