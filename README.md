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
2. `git checkout master`. Neue Releases gehen grundsätzlich vom `master`-Branch aus, daher auf diesen wechseln.
3. `nbgv prepare-release` im Rootverzeichnis dieses Repos ausführen.
    * Die Datei `version.json` im master Branch wird in der Minor-Komponente inkrementiert, `-alpha` als Suffix gesetzt.
    * Es wird ein neuer Branch `release/vX.Y` erstellt. Hier ist `X.Y` als Version ohne Suffix in `version.json` gesetzt. Dabei ist X.Y entsprechend die Version *vor* Ausführung des Befehls - sprich, diese Version ist nun aus der Alpha(/Beta/...)-Phase heraus und wird veröffentlicht. Alle je veröffentlichten X.Y.z-Versionen werden aus diesem Branch veröffentlicht und korrespondieren zu einzelnen Commits dort.
    * siehe auch [NBGV Readme](https://github.com/dotnet/Nerdbank.GitVersioning) für Hintergründe zur Versionierung.
4. Den eben automatisch erstellten `release/vX.Y` Branch pushen: `git checkout release/vX.Y && git push -u origin release/vX.Y`
5. Nun wird per GitHub Action automatisch ein GitHub Release angelegt und zwei Docker Images erzeugt, die von [GHCR](https://ghcr.io) abgerufen werden können.
    * `docker pull ghcr.io/akademischerverein/ava-wasm:latest && docker pull ghcr.io/akademischerverein/ava-storagebackend:latest`

## Branches / Aufbau des Repos

* Entwickelt wird grundsätzlich auf dem `master`-Branch oder bei größeren Unterprojekten auf beliebig benannten Featurebranches.
* Alle öffentlich releasten, stabilen Versionen basieren auf einem Commit in einem `release/vX.Y`-Branch.
  * Jedes Binary kann anhand seiner Dateiversion zum den spezifischen Commit, aus dem es erzeugt wurde, [zugeordnet werden](https://github.com/dotnet/Nerdbank.GitVersioning#what-is-git-height).
  * GitHub Actions erzeugt aus den Commits, die zu Release-Branches gehören, automatisch Docker Container, stellt diese auf ghcr zur Verfügung, erstellt ein GitHub Release und taggt die Commits in git mit der spezifischen Version, die ihnen zugeordnet wurde.
* Nie `master` in `release/vX.Y` mergen. Andersrum geht, siehe unten.

### Details zur Branching-Logik

* Manuelle Commits auf `release/vX.Y`-Branches sind nie bis sehr selten nötig. Der Branching-Philosophie bzw. SemVer-Denkweise nach handelt es sich hierbei um *Hotfixes der spezifischen Version*, die für diese Anwendung selten relevant sein dürften.
  * Im Falle von manuellen Commits kann der Release-Branch in `master` gemergt werden, wenn die Änderungen an der spezifischen Version auch für künstige Versionen relevant sind. Nie andersherum!
  * In `release/v0.2` gibt es einige manuelle Commits, um die CI/CD Pipelines zu entwickeln und zu testen, ohne sehr viele unnötige Release-Branches anzulegen. Zu anderen Zwecken ist das meist unnötig.
* Wenn man plant, `master` in einen Release-Branch zu mergen, macht man grundsätzlich etwas falsch. Andersrum: Einen Release-Branch in den `master`-Branch zu mergen, kann sinnvoll sein, falls es auf dem Release-Branch manuelle Commits gab.
  * Hat man doch `master` in einen Release-Branch gemergt, sollte der Commit reverted(/entfernt) werden. Andernfalls ist der Inhalt der `version.json`-Datei auf dem Release-Branch fehlerhaft.
