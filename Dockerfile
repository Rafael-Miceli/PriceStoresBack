FROM microsoft/aspnetcore-build:2.0 AS build-env

WORKDIR /src

COPY *.sln ./

COPY lib/CustomConfigDockerSecrets.csproj lib/

COPY src/Api.csproj src/

COPY tests/Unit/UnitTests.csproj tests/Unit/

COPY tests/Integration/IntegrationTests.csproj tests/Integration/


RUN dotnet restore 

COPY . .

WORKDIR /src/src

RUN dotnet publish --no-restore -c Release -o /app

#RUN ls -alR

FROM microsoft/aspnetcore:2.0 AS runtime-env
WORKDIR /app
COPY --from=build-env /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll"]