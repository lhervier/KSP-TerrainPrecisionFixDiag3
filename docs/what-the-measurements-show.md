# What the measurements show

Part of [Terrain Precision Fix Diag 3](../README.md): what [the measurements](the-measurements.md) say
about the frame the ground is built in.

This page says when the frame changes, and by how much. What a change of frame does to the ground is
on [the page of the
fix](https://github.com/lhervier/KSP-TerrainPrecisionFix/blob/master/docs/the-culprit.md#why-it-is-different-at-every-load).

## Loading a save does not give back the frame it was saved in

In case 1, the same quicksave, loaded
three times, gives three different angles. From one quickload to the next, `directRotAngle` moved back
by 0.227198° then 0.305716°: Kerbin's rotation over 13.60 s and 18.30 s, the order of the time spent in flight between
two loadings. The frame a save comes back in depends on how long the game ran before it was loaded,
which the save knows nothing about. The terrain sphere moved by 2.4 km, then 3.2 km.

## The world origin moves by 500 m at a time, and not at all near a parked craft

In case 3, on its
own, the rover dragged the origin 500.025 m away before it jumped: the next shift moved the terrain
sphere by exactly that much. Near the capsule, the rover stood 1,901.2 m from the origin, and **Sphere
origin** had not moved by a millimetre: no shift at all. The origin moved when the capsule was
unloaded, in a single shift of 530.606 m: the distance the rover then stood from it, which it had been
waiting for. So a frame is kept still for as long as a craft
is parked within 2.5 km of the one you fly, and leaving that craft behind is what moves it — all at
once, by however far the trip has taken you.

## The frame also changes during a flight, with nothing loaded

In case 2, `directRotAngle` stays put
while the rocket is below 100 km and follows the clock above it: on the way back down, it was
4.903481° further than on the pad, Kerbin's rotation over the 293.5 s spent above. The craft that lands
there finds a frame that depends on its trajectory. Its position moved all along the way too: at that
speed, the world origin follows the rocket at almost every frame, some 19 m at a time. In case 4, the angle never moves, but **Sphere
origin** does: back at the very spot it started from, the rover finds the terrain sphere 6.2 m from
where it was, after four shifts each way, because the world origin follows the craft in jumps, and where the last jump happened
depends on the way it drove (see [the floating origin](what-it-measures.md)). Both are ordinary play:
coming down from orbit, driving to a base. The ground built on the way, or when a parked craft comes
within range, is built in that frame.
