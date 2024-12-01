# Use the official SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID
WORKDIR /src

# Copy and build the application
COPY . .
RUN dotnet restore "./Drivers.Api/Drivers.Api.csproj"
COPY . .
RUN dotnet build "./Drivers.Api/Drivers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Drivers.Api/Drivers.Api.csproj" -c Release -o /app/publish

# Final image for running the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Drivers.Api.dll"]
