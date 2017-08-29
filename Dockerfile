FROM microsoft/aspnetcore:latest
WORKDIR /app
COPY bin/Release/netcoreapp2.0 .
ENTRYPOINT ["dotnet", "Api.dll"]