# SoundInvaders
To compile the game, the project will need to be imported into Unity. The project is tested as working with Unity 2019.2.17f1.

The default scene should be removed, and the scenes from Assets>Scenes added, with Menu left as the only loaded scene.

You will also need to import the Google Resonance Audio and Oculus Audio Spatialization packages into the project. These can be found at:

Google Resonance -  https://github.com/resonance-audio/resonance-audio-unity-sdk/releases

Oculus Audio Spatialization - https://developer.oculus.com/downloads/package/oculus-spatializer-unity/

To utilise these spatializers, you will need to enable the respective scripts on the player and enemy objects, as well as the cube in the main scene that acts as a reverb zone. You will also need to select the correct spatializer plugin and ambisonic decoder plugin within Edit > Project Settings > Audio

For Resonance audio, the output of the enemy audio source should be set to Resonance Audio Mixer > Master