# The saves

Part of [Terrain Precision Fix Diag 3](../README.md): the saves its protocol uses, and the ones the
other instruments of the family use on Real Solar System.

- [`approach-kerbin.sfs`](approach-kerbin.sfs) — the save [case 3 of the protocol](../docs/the-protocol.md#case-3-a-rover-near-a-parked-craft-then-on-its-own)
  uses: a capsule landed on the flat grass west of the KSC, and a rover 26 m from it.

Copy a save into the folder of a sandbox game and load it from that game.

## On Real Solar System

The saves [Terrain Precision Fix Diag 1](https://github.com/lhervier/KSP-TerrainPrecisionFixDiag) and
[Terrain Precision Fix Diag 2](https://github.com/lhervier/KSP-TerrainPrecisionFixDiag2) are loaded
from on [Real Solar System](https://github.com/KSP-RO/RealSolarSystem) 20.1.3.0, kept here too so
that this instrument can be run on them. They need Real Solar System and what it requires (Kopernicus,
Modular Flight Integrator, KSPTextureLoader, the RSS textures), on an install of their own: they only
load there.

- [`reload-moon-rss.sfs`](reload-moon-rss.sfs) — a capsule on an empty FL-T100, landed on flat ground
  on the Moon.
- [`reload-moon-rss-resave.sfs`](reload-moon-rss-resave.sfs) — the same craft, saved again at a later
  load.
- [`reload-earth-rss-resave.sfs`](reload-earth-rss-resave.sfs) — the same kind of craft, on the grass
  about 1.4 km west of the KSC on Earth.
- [`reload-earth-rss-landed.sfs`](reload-earth-rss-landed.sfs) — the save above, with one line changed
  in the file: the situation of the craft, from `PRELAUNCH` to `LANDED`.
