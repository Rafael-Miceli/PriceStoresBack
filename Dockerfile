FROM microsoft/aspnetcore
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "api.dll"]