# ── Stage 1: Build ──────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file first (better caching)
COPY *.csproj .
RUN dotnet restore

# Copy all source code
COPY . .

# Build and publish
RUN dotnet publish -c Release -o /app/publish

# ── Stage 2: Runtime (small final image) ────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Port your app listens on
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "CrudApp.dll"]