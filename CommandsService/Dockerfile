# Pull SDK to build .NET 5 app
# Build Release version as build-env
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Copy runtime version and entrypoint = dotnet CommandsService.dll
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "CommandsService.dll" ]