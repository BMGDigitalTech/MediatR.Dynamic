set version=1.0.0-alpha.1
dotnet build MediatR.Dynamic.csproj -c Release
nuget.exe pack MediatR.Dynamic.nuspec -OutputDirectory "..\..\..\..\LocalNugetFeed" -Version %version% -Properties Configuration=Release