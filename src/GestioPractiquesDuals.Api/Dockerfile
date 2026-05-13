FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/GestioPractiquesDuals.Api/GestioPractiquesDuals.Api.csproj", "src/GestioPractiquesDuals.Api/"]
COPY ["src/GestioPractiquesDuals.Application/GestioPractiquesDuals.Application.csproj", "src/GestioPractiquesDuals.Application/"]
COPY ["src/GestioPractiquesDuals.Domain/GestioPractiquesDuals.Domain.csproj", "src/GestioPractiquesDuals.Domain/"]
COPY ["src/GestioPractiquesDuals.Infrastructure/GestioPractiquesDuals.Infrastructure.csproj", "src/GestioPractiquesDuals.Infrastructure/"]
COPY ["src/GestioPractiquesDuals.Shared/GestioPractiquesDuals.Shared.csproj", "src/GestioPractiquesDuals.Shared/"]
RUN dotnet restore "src/GestioPractiquesDuals.Api/GestioPractiquesDuals.Api.csproj"

COPY . .
RUN dotnet publish "src/GestioPractiquesDuals.Api/GestioPractiquesDuals.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "GestioPractiquesDuals.Api.dll"]
