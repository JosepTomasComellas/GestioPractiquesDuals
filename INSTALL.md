# INSTALL

Versió actual del producte: `V:0.3.6`

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

- [docker-compose.yml](deploy/docker/docker-compose.yml)

Des de `D:\Codex\Duals\deploy\docker`:

```powershell
docker compose up --build
```

## Desplegament en LXC de Proxmox

Per a entorns LXC es treballara sobre la ruta acordada:

- `/docker/GestioPractiquesDuals`

Script de desplegament/actualitzacio:

- [deploy-lxc.sh](deploy/docker/deploy-lxc.sh)
- [fitxer .env d'exemple](deploy/docker/.env.example)

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

## Fitxer `.env`

El desplegament llegeix les variables de configuració externes des de:

- `/docker/GestioPractiquesDuals/deploy/docker/.env`

Si aquest fitxer no existeix, el script el crearà automàticament a partir de:

- `deploy/docker/.env.example`

Variables especialment importants:

- `SCHOOL_EMAIL_DOMAIN=sarria.salesians.cat`
- `BOOTSTRAP_ADMIN_EMAIL=admin.duals@sarria.salesians.cat`
- `BOOTSTRAP_ADMIN_DISPLAY_NAME=Administrador Duals`
- `BOOTSTRAP_ADMIN_PASSWORD=...`

Els certificats de `deploy/docker/certs/` no es versionen. Pots mantenir-hi `fullchain.pem` i `privkey.pem` locals sense que interfereixin amb les actualitzacions del repositori.

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

## Migracions i identitat

La web aplica migracions de base de dades a l'arrencada i sembra els rols i usuaris inicials.

Administrador inicial:

- correu: valor de `BOOTSTRAP_ADMIN_EMAIL`
- contrasenya: valor de `BOOTSTRAP_ADMIN_PASSWORD`

Perquè la web pugui arrencar, `PostgreSQL` ha d'estar disponible i accessible amb la `connection string` configurada.

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
- `V:0.2.0`: s'obre la fase 2 amb millora visual, favicon i base de rols.
- `V:0.2.1`: el dashboard s'apropa a l'estil AutoCo i prepara millor el shell funcional.
- `V:0.3.0`: s'activa identitat amb `ASP.NET Core Identity`, migracions i login inicial.
- `V:0.3.1`: l'administrador bootstrap i el domini escolar passen a configuració externa via `.env`.
- `V:0.3.2`: els certificats es mantenen fora de Git i el `deploy-lxc.sh` local es sobreescriu abans de cada `pull`.
- `V:0.3.3`: la redirecció d'autenticació de la web es fixa a `/login` en lloc de `/Account/Login`.
- `V:0.3.4`: es preserva el port extern del reverse proxy amb `ForwardedHeaders` i `Host` reenviat complet.
- `V:0.3.5`: es refà el login amb `POST` real al servidor i una interfície més propera a la referència AutoCo.
- `V:0.3.6`: la redirecció al login passa a ser relativa i preserva el host/port originals del navegador.

## Estat actual

- documentacio base creada
- esquelet de fase 1 compilable
- fase 2 amb dashboard API i autenticacio inicial
- proves base en verd
- remot GitHub configurat
