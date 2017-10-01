FROM microsoft/aspnetcore-build:2 AS build-env
WORKDIR /app

COPY src/Api.csproj ./src/
RUN dotnet restore src/Api.csproj

COPY tests/Unit/UnitTests.csproj ./tests/Unit/
RUN dotnet restore tests/Unit/UnitTests.csproj

#RUN ls -alR
COPY . .