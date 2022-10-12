# Assumes the build context directory is the repo root

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AV.AvA.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["src/AV.AvA.Common/AV.AvA.Common.csproj", "src/AV.AvA.Common/"]
COPY ["src/AV.AvA.Data/AV.AvA.Data.csproj", "src/AV.AvA.Data/"]
COPY ["src/AV.AvA.Model/AV.AvA.Model.csproj", "src/AV.AvA.Model/"]
COPY ["src/AV.AvA.StorageModel/AV.AvA.StorageModel.csproj", "src/AV.AvA.StorageModel/"]
COPY ["src/AV.AvA.BackupTool/AV.AvA.BackupTool.csproj", "src/AV.AvA.BackupTool/"]
RUN dotnet restore "src/AV.AvA.BackupTool/AV.AvA.BackupTool.csproj"
COPY . .
WORKDIR "/src/src/AV.AvA.BackupTool"
RUN dotnet build "AV.AvA.BackupTool.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "AV.AvA.BackupTool.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AV.AvA.BackupTool.dll"]
