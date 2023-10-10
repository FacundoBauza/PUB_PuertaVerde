#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi-PUB_PV/WebApi-PUB_PV.csproj", "WebApi-PUB_PV/"]
RUN dotnet restore "WebApi-PUB_PV/WebApi-PUB_PV.csproj"
COPY . .
WORKDIR "/src/WebApi-PUB_PV"
RUN dotnet build "WebApi-PUB_PV.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi-PUB_PV.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi-PUB_PV.dll"]