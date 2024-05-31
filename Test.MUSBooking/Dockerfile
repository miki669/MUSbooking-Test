# Use the official .NET 8.0 ASP.NET runtime image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Use the .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Test.MUSBooking/Test.MUSBooking.csproj", "Test.MUSBooking/"]
COPY ["MUSbooking/MUSbooking.csproj", "MUSbooking/"]
RUN dotnet restore "Test.MUSBooking/Test.MUSBooking.csproj"
COPY . .
WORKDIR "/src/Test.MUSBooking"
RUN dotnet build "Test.MUSBooking.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Test.MUSBooking.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.MUSBooking.dll"]
