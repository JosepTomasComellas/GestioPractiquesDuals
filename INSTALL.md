# INSTALL

Versio actual del producte: `V:0.1.0`

Aquest document descriu la instal.lacio i posada en marxa inicial de **Gestio de les practiques Duals**.

## Requisits previs

- `Visual Studio 2026` o posterior, o `dotnet SDK 10`
- `Docker Desktop` o entorn Docker equivalent
- Git
- Acces a Internet per restaurar paquets NuGet

## Estructura base

La solucio principal es troba a:

- `D:\Codex\Duals\GestioPractiquesDuals.slnx`

## Execucio en desenvolupament

### 1. Restaurar i compilar

```powershell
dotnet restore D:\Codex\Duals\GestioPractiquesDuals.slnx
dotnet build D:\Codex\Duals\GestioPractiquesDuals.slnx
```

### 2. Executar proves

```powershell
dotnet test D:\Codex\Duals\GestioPractiquesDuals.slnx --no-build
```

### 3. Executar l'API

```powershell
dotnet run --project D:\Codex\Duals\src\GestioPractiquesDuals.Api
```

### 4. Executar la web

```powershell
dotnet run --project D:\Codex\Duals\src\GestioPractiquesDuals.Web
```

## Execucio amb Docker Compose

Fitxer principal:

- [docker-compose.yml](/D:/Codex/Duals/deploy/docker/docker-compose.yml)

Des de `D:\Codex\Duals\deploy\docker`:

```powershell
docker compose up --build
```

## Serveis previstos

- `postgres`: base de dades principal
- `redis`: cache
- `api`: backend HTTP
- `web`: frontend Blazor
- `nginx`: reverse proxy d'entrada

## Certificats

La carpeta `deploy/docker/certs` queda reservada per als certificats del reverse proxy. En l'estat actual hi ha un `.gitkeep`, i caldra afegir-hi els certificats reals en entorns de pilot o produccio.

## Configuracio de la base de dades

Motor inicial:

- `PostgreSQL`

Criteri:

- la base de dades comenca dins `docker compose`;
- la connexio esta desacoblada per `connection string`;
- el sistema queda preparat per moure la BBDD a una instancia externa de PostgreSQL sense canvis estructurals al codi.

## Versionat

El producte segueix el format `V:x.y.z`.

Quan es faci una nova fase o una modificacio rellevant, cal actualitzar:

- `README.md`
- `INSTALL.md`
- `Directory.Build.props`

## Publicacio a GitHub

Abans de poder pujar el repositori de forma continua, cal configurar el remot. Exemple orientatiu:

```powershell
git -C D:\Codex\Duals remote add origin https://github.com/JosepTomasComellas/<nom-repo>.git
git -C D:\Codex\Duals branch -M main
git -C D:\Codex\Duals push -u origin main
```

## Estat actual

- documentacio base creada
- esquelet de fase 1 compilable
- proves base en verd
- remot de GitHub encara no configurat
