# Wargear Tracker

Track your miniature collection painting progress — built with ASP.NET Core, Entity Framework and Blazor.

![Status](https://img.shields.io/badge/status-work%20in%20progress-amber)
![License](https://img.shields.io/badge/license-MIT-teal)

## What it does

Wargear Tracker lets you manage your wargaming miniature collection: add armies, track each unit's painting status, and share your progress with a public link.

- Add armies by game and faction
- Track painting status per unit (unboxed → built → primed → base coat → detailed → finished)
- Visual progress bar per army
- Public shareable link for your collection

## Screenshots

> _Coming in phase 2_

## Getting started

### Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) _(phase 2)_

### Run locally

```bash
git clone https://github.com/TejeDesmoi/wargear-tracker.git
cd wargear-tracker
dotnet run --project src/WargearTracker.Api
```

API available at `https://localhost:5001`. Swagger UI at `/swagger`.

## Project structure

```
src/
  WargearTracker.Core/    # Entities, enums, interfaces
  WargearTracker.Data/    # EF Core DbContext, migrations
  WargearTracker.Api/     # ASP.NET Core Minimal API
  WargearTracker.Web/     # Blazor WASM frontend (phase 2)
tests/
  WargearTracker.Tests/
```

## Roadmap

- [x] Project setup
- [ ] Army and miniature CRUD API
- [ ] EF Core + SQLite
- [ ] Auth (JWT)
- [ ] Blazor frontend
- [ ] Docker + deploy
- [ ] Public collection links
- [ ] Wahapedia integration

## Built with

- [ASP.NET Core](https://learn.microsoft.com/aspnet/core) — Minimal API
- [Entity Framework Core](https://learn.microsoft.com/ef/core) — ORM
- [SQLite](https://sqlite.org) → [PostgreSQL](https://postgresql.org) — Database
- [Blazor WebAssembly](https://learn.microsoft.com/aspnet/core/blazor) — Frontend

## Contributing

This is a personal project but issues and suggestions are welcome. If you find it useful, consider [supporting it on Ko-fi](https://ko-fi.com).

## License

MIT
