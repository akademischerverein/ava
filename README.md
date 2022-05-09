# AVA

> AV Adressverwaltung

## Prerequirements / Tools used

* Postgres
  * portable/ZIP deployment, siehe `tools`
  * lokale Installation
* [.editorconfig](https://editorconfig.org/)
  * In VSCode: Plugin, in Visual Studio nativ -> geht einfach
* fyi, keine weitere Aktion erforderlich
  * [NBGV](https://github.com/dotnet/Nerdbank.GitVersioning), Versionierung anhand git-depth
  * [StyleCop.Analyzers](https://www.nuget.org/packages/StyleCop.Analyzers/), einheitlicher Code-style
  * [icongen](https://cthedot.de/icongen/), PWA Icons & Favicon aus SVG, hat besser geklappt als [PWA Builder](https://www.pwabuilder.com/imageGenerator)

## Aktuelle Version hosten

Siehe `docker-compose.hosting.yml`-Datei:

1. `ava.localhost` durch den richtigen Hostname ersetzen.
2. Kryptografisch sicheres JwtSecret erzeugen und einfügen, mindestens 16 Zeichen lang.
3. Postgres Connection String ggf. anpassen, ggf. Postgres Container in Compose hinzufügen.

## Neue Version releasen

1. ggf. [nbgv cli installieren](https://github.com/dotnet/Nerdbank.GitVersioning/blob/master/doc/nbgv-cli.md): `dotnet tool install -g nbgv`
2. `nbgv prepare-release` im Rootverzeichnis dieses Repos ausführen.
    * Die Datei `version.json` im master Branch wird in der Minor-Komponente inkrementiert, `-alpha` als Suffix gesetzt.
    * Es wird ein neuer Branch `release/vX.Y` erstellt. Hier ist `X.Y` als Version ohne Suffix in `version.json` gesetzt. Dabei ist X.Y entsprechend die Version *vor* Ausführung des Befehls - sprich, diese Version ist nun aus der Alpha(/Beta/...)-Phase heraus und wird veröffentlicht. Alle je veröffentlichten X.Y.z-Versionen werden aus diesem Branch veröffentlicht und korrespondieren zu einzelnen Commits dort.
    * siehe auch [NBGV Readme](https://github.com/dotnet/Nerdbank.GitVersioning) für Hintergründe zur Versionierung.
3. Den eben automatisch erstellten `release/vX.Y` Branch pushen: `git checkout release/vX.Y && git push -u origin release/vX.Y`
4. Nun wird per GitHub Action automatisch ein GitHub Release angelegt und zwei Docker Images erzeugt, die von [GHCR](https://ghcr.io) abgerufen werden können.
    * `docker pull ghcr.io/akademischerverein/ava-wasm:latest && docker pull ghcr.io/akademischerverein/ava-storagebackend:latest`
