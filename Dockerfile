# Use the official ASP.NET Core runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["LibraryManager.Api/LibraryManager.Api.csproj", "LibraryManager.Api/"]
COPY ["LibraryManager.Infrastructure/LibraryManager.Infrastructure.csproj", "LibraryManager.Infrastructure/"]
COPY ["LibraryManager.Domain/LibraryManager.Domain.csproj", "LibraryManager.Domain/"]
RUN dotnet restore "LibraryManager.Api/LibraryManager.Api.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/LibraryManager.Api"
RUN dotnet build "LibraryManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LibraryManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:$PORT
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LibraryManager.Api.dll"]
