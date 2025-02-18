<!-- add integration specific information below -->
## Versioning

The version number of a the Signum Orchestrator Extension can be verified by right clicking on the Signum.dll file in the Extensions/Signum installation folder, selecting Properties, and then clicking on the Details tab.
&nbsp;  
&nbsp; 
## Signum Orchestrator Extension Installation
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
## Certificate Store Type Settings
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
## Creating an Signum Certificate Store in Keyfactor Command  
To create a Keyfactor Command certificate store of certificate store type Signum, go to Locations => Certificate Stores and click ADD.  Then enter the following:  
- Category - Signum (or the alternate ShortName value you entered when creating your certificate store type).
- Container - Optional.  Refer to Keyfactor Command documentation about this feature.
- Client Machine - The URL that will be used as the base URL for Signum endpoint calls.  Should be something like https://{base url for your signum install}/rtadminservice.svc/basic. The API service port can be configured so yours may use something other than default https/443.  The "/basic" at the end is required, as this integration makes use of Basic Authentication only when consuming the Signum SOAP API library.- Store Path - Not used and hardcoded to NA for "not applicable"
- Server Username and Server Password - The id/password credentials that have authorization to execute Signum SOAP endpoints in your Signum environment.
### License
[Apache](https://apache.org/licenses/LICENSE-2.0)

