#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PackageApi/PackageApi.csproj", "PackageApi/"]
COPY ["PackageApi.Infrastructure/PackageApi.Infrastructure.csproj", "PackageApi.Infrastructure/"]
RUN dotnet restore "PackageApi/PackageApi.csproj"
COPY . .
WORKDIR "/src/PackageApi"
RUN dotnet build "PackageApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PackageApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PackageApi.dll"]