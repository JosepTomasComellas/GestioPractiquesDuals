#!/usr/bin/env bash

set -Eeuo pipefail

REPO_URL="${REPO_URL:-https://github.com/JosepTomasComellas/GestioPractiquesDuals.git}"
APP_DIR="${APP_DIR:-/docker/GestioPractiquesDuals}"
BRANCH="${BRANCH:-main}"
COMPOSE_DIR="${COMPOSE_DIR:-$APP_DIR/deploy/docker}"
COMPOSE_FILE="${COMPOSE_FILE:-$COMPOSE_DIR/docker-compose.yml}"
ENV_FILE="${ENV_FILE:-$COMPOSE_DIR/.env}"
ENV_EXAMPLE_FILE="${ENV_EXAMPLE_FILE:-$COMPOSE_DIR/.env.example}"
SKIP_GIT_PULL="${SKIP_GIT_PULL:-0}"

log() {
  printf '[duals] %s\n' "$*"
}

fail() {
  printf '[duals][error] %s\n' "$*" >&2
  exit 1
}

require_command() {
  command -v "$1" >/dev/null 2>&1 || fail "No s'ha trobat la comanda requerida: $1"
}

ensure_repo() {
  if [[ -d "$APP_DIR/.git" ]]; then
    log "Repositori existent detectat a $APP_DIR"
    return
  fi

  if [[ -e "$APP_DIR" && ! -d "$APP_DIR/.git" ]]; then
    fail "La ruta $APP_DIR existeix pero no es un repositori Git valid."
  fi

  log "Clonant repositori a $APP_DIR"
  mkdir -p "$(dirname "$APP_DIR")"
  git clone "$REPO_URL" "$APP_DIR"
}

update_repo() {
  if [[ "$SKIP_GIT_PULL" == "1" ]]; then
    log "S'omet l'actualitzacio Git perque SKIP_GIT_PULL=1"
    return
  fi

  log "Actualitzant codi des de la branca $BRANCH"
  git -C "$APP_DIR" fetch origin
  if git -C "$APP_DIR" status --short -- deploy/docker/deploy-lxc.sh | grep -q .; then
    log "Es restaura la versio publicada de deploy-lxc.sh abans de fer pull"
    git -C "$APP_DIR" restore deploy/docker/deploy-lxc.sh
  fi
  git -C "$APP_DIR" checkout "$BRANCH"
  git -C "$APP_DIR" pull --ff-only origin "$BRANCH"
}

validate_compose() {
  [[ -f "$COMPOSE_FILE" ]] || fail "No s'ha trobat el fitxer docker compose a $COMPOSE_FILE"
}

ensure_env_file() {
  if [[ -f "$ENV_FILE" ]]; then
    log "Fitxer d'entorn detectat a $ENV_FILE"
    return
  fi

  if [[ -f "$ENV_EXAMPLE_FILE" ]]; then
    cp "$ENV_EXAMPLE_FILE" "$ENV_FILE"
    log "S'ha creat $ENV_FILE a partir de .env.example"
    log "Revisa especialment el correu i la contrasenya inicial de l'administrador."
    return
  fi

  fail "No s'ha trobat ni $ENV_FILE ni $ENV_EXAMPLE_FILE"
}

show_certificate_warning() {
  local cert_dir="$COMPOSE_DIR/certs"
  if [[ ! -f "$cert_dir/fullchain.pem" || ! -f "$cert_dir/privkey.pem" ]]; then
    log "Atencio: no s'han trobat els certificats Nginx a $cert_dir"
    log "Cal afegir fullchain.pem i privkey.pem abans de publicar HTTPS real."
  fi
}

deploy_stack() {
  log "Construint i arrencant la solucio amb Docker Compose"
  docker compose -f "$COMPOSE_FILE" pull postgres redis nginx || true
  docker compose -f "$COMPOSE_FILE" up -d --build
}

show_status() {
  log "Estat final dels serveis"
  docker compose -f "$COMPOSE_FILE" ps
}

main() {
  require_command git
  require_command docker

  if ! docker info >/dev/null 2>&1; then
    fail "Docker no respon. Revisa que el servei Docker estigui actiu dins l'LXC."
  fi

  ensure_repo
  update_repo
  validate_compose
  ensure_env_file
  show_certificate_warning
  deploy_stack
  show_status

  log "Desplegament/actualitzacio completat."
}

main "$@"
