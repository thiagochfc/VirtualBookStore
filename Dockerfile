FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY *.sln .

COPY /src ./src

COPY /tests ./tests

COPY /scripts ./scripts

RUN dotnet restore VirtualBookstore.sln

RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

RUN apt-get update && apt-get install -y wget

EXPOSE 8080

ENTRYPOINT ["dotnet", "VirtualBookstore.WebApi.dll"]