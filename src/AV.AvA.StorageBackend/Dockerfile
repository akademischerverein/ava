# Assumes the build context directory is the repo root
# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AV.AvA.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["src/AV.AvA.Common/AV.AvA.Common.csproj", "src/AV.AvA.Common/"]
COPY ["src/AV.AvA.Data/AV.AvA.Data.csproj", "src/AV.AvA.Data/"]
COPY ["src/AV.AvA.Model/AV.AvA.Model.csproj", "src/AV.AvA.Model/"]
COPY ["src/AV.AvA.StorageModel/AV.AvA.StorageModel.csproj", "src/AV.AvA.StorageModel/"]
COPY ["src/AV.AvA.StorageBackend/AV.AvA.StorageBackend.csproj", "src/AV.AvA.StorageBackend/"]
RUN dotnet restore "src/AV.AvA.StorageBackend/AV.AvA.StorageBackend.csproj"
COPY . .
WORKDIR "/src/src/AV.AvA.StorageBackend"
RUN dotnet build "AV.AvA.StorageBackend.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "AV.AvA.StorageBackend.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AV.AvA.StorageBackend.dll"]
