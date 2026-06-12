# Wargear Tracker

> Stop using Excel to track your pile of shame.

Wargear Tracker lets you manage your wargaming miniature collection and follow your painting progress army by army — from sprue to finished model.

![Status](https://img.shields.io/badge/status-phase%201%20complete-green)
![License](https://img.shields.io/badge/license-MIT-teal)
![Stack](https://img.shields.io/badge/stack-.NET%209%20%7C%20EF%20Core%20%7C%20SQLite-blue)

---

## What it does

- **Track your armies** — organize by game system and faction (40k, AoS, Bolt Action...)
- **Follow painting progress** per unit: Unboxed → Built → Primed → Base Coat → Detailed → Finished
- **Manage your collection** — add, update and delete units with full CRUD support
- Public shareable link per army *(coming in phase 2)*
- Visual progress bar per army *(coming in phase 2)*

---

## Screenshots

> _Coming in phase 2_

## Getting started

### Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(phase 2 only)*

### Run locally

```bash
git clone https://github.com/TejeDesmoi/wargear-tracker.git
cd wargear-tracker

# Apply database migrations
dotnet ef database update \
  --project src/WargearTracker.Data \
  --startup-project src/WargearTracker.Api

# Start the API
dotnet run --project src/WargearTracker.Api
```

API available at `https://localhost:7xxx` — exact port shown in terminal output.  
Swagger UI at `/swagger` — use it to test all endpoints without a frontend.

---

## API endpoints (phase 1)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/armies` | List all armies |
| POST | `/armies` | Create a new army |
| GET | `/armies/{id}` | Get a single army |
| DELETE | `/armies/{id}` | Delete an army |
| GET | `/armies/{id}/miniatures` | List miniatures in an army |
| POST | `/miniatures` | Add a miniature |
| PATCH | `/miniatures/{id}/status` | Update paint status |
| DELETE | `/miniatures/{id}` | Delete a miniature |

---

## Project structure

```
wargear-tracker/
├── src/
│   ├── WargearTracker.Core/     # Entities, enums — no external dependencies
│   ├── WargearTracker.Data/     # EF Core DbContext, migrations
│   ├── WargearTracker.Api/      # ASP.NET Core Minimal API + Swagger
│   └── WargearTracker.Web/      # Blazor WASM frontend (phase 2)
├── tests/
│   └── WargearTracker.Tests/
├── .github/
│   ├── ISSUE_TEMPLATE/
│   └── pull_request_template.md
└── README.md
```

---

## Roadmap

### Phase 1 — Local MVP ✅
- [x] Solution structure and project setup
- [x] Core domain entities (Army, Miniature, PaintStatus)
- [x] EF Core + SQLite + migrations
- [x] Army CRUD endpoints
- [x] Miniature CRUD endpoints
- [x] Swagger UI

### Phase 2 — Deploy and community
- [ ] JWT authentication (register + login)
- [ ] Migrate to PostgreSQL + Docker
- [ ] Public shareable link per army
- [ ] Blazor WebAssembly frontend
- [ ] CI/CD with GitHub Actions
- [ ] Deploy to Railway / Fly.io

### Phase 3 — Community features
- [ ] Wahapedia integration (army list points)
- [ ] Spending tracker per army
- [ ] Community statistics

---

## Built with

| Layer | Technology |
|-------|-----------|
| API | ASP.NET Core 9 — Minimal API |
| ORM | Entity Framework Core 9 |
| Database | SQLite (phase 1) → PostgreSQL (phase 2) |
| Frontend | Blazor WebAssembly (phase 2) |
| Docs | Swashbuckle / Swagger UI |

---

## Contributing

This is a personal project but issues and suggestions are welcome.  
If you find it useful, consider [supporting it on Ko-fi](https://ko-fi.com).

---

## License

MIT
