## Requirements
You need a clean and unmodified PCBS_Data\Managed\Assembly-CSharp-firstpass.dll!
Other mods that replace that file are NOT compatible with the modloader.

## PCBS Modloader Installer
1. Run PCBSModloaderInstaller.exe
2. Choose the game directory (where PCBS.exe resides)
3. The injector will show you if you already have the modloader installed and if it is updateable
4. Click "Install Modloader" / "Update Modloader" 
5. If it's green you should be good to go

!When the game updates it will usually overwrite the modloader, so you may have to reinstall the modloader then!
!Even mods that exist outside of the modloader may get overwritten during a game update!

## Mod Installation
Mods for the Modloader should have an explanation/readme on how to install them, but the usual way to install them
is to put the mod-folder into <PCBS folder>/Mods, t.ex. <PCBS folder>/Mods/MyNewMod and then that folder has all
the mod contents. eg.:

PCBS Main Folder
+ Mods
  + MyNewMod
    + MyNewMod.dll
	+ readme.txt
  ..
PCBS.exe
..

## Credits
FusioN. for his PCBS modloader ( https://www.youtube.com/channel/UCb8NrhEi7gWJzVt1eVC7IRQ )
The Harmony C# patching library ( https://github.com/pardeike/Harmony )
