#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AV.AVA.StorageBackend/AV.AVA.StorageBackend.csproj", "src/AV.AVA.StorageBackend/"]
RUN dotnet restore "src/AV.AVA.StorageBackend/AV.AVA.StorageBackend.csproj"
COPY . .
WORKDIR "/src/src/AV.AVA.StorageBackend"
RUN dotnet build "AV.AVA.StorageBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AV.AVA.StorageBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AV.AVA.StorageBackend.dll"]