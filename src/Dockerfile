FROM microsoft/aspnetcore:latest AS build-env
WORKDIR /app

COPY /bin/Debug/netcoreapp2.0/publish .
#RUN dotnet restore

#COPY . .
#RUN dotnet publish -o /publish

# Runtime Image Stage
# FROM microsoft/aspnetcore:latest
# WORKDIR /publish
# COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Api.dll"]