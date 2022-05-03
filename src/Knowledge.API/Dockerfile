FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Knowledge.API/Knowledge.API.csproj", "src/Knowledge.API/"]
RUN dotnet restore "src/Knowledge.API/Knowledge.API.csproj"
COPY . .
WORKDIR "/src/src/Knowledge.API"
RUN dotnet build "Knowledge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Knowledge.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Knowledge.API.dll"]