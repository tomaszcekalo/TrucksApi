# TrucksApi
Solution to assignment, which you can find here:
https://coldrun.notion.site/NET-Developer-2188b313494546f0b2aae6660a774692

# How to run?
* If database doesn't exist: Tools -> NuGet Package Manager -> Package Manager Console
* Execute dotnet ef database update --project TrucksApi\TrucksApi.csproj
* If you get "dotnet : Could not execute because the specified command or file was not found.", then install ef core tools by executing "dotnet tool install --global dotnet-ef" first
* Hit F5

In case of database corruption, delete Trucks.db and apply migrations again.
