FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY . .

RUN dotnet restore
WORKDIR /src/Velvetech.Presentation
RUN dotnet publish "Velvetech.Presentation.csproj" -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app .
WORKDIR /app
ENTRYPOINT ["dotnet", "Velvetech.Presentation.dll"]