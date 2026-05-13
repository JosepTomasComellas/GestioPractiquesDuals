FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/GestioPractiquesDuals.Web/GestioPractiquesDuals.Web.csproj", "src/GestioPractiquesDuals.Web/"]
COPY ["src/GestioPractiquesDuals.Application/GestioPractiquesDuals.Application.csproj", "src/GestioPractiquesDuals.Application/"]
COPY ["src/GestioPractiquesDuals.Shared/GestioPractiquesDuals.Shared.csproj", "src/GestioPractiquesDuals.Shared/"]
RUN dotnet restore "src/GestioPractiquesDuals.Web/GestioPractiquesDuals.Web.csproj"

COPY . .
RUN dotnet publish "src/GestioPractiquesDuals.Web/GestioPractiquesDuals.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "GestioPractiquesDuals.Web.dll"]
