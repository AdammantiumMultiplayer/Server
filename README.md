# AMP-S - Adammantium Multiplayer Mod Server
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/fuck-it-ship-it.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/gluten-free.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/60-percent-of-the-time-works-every-time.svg)](https://forthebadge.com)
---------
Server Wrapper to run Adammantium Multiplayer as a standalone server with basic plugin support.
---------
## Installation Instructions
### What do you need?
- Get the latest Server files in the [Release](https://github.com/Adammantium/-AMP-Server/releases) Tab
- The lastest version of AMP from [Nexus](https://www.nexusmods.com/bladeandsorcery/mods/6888?tab=files) or the specific version you want to host
- Installation of Blade and Sorcery to grab some required files
__You can also use the [ServerManager](https://github.com/flexhd41/AMP-server-manager) made by flex hd.__
### What files are needed?
#### AMP-Server
- `AMP_Server.exe`
- `AMP_Server.pdb`
- Optional: `plugins` folder
#### AMP-Mod
- `Adammantium Multiplayer Mod.dll`
- `Adammantium Multiplayer Mod.pdb`
#### Blade & Sorcery
Navigate to your installation folder. When inside the folder go to `BladeAndSorcery_Data/Managed`.
- `Dungen.dll`
- `Newtonsoft.Json.dll`
- `ThunderRoad.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.SharedInternalsModule.dll`  
**Note: I won't ship required .dll files of B&S with my server, because i don't own the rights to them, thats why you need to grab them yourself.**
#### Your folder should like this:
> ![image](https://user-images.githubusercontent.com/38858318/215270602-e3dbf7cc-9bc5-49c6-9453-311931d44779.png)
### How to install the Server?
1) Get all the files from the step above and put them in the same folder
2) Execute the `AMP_Server.exe`  
3a) **On Windows:** Just double click the file or run it via the console  
3b) **On Linux:** You must have Mono installed. Just run the file with `mono AMP_Server.exe`
![image](https://user-images.githubusercontent.com/38858318/215270515-88c7e1a4-d996-4109-aba5-304d192a81ea.png)
Check that the Mod Version inside of the console is the one you want.
### How to configure the server?
When you start the server for the first time a `server.json` file will be generated. Inside of there you can configure the server how you want.
> ![image](https://user-images.githubusercontent.com/38858318/215270420-42660dfe-0115-4307-a220-086d043bce6a.png)

