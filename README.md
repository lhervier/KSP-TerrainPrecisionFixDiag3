# Terrain Precision Fix - Diagnostic Mod 3

**⚠️ Work in progress.** This is an active investigation, not a finished mod. The code and this page can still change, and several questions are still open.

A measuring instrument for KSP 1.12. It shows, on your own install, the world frame the terrain of a
body is built in: how the body is turned in Unity's world axes, and where it sits in them. It records
that frame whenever you ask, so that you can compare it from one loading to the next, and from one
moment of a flight to another.

**It explains, it does not compare.** The other diagnostic mods of this family are read twice — once
on a stock install, once with a fix installed — and the difference between the two readings is the
whole point of running them. This one is read once. The five values it shows are the game's own
description of its world: a fix to the terrain does not have to touch them, and the one this family
proposes writes none of them. Reading the same figures with a fix installed is the expected outcome
here, not a sign that the fix does nothing. Every figure on this page is stock KSP, and what they show
is how the game places the ground it builds.

**How this was made.** Written with Claude, Anthropic's AI assistant, and reviewed line by line by a
human — me. I am saying so up front, because contributions made with an AI deserve a closer look than
others, and because some people would rather stop reading here. That look is easy to give here: this
mod changes nothing in the game and reads five values the game already holds, the source is a few
hundred lines, and the protocol runs on a stock install with no dependency of any kind.

## What it measures

Unity places every object with `float` coordinates, precise near the origin of its world and only to a
few centimetres 600 km away. So KSP keeps that origin on your craft, and shifts it back onto the craft
once it has drifted far enough — except while another landed craft is loaded, when it waits until that
craft is unloaded. Close to a planet, it stands still in Unity's axes and the sky turns around it; further
out, the planet turns. The ground is placed relative to the planet's terrain sphere: where that sphere sits
and how it is turned are the frame the ground is built in.

The window records, one line at a time: the game clock, whether the planet or the sky is turning, the
two angles the game splits the planet's rotation between, and the world position of the terrain
sphere; and, for the world origin itself, how far your craft is from it, how many times it has been
shifted since the previous line, and how far the last shift moved it.

**→ Full chapter: [What it measures](docs/what-it-measures.md)**

## The protocol

Four cases, each moving one input of the frame in a situation every player meets: the same save loaded
three times; a sounding rocket taken out of the rotating frame and back; a rover driven near a parked craft, then away
from it and on its own; a rover driven down the runway and back. Only the first loads a save between
two records. The two craft, and the save the third case starts from, come with this mod.

**→ Full chapter: [The protocol](docs/the-protocol.md)**

## The measurements

The four cases, played on stock KSP 1.12.5 with KSP Community Fixes.

- **Loading the same save three times**: a different angle at every quickload, −140.000535°, −140.227733°,
  −140.533449°, and a terrain sphere moved by 2.4 km, then 3.2 km.
- **A rocket out of the rotating frame and back**: the frame switches at exactly 100 km, and the angle is 4.903481°
  further on the way down than on the pad.
- **A rover near a parked craft, then on its own**: 1,901.2 m from the world origin near the capsule,
  without a single shift; one shift of 530.606 m the moment the capsule was unloaded; then, on its own,
  one shift of 500.025 m.
- **A rover down the runway and back**: back at the very spot it started from, the terrain sphere is
  6.2 m from where it was.

**→ Full chapter: [The measurements](docs/the-measurements.md)**

## What the measurements show

Loading a save does not give back the frame it was saved in: the angle it comes back at depends on how
long the game ran before, which the save knows nothing about. The world origin moves by 500 m at a
time, and not at all while a parked craft is loaded nearby — leaving that craft behind moves it all at
once. And the frame changes during a flight with nothing loaded, both in angle, above 100 km, and in
position, on the ground: the ground built on the way, or under a parked craft you come back to, is
built in whatever frame the trip has left.

**→ Full chapter: [What the measurements show](docs/what-the-measurements-show.md)**

## Get it

Either way you end up with the same `GameData/TerrainPrecisionFixDiag3Mod/` folder.

**Download it** — from the assets of the
[latest release](https://github.com/lhervier/KSP-TerrainPrecisionFixDiag3/releases/latest).

**Or compile it** — clone this repository, set `KSPDIR` to your KSP install folder and run
`build.bat`. It needs the .NET SDK, takes a few seconds, reads the KSP assemblies straight from your
install, and puts the DLL in `GameData/TerrainPrecisionFixDiag3Mod/` inside the repository. It does
not install anything. Worth doing if you would rather not run a binary you have no source for while
reporting a measurement.

## Install

Drop `GameData/TerrainPrecisionFixDiag3Mod` into the `GameData` of KSP, so that you end up with
`GameData/TerrainPrecisionFixDiag3Mod/TerrainPrecisionFixDiag3Mod.dll`. It runs on a stock install:
no Harmony, no ModuleManager, no dependency of any kind.

It reads the world and writes nothing at all: the table lives in memory and is gone when you close the
game. Your saves are never touched. Removing the folder removes the mod.

## License

MIT
