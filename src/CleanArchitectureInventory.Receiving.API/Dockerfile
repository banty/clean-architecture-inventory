FROM mcr.microsoft.com/dotnet/sdk:6.0 As build
WORKDIR /src

COPY CleanArchitectureInventory.Receiving.API/CleanArchitectureInventory.Receiving.API.csproj CleanArchitectureInventory.Receiving.API/
COPY CleanArchitectureInventory.Receiving.Infrastructure/CleanArchitectureInventory.Receiving.Infrastructure.csproj CleanArchitectureInventory.Receiving.Infrastructure/
COPY CleanArchitectureInventory.Receiving.Applicaiton/CleanArchitectureInventory.Receiving.Applicaiton.csproj CleanArchitectureInventory.Receiving.Applicaiton/
COPY CleanArchitectureInventory.Receiving.Domain/CleanArchitectureInventory.Receiving.Domain.csproj CleanArchitectureInventory.Receiving.Domain/

RUN dotnet restore CleanArchitectureInventory.Receiving.API/CleanArchitectureInventory.Receiving.API.csproj

COPY . .
WORKDIR /src/CleanArchitectureInventory.Receiving.API



RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app .

EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet","CleanArchitectureInventory.Receiving.API.dll"]