FROM mcr.microsoft.com/dotnet/sdk:6.0 as Build
WORKDIR /src


COPY CleanArchitectureInventory.Catalog.Domain/CleanArchitectureInventory.Catalog.Domain.csproj CleanArchitectureInventory.Catalog.Domain/
COPY CleanArchitectureInventory.Catalog.Application/CleanArchitectureInventory.Catalog.Application.csproj CleanArchitectureInventory.Catalog.Application/
COPY CleanArchitectureInventory.Catalog.Infrastructure/CleanArchitectureInventory.Catalog.Infrastructure.csproj  CleanArchitectureInventory.Catalog.Infrastructure/
COPY CleanArchitectureInventory.Catalog.API/CleanArchitectureInventory.Catalog.API.csproj CleanArchitectureInventory.Catalog.API/

RUN dotnet restore CleanArchitectureInventory.Catalog.API/CleanArchitectureInventory.Catalog.API.csproj
COPY . .
WORKDIR /src/CleanArchitectureInventory.Catalog.API

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 
WORKDIR /app
COPY --from=Build /app .
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT [ "dotnet","CleanArchitectureInventory.Catalog.API.dll" ]
