# Project Title

Full-Stack test with c# and VueJS

## Run Dev

Server Side .NET c#: /testApp
LOCAL: http://localhost:5172/

```bash
cd /testApp
dotnet run
```

Client Side VueJS 3: /client
LOCAL: http://localhost:4141/

```bash
cd /client
npm i
npm run serve
```

SQL Server: Azure-hosted
The full connection string is on /testApp/appsettings.json (need to change the PASSWORD keyword)
DB is IP restricted, So either I open it up for a specific one, or build your own table on your server with this command, and replace the DefaultConnection string on the /testApp/appsettings.json file.

```bash
CREATE TABLE [dbo].[Apps](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nickname] [varchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Apps] ADD PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
```
