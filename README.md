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

> Todo

Siehe GHCR-Pakete dieses Repos, Docker Container hosten. Siehe [Traefik](https://traefik.io/).

## Neue Version releasen

1. ggf. [nbgv cli installieren](https://github.com/dotnet/Nerdbank.GitVersioning/blob/master/doc/nbgv-cli.md): `dotnet tool install -g nbgv`
2. In das Rootverzeichnis dieses Repos wechseln.
3. `nbgv prepare-release` ausführen.
    * Die Datei `version.json` im master Branch wird in der Minor-Komponente inkrementiert, `-alpha` als Suffix gesetzt.
    * Es wird ein neuer Branch `release/vX.Y` erstellt. Dabei ist X.Y entsprechend die Version *vor* Ausführung des Befehls - sprich, diese Version ist nun aus der Alpha(/Beta/...)-Phase heraus und wird veröffentlicht. Alle je veröffentlichten X.Y.z-Versionen werden aus diesem Branch veröffentlicht und korrespondieren zu einzelnen Commits dort.
    * siehe auch [NBGV Readme](https://github.com/dotnet/Nerdbank.GitVersioning) für Hintergründe zur Versionierung.
4. Den eben automatisch erstellten `release/vX.Y` Branch pushen.
5. TODO: Nun wird per GitHub Action automatisch ein GitHub Release angelegt und zwei Docker Images erzeugt, die von [GHCR](https://ghcr.io) abgerufen werden können.
