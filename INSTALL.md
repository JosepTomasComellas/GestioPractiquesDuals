# INSTALL

Versió actual del producte: `V:0.2.1`

Aquest document descriu la instal·lació i posada en marxa inicial de **Gestió de les pràctiques Duals**.

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

## Desplegament en LXC de Proxmox

Per a entorns LXC es treballara sobre la ruta acordada:

- `/docker/GestioPractiquesDuals`

Script de desplegament/actualitzacio:

- [deploy-lxc.sh](/D:/Codex/Duals/deploy/docker/deploy-lxc.sh)

Exemple d'us:

```bash
chmod +x /docker/GestioPractiquesDuals/deploy/docker/deploy-lxc.sh
/docker/GestioPractiquesDuals/deploy/docker/deploy-lxc.sh
```

Exemple clonant en una ruta concreta:

```bash
APP_DIR=/docker/GestioPractiquesDuals \
REPO_URL=https://github.com/JosepTomasComellas/GestioPractiquesDuals.git \
bash /docker/GestioPractiquesDuals/deploy/docker/deploy-lxc.sh
```

El script:

- valida `git` i `docker`;
- clona el repositori si encara no existeix;
- fa `fetch` i `pull` de `main`;
- comprova que existeixi `docker-compose.yml`;
- executa `docker compose up -d --build`;
- mostra l'estat final dels serveis.

## Serveis previstos

- `postgres`: base de dades principal
- `redis`: cache
- `api`: backend HTTP
- `web`: frontend Blazor
- `nginx`: reverse proxy d'entrada

## Ports

Configuracio actual recomanada:

- `nginx` publica `443:443` dins la maquina/LXC
- la publicacio externa cap a Internet es pot fer com `4444 -> 443`

Aixo permet que la solucio treballi internament en el port HTTPS estandard i que la redireccio externa es faci des del router, firewall o reverse proxy superior.

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

Canvi actual:

- `V:0.1.1`: s'afegeix script de desplegament/actualitzacio per entorn LXC.
- `V:0.1.2`: s'ajusta la ruta objectiu de desplegament a `/docker/GestioPractiquesDuals`.
- `V:0.1.3`: s'ajusta el desplegament per exposar el port 443 a la maquina/LXC.

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
