#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Airport.Api/Airport.Api.csproj", "Services/Airport.Api/"]
RUN dotnet restore "Services/Airport.Api/Airport.Api.csproj"
COPY . .
WORKDIR "/src/Services/Airport.Api"
RUN dotnet build "Airport.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Airport.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Airport.Api.dll"]