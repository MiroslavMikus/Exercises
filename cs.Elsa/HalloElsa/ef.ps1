$Env:EF_CONNECTIONSTRING = "Data Source=c:\temp\elsa.db;Cache=Shared;"
[Environment]::SetEnvironmentVariable("EF_CONNECTIONSTRING", "Data Source=c:\temp\elsa.db;Cache=Shared;", "Machine")
SET EF_CONNECTIONSTRING="Data Source=c:\temp\elsa.db;Cache=Shared;"
dotnet ef database update --context SqliteContext
