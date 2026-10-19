When deploying the FLTExportConsoleApp:

* update the .config file for DownloadPath; this should be the FLT Web Site download folder.

    <add key="DownloadPath" value="E:\Contoso\FLT\FLTExportConsoleApp\exports" />

* update the .config file for LogFile Path; this should be in the same location as the folder that holds the .exe and the .config files.

        <add key="ContosoLoggerTextFilePath" value="E:\Contoso\FLT\FLTExportConsoleApp\logs\FLTExport.log" />

* Add an entry to Credentials Manager on the server while logged in as the User that will be used for executing the console app in a scheduled task:

  + c:\users\{UserName}> cmdkey /generic:FLT.Export.Auth /user:{UserName} /pass:{Password}

  + NOTE: the User for the export will be the user account used for SQL Integrated Security - sqldb_flt. This user account is configured to not allow logins; it will need to be adjusted temporarily to allow logging into the Server so that the entry can be added to the Credentials Manager.

