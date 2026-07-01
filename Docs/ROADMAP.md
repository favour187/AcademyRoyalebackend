# Blood Ring — Production Roadmap

Authored-art production is incremental and batched. Each art batch is finished PNG
content (no code-generated placeholders), indexed into `Generated/asset_manifest.json`
and gated by `Tools/validate_assets.py`.

## Milestones

### M0 — Cleanup & foundation ✅
- Removed all code-based art generators (Python + runtime `ProceduralArt`).
- Removed procedural/"recipe" placeholder assets and noise textures.
- Reorganized authored art into `Assets/Resources/Art/{Characters,Weapons,Vehicles,Terrain,UI,...}`.
- Added `BloodRingArtLibrary`, `SceneArtBackdrop`, asset manifest + validator, GitHub CI.

### M1 — Core authored art (in progress)
- [x] Terrain tiles: grass, sand, rock, snow, asphalt, mud, concrete, metal grate
- [x] Weapons: assault rifle, SMG, shotgun, sniper (HD) + new pistol, SMG variants
- [ ] Weapons: AR, LMG, DMR, pistol family, throwables (continue next batch)
- [ ] Vehicles: complete ground/air/water set + skins
- [ ] Characters: hero roster portraits + outfits
- [ ] UI: icon set, buttons, HUD, frames, backgrounds
- [ ] Effects: smoke, explosion, weather, lighting sheets
- [ ] Scene backdrops: splash, lobby, character hub, battle map, results

### M2 — Systems vertical slice
- Full match loop: aircraft → landing → gameplay → zone → results.
- Inventory, loot, weapons handling, vehicles drivable.
- Backend: auth, profile, store, leaderboard, social wired to client.

### M3 — LiveOps & polish
- Battle pass, events, store rotation via `/LiveOps` remote config.
- Ranking, clans/factions, missions, cosmetics.
- Performance pass: LOD/streaming/pooling/occlusion verified on mid-range Android.

### M4 — Release candidate
- Anti-cheat, regional matchmaking, cloud saves hardened.
- Full QA pass (see `QA_CHECKLIST.md`), store submission assets finalized.

## Art generation note
Art is produced in capped batches. When a batch hits the per-session generation
limit, production pauses and resumes in the next batch — terrain and weapons are
prioritized first, then vehicles, characters, UI, and effects.


