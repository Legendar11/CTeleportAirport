#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/AirportInfo/AirportInfo.Grpc/AirportInfo.Grpc.csproj", "Services/AirportInfo/AirportInfo.Grpc/"]
RUN dotnet restore "Services/AirportInfo/AirportInfo.Grpc/AirportInfo.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/AirportInfo/AirportInfo.Grpc"
RUN dotnet build "AirportInfo.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirportInfo.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirportInfo.Grpc.dll"]