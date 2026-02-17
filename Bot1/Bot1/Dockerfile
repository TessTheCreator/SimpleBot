FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build
WORKDIR /src

COPY . .

RUN dotnet publish "Bot1/Bot1/Bot1.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS final
WORKDIR /app

COPY --from=build /app/publish .
RUN mkdir -p /app/Data/DB

ENTRYPOINT ["dotnet", "Bot1.dll"]
