# The protocol

Part of [Terrain Precision Fix Diag 3](../README.md): four cases, step by step. The columns they fill
are in [What it measures](what-it-measures.md#the-window), and what they read is in [The measurements](the-measurements.md).

The frame has two inputs, the orientation and the position of the terrain sphere. Each case makes
one of them move, in a situation every player meets:

| case | what moves | a save loaded? | in play |
|---|---|---|---|
| 1. The same save, loaded three times | the orientation | yes | loading a game |
| 2. A rocket out of the rotating frame and back | the orientation | no | coming down from orbit to land near a base |
| 3. A rover near a parked craft, then on its own | the position | no | driving to a base, and away from it |
| 4. A rover driven 2 km and back | the position | no | driving to a base, or coming within range of it |

Case 1 is the only one where a save is loaded between two records, so it is the only one a change
made at loading could reach. Cases 2 to 4 show what happens after that.

The two craft it uses are in [`craft/`](../craft/): copy
[`Diag3-Rover.craft`](../craft/Diag3-Rover.craft) into the `Ships/SPH` folder of a sandbox game, and
[`Diag3-Rocket.craft`](../craft/Diag3-Rocket.craft) into its `Ships/VAB` folder. Case 3 also needs a
craft parked 2 km away, so it comes with a save,
[`approach-kerbin.sfs`](../save/approach-kerbin.sfs): copy it into the folder of a sandbox game, and
load it from that game.

## Case 1: loading the same save three times

Put `Diag3-Rover` on the runway, and put its brakes on straight away: it comes out of the hangar with
them off. Quicksave (F5). Then, three times: wait about ten seconds, quickload (hold F9) and press
**Record**. Compare the three lines.

**→ Measured in [case 1](the-measurements.md#case-1-loading-the-same-save-three-times)**

## Case 2: leaving the rotating frame and coming back, in one flight

Put `Diag3-Rocket` on the launch pad. It is a sounding rocket that climbs high enough for the frame
of Kerbin to stop being `Rotating`. It has no decoupler: its crew does not survive the
landing.

Press **Record** on the pad. Turn SAS on, so that the rocket keeps pointing straight up, launch at
full throttle, and press **Record** once **Frame**
reads `Inertial`. Let it fall back, and press **Record** again once **Frame** reads `Rotating`, before
it hits the ground. Compare the lines: no save was loaded between them. All along the flight, keep an eye
on **Frame**: the moment it changes, on the way up and on the way down, note the altitude shown by the
game's altimeter.

**→ Measured in [case 2](the-measurements.md#case-2-leaving-the-rotating-frame-and-coming-back-in-one-flight)**

## Case 3: a rover near a parked craft, then on its own

Load `approach-kerbin`. You drive `Diag3-Rover`, facing south; a capsule is landed about 2 km north
of it, on the same north–south line. Watch the line in progress all along: **Origin distance** and
**Shifts**.

- Press **Record**.
- Drive north to about 100 m from the capsule, stop and press **Record**.
- Turn round and drive south until the capsule is more than 2.6 km away, so that it is unloaded. Stop
  and press **Record**.
- Keep driving south in a straight line. When **Shifts** goes up, stop and press **Record**.
  *Spoiler, so that you do not have to creep forward a metre at a time: it happens when **Origin
  distance** reaches about 500 m.*

Compare **Origin distance** on the line taken near the capsule, and **Shifts** and **Last shift** on
the two lines after it.

**→ Measured in [case 3](the-measurements.md#case-3-a-rover-near-a-parked-craft-then-on-its-own)**

## Case 4: a rover driven 2 km and back

Make sure no other craft is landed anywhere around the Space Center: one parked within 2.5 km of the
rover would keep the world origin from moving at all, as case 3 shows. Put `Diag3-Rover` on the runway, put its brakes on straight away — it comes out of the hangar with
them off — and press **Record**. Drive to the far end of the runway, stop and
press **Record**, then drive back to where you started, stop and press **Record** again.

**→ Measured in [case 4](the-measurements.md#case-4-a-rover-driven-2-km-and-back)**
