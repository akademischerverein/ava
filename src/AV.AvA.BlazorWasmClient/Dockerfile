# Assumes the build context directory is the repo root

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AV.AvA.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["src/AV.AvA.BlazorWasmClient/AV.AvA.BlazorWasmClient.csproj", "src/AV.AvA.BlazorWasmClient/"]
COPY ["src/AV.AvA.Common/AV.AvA.Common.csproj", "src/AV.AvA.Common/"]
COPY ["src/AV.AvA.Model/AV.AvA.Model.csproj", "src/AV.AvA.Model/"]
RUN dotnet restore "src/AV.AvA.BlazorWasmClient/AV.AvA.BlazorWasmClient.csproj"
COPY . .
RUN dotnet build "src/AV.AvA.BlazorWasmClient/AV.AvA.BlazorWasmClient.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "src/AV.AvA.BlazorWasmClient/AV.AvA.BlazorWasmClient.csproj" -c Release -o /app/publish /p:RunAOTCompilation=true

FROM fholzer/nginx-brotli:latest AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY "src/AV.AvA.BlazorWasmClient/nginx.conf" /etc/nginx/nginx.conf
