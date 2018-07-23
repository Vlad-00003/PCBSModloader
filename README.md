# PCBSModloader

## Basics
The modloader is installed using the PCBS Modloader Installer, see [releases](https://github.com/fusiion/PCBSModloader/releases)

Each mod has its own folder. A mod folder is placed under *\<PCBS folder\>\Mods*. The DLLs in the mod's own folder will be automatically read by the modloader when the game starts up. Even [Harmony](https://github.com/pardeike/Harmony) patches contained in these DLLs will be read and applied at that point.
  
## How to make a mod
Visual Studio:
1. Download the modloader installer and extract the *PCBSModloader.dll* and if needed the *0Harmony.dll* to somewhere you can access
2. Create a new project with the project type being *Class Library (.NET Framework)*
3. In the project properties set the target framework to *Unity 3.5 .net full Base Class Libraries* **if** you have it available, this aims to reduce incompatibilities since the DLL is going to be loaded into the game context and the game is built with Unity. It might still work with another target framework, but it is not recommended. You might get warnings about references in the project not matching with the target framework, and you should remove those references.
4. Reference the *PCBSModloader.dll* and if needed the *0Harmony.dll* from your project (Right click on *References* -> *Add Reference...* -> *Browse...*)
5. Create a class extending *Mod* from the modloader, the minimum for such a class should look like this:
using PCBSModloader;

```C#
using PCBSModloader;

namespace TestMod
{
    public class Class1 : Mod
    {
        public override string ID { get { return "MyTestMod"; } }

        public override string Version { get { return "0.1"; } }

        public override string Author { get { return "Me"; } }
    }
}

```

6. If you have those three properties implemented build the project
7. Install the modloader for PC Building Simulator if you haven't done it yet
7. Create a folder *Mods\TestMod* (or whatever name you want) in the \<PCBS folder\> and put your compiled DLL there.
8. Run PC Building Simulator, it should say "Modloader initialized" in the upper left corner
  
To check if your mod has been successfully loaded go to the *Mods* folder and look for a file called *log.txt*, it should look something like this:
```
----- Initializing PCBSModloader... -----

Initializing harmony...
Loading internal mods...
Loaded mod ModUI
Loading mods...
**Loaded mod MyTestMod**
Finished loading.

```

And that's it, if your mod name shows up in the log it was successfully loaded. Now you can either implement more of the methods that the *Mod* base class has available or you can create Harmony patches in your project that will get automatically loaded as the mod gets loaded. You may of course need to reference more assemblies from the game to access those classes and functions, most certainly you will want to reference the *Assembly-CSharp-firstpass.dll* which contains most of the game's code, and then you may need to reference Unity DLLs as needed as you use the various Unity functions.

To check the structure of the *Assembly-CSharp-firstpass.dll* and the various Unity DLLs referenced by it I'd recommend you to use [dnSpy](https://github.com/0xd4d/dnSpy) if you haven't already.
