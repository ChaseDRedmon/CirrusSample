FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CirrusTest/CirrusTest.csproj", "CirrusTest/"]
RUN dotnet restore "CirrusTest/CirrusTest.csproj"
COPY . .
WORKDIR "/src/CirrusTest"
RUN dotnet build "CirrusTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CirrusTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CirrusTest.dll"]
