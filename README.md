# Gestio de les practiques Duals

Versio actual del producte: `V:0.1.3`

Projecte per a la gestio de les practiques Duals dels cicles formatius, implementat amb `.NET 10`, `Blazor`, `API ASP.NET Core`, `PostgreSQL`, `Redis`, `Nginx`, `Docker Compose` i una capa preparada per `MCP`.

## Estat actual

- Fase en curs: `Fase 1`
- Estat: esquelet base creat i compilable
- Compatibilitat objectiu: `Visual Studio 2026` o posterior
- Base de dades inicial: `PostgreSQL`
- Criteri d'implementacio: simplicitat, mantenibilitat i portabilitat

## Estructura del repositori

- `src/`: projectes aplicatius
- `tests/`: proves automatitzades
- `deploy/`: `docker compose`, `nginx` i configuracio de desplegament
- `docs/`: documentacio funcional i de suport
- `Imatges/`: branding base del projecte
- `Exemples/`: fitxers originals d'exemple

## Projectes de la solucio

- `GestioPractiquesDuals.Web`: frontend Blazor amb MudBlazor
- `GestioPractiquesDuals.Api`: API HTTP
- `GestioPractiquesDuals.Application`: serveis d'aplicacio
- `GestioPractiquesDuals.Domain`: model de domini
- `GestioPractiquesDuals.Infrastructure`: persistencia i integracions
- `GestioPractiquesDuals.Shared`: constants i peces compartides
- `GestioPractiquesDuals.McpServer`: scaffold inicial per MCP

## Execucio local

Per la posada en marxa local consulta [INSTALL.md](/D:/Codex/Duals/INSTALL.md).

## Desplegament en LXC

Per al desplegament o actualitzacio en un contenidor LXC de Proxmox es disposa del script:

- [deploy-lxc.sh](/D:/Codex/Duals/deploy/docker/deploy-lxc.sh)

El script permet:

- clonar o reutilitzar el repositori;
- actualitzar el codi des de `main`;
- reconstruir i aixecar els contenidors;
- mostrar l'estat final dels serveis.

Ruta objectiu de desplegament acordada:

- `/docker/GestioPractiquesDuals`

Configuracio de port acordada:

- la maquina/LXC exposa `443`
- la publicacio externa a Internet es pot mapar a `4444`

## Versionat del producte

El projecte seguira el format `V:x.y.z`.

- `x`: canvi de fase, milestone o canvi major funcional/arquitectonic
- `y`: increment funcional rellevant dins la mateixa fase
- `z`: correccions, ajustos menors o refinaments tecnics

### Regles acordades

- cada canvi de fase ha d'incrementar la versio del producte;
- cada modificacio significativa ha de reflectir una nova versio;
- la versio s'ha de mantenir sincronitzada a:
  - aquest `README.md`;
  - `INSTALL.md`;
  - `Directory.Build.props`;
  - commits o tags de Git quan pertoqui.

## Flux Git recomanat

Flux base proposat:

1. actualitzar la versio del producte;
2. documentar el canvi a `README.md` i, si cal, a `INSTALL.md`;
3. compilar i provar;
4. fer `git add`, `git commit` i `git tag` si correspon;
5. fer `git push` i `git push --tags`.

## Situacio Git actual

En aquest moment el repositori local existeix, pero encara no te cap remot configurat. Abans de poder publicar continuament a GitHub caldra afegir el remot del repositori.
