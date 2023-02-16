FROM mcr.microsoft.com/dotnet/sdk:6.0 As build
WORKDIR /src
COPY  CleanArchitectureInventory.Identity.IdentityServerAPI.csproj .
RUN dotnet restore 
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ENTRYPOINT [ "dotnet","CleanArchitectureInventory.Identity.IdentityServerAPI.dll" ]



