# Gestió de les pràctiques Duals

Versió actual del producte: `V:0.4.2`

Projecte per a la gestió de les pràctiques Duals dels cicles formatius, implementat amb `.NET 10`, `Blazor`, `API ASP.NET Core`, `PostgreSQL`, `Redis`, `Nginx`, `Docker Compose` i una capa preparada per `MCP`.

## Estat actual

- Fase en curs: `Fase 2`
- Estat: shell visual reforçat, dashboard refinat i primer mòdul acadèmic funcional disponible
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

Per la posada en marxa local consulta [INSTALL.md](INSTALL.md).

## Desplegament en LXC

Per al desplegament o actualitzacio en un contenidor LXC de Proxmox es disposa del script:

- [deploy-lxc.sh](deploy/docker/deploy-lxc.sh)

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

Nota de desplegament actual:

- la web consumeix l'API interna del `docker compose` via `http://api:8080/`
- si l'API no esta disponible temporalment, la portada no ha de caure amb error 500

Novetats de `V:0.4.2`:

- repàs de maquetació del shell principal per corregir proporcions, espaiats i escalat del branding
- capçalera reajustada amb el `logo.png` correcte en lloc del lockup desbordat
- dashboard i pantalles internes reequilibrats per eliminar sobredimensionats i apropar-se millor a la referència AutoCo

Novetats de `V:0.4.1`:

- refinament visual del shell principal i del dashboard per apropar-lo a la referència AutoCo
- nou mòdul `Gestió de classes`
- nou mòdul `Cicles i tutors`
- acció funcional per obrir o tancar l'edició del formulari d'alumnes per classe
- nova pantalla `Activitats i seguiment` per validar navegació i botons
- nou `mode de proves` configurable des del `.env` per entrar sense autenticació

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

## Credencials inicials de prova

Quan la base de dades arrenca correctament i s'apliquen les migracions, la web crea un usuari administrador inicial:

- correu: `admin.duals@salesianssarria.test`
- contrasenya: `Duals.Admin.2026!`

També es creen usuaris inicials associats al professor i a l'alumne demo sembrats a la base de dades.

La identitat inicial de l'administrador ja no queda fixa al codi. En desplegament es configura des de:

- [deploy/docker/.env.example](deploy/docker/.env.example)
- i al servidor des de `deploy/docker/.env`

Variables principals:

- `SCHOOL_EMAIL_DOMAIN`
- `BOOTSTRAP_ADMIN_EMAIL`
- `BOOTSTRAP_ADMIN_DISPLAY_NAME`
- `BOOTSTRAP_ADMIN_PASSWORD`

La contrasenya bootstrap ha de tenir com a mínim 10 caràcters, una majúscula, una minúscula i un número.

En desplegament, el fitxer `deploy/docker/deploy-lxc.sh` es restaura automàticament abans del `pull` perquè la versió del repositori prevalgui sobre canvis locals accidentals. Els certificats `fullchain.pem` i `privkey.pem` queden fora del control de versions.

## Situació Git actual

El repositori ja està vinculat a GitHub a:

- [JosepTomasComellas/GestioPractiquesDuals](https://github.com/JosepTomasComellas/GestioPractiquesDuals)
