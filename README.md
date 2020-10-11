# LibraryManagement

Getting started

1. Change connection string in appsettings.json to respective sql server instance
2. Run Update-Database if VS2019 , if .Net CLi run dotnet ef database update after installing ef core tools
3. Run the application(dotnet run if .Net cli) to seed initial data
4. Hit login api to get user token , only the specified users from ASPNetUser tables must be used
5. Set this token in authorisation in Swagger UI - Bearer $token
