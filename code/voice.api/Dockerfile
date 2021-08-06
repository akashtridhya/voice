#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debuguing.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["voice.api/voice.api.csproj", "voice.api/"]
COPY ["voice.dapper/voice.dapper.csproj", "voice.dapper/"]
COPY ["voice.models/voice.models.csproj", "voice.models/"]
COPY ["voice.queue/voice.queue.csproj", "voice.queue/"]
COPY ["voice.files/voice.files.csproj", "voice.files/"]
COPY ["voice.logs/voice.logs.csproj", "voice.logs/"]
COPY ["voice.security/voice.security.csproj", "voice.security/"]
COPY ["voice.notify/voice.notify.csproj", "voice.notify/"]
RUN dotnet restore "voice.api/voice.api.csproj"
COPY . .
WORKDIR "/src/voice.api"
RUN dotnet build "voice.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "voice.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "voice.api.dll"]
