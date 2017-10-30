FROM microsoft/aspnetcore-build:latest AS build-env
WORKDIR /app

COPY Api.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -o /publish

# Runtime Image Stage
FROM microsoft/aspnetcore:latest
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Api.dll"]

#Command with compose
# docker-compose -f docker-compose.yml -f docker-compose.prod.yml up --force-recreate --build