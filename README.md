# unity_cutscene_trigger
Cutscene Trigger for Unity3D and 2D

## Type Of Triggers
1. RadialDistance - This will set a radial from the object to trigger the cutscene.
2. Interact - Call the funtion "StartCutscene()" to start the Cutscene.
3. Collider3D - Uses the object's collider for detection. isTrigger must be true.
4. Collider2D - Same as Collider3D but for 2D.

## Use Playable Director
Check the box if you want to use Playable Director.
If left Uncheck, it will use CutSceneList script for cutscenes using the specified Scene Name.

## Radial Distance Detection
- Distance Radius
- isTriggered - if this is true, it won't activate the cutscene again. Make a handler for this to allow multiple trigger.

##Markdown Scripting
This is experimental. It will be implemented in the future.
