# GitHub Actions Workflows

Este diretório contém os arquivos de workflow do GitHub Actions para o projeto Presence Tracker.

## build.yml

O arquivo `build.yml` é responsável por:

- Construir e testar o projeto
- Rodar regressão de seleção multi-dia do calendário (`CalendarSelectionTests`)
- Rodar regressão de backup/restore (`SqliteBackupServiceTests`)
- Publicar o binário self-contained usado pelo setup
- Compilar o instalador gráfico com Inno Setup (`PresenceTracker-Setup.exe`)
- Empacotar o mesmo instalador em `PresenceTracker-Setup.zip` (fallback se o navegador bloquear `.exe`)
- Publicar `.exe` e `.zip` na release rolling `latest`
- Sincronizar os mesmos arquivos para a release marcada como Latest no GitHub (ex.: `v1.0`), para `/releases/latest` nunca ficar só com o código-fonte

O download fica em: https://github.com/andersongni/presence-tracker/releases/latest
