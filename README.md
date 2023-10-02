# AMP-S - Adammantium Multiplayer Mod Server
[![forthebadge](https://raw.githubusercontent.com/BraveUX/for-the-badge/master/src/images/badges/built-with-love.svg)](https://forthebadge.com)
[![forthebadge](https://raw.githubusercontent.com/BraveUX/for-the-badge/master/src/images/badges/fuck-it-ship-it.svg)](https://forthebadge.com)
[![forthebadge](https://raw.githubusercontent.com/BraveUX/for-the-badge/master/src/images/badges/gluten-free.svg)](https://forthebadge.com)
[![forthebadge](https://raw.githubusercontent.com/BraveUX/for-the-badge/master/src/images/badges/60-percent-of-the-time-works-every-time.svg)](https://forthebadge.com)
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
- `Newtonsoft.Json.dll`
- `Unity.ResourceManager.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.PhysicsModule.dll`
- `UnityEngine.SharedInternalsModule.dll`
- Optional: `plugins` folder
#### Netamite
- `Netamite.dll`
- `Netamite.pdb`
- `Netamite.Steam.dll`
- `Netamite.Steam.pdb`
- `Netamite.Unity.dll`
- `Netamite.Unity.pdb`
#### AMP-Mod
- `AMP.dll`
- `AMP.pdb`  
If you download the mod from nexus or mod.io, you probably need to rename both `ZZZ_AMP` to `AMP`.
#### Blade & Sorcery
Navigate to your installation folder. When inside the folder go to `BladeAndSorcery_Data/Managed`.
- `ThunderRoad.dll`  
- `ThunderRoad.Manikin.dll`  

**Note: I won't ship required .dll files of B&S with my server, because i don't own the rights to them, thats why you need to grab them yourself.**
#### Your folder should like this:
> ![image](https://user-images.githubusercontent.com/38858318/234640492-39fa9123-10e7-470c-a4c6-155c28681781.png)

### How to install the Server?
1) Get all the files from the step above and put them in the same folder
2) Execute the `AMP_Server.exe`  
3a) **On Windows:** Just double click the file or run it via the console  
3b) **On Linux:** You must have Mono installed. Just run the file with `mono AMP_Server.exe`
![image](https://user-images.githubusercontent.com/38858318/215270515-88c7e1a4-d996-4109-aba5-304d192a81ea.png)
Check that the Mod Version inside of the console is the one you want.
### How to configure the server?
When you start the server for the first time a `server.json` file will be generated. Inside of there you can configure the server how you want.
> ![image](https://user-images.githubusercontent.com/38858318/234640612-5c5df4be-260e-4c5a-8e8d-508a0d69e1ff.png)  

#### **serverSettings**
| Tag | Description | Value Type | Default Value |
| --- | --- | --- | --- |
| `port` | The port you wanna use, it will use UDP & TCP | `number` | 26950 |
| `map` | The map you want to start the server with | `text` | Arena |
| `mode` | The mode you want to start the server with | `text` | Sandbox |
| `max_players` | Max players that are allowed to be connected at the same time | `number` | 10 |
| `password` | Protects the server with that password | `text` | none |
| `masterServerUrl` | The Master Server URL to report to | `text` | bns.devforce.de |
| `showInServerList` | Annouces the server to the serverlist at the address specified in `masterServerUrl` | `boolean` | false |
| `servername` | Name to show in the serverlist | `text` | Unnamed Server |
| `serverdescription` | Description to show in the serverlist | `text` | No description |

#### **hostingSettings**
| Tag | Description | Value Type | Default Value |
| --- | --- | --- | --- |
| `pvpEnable` | Toggles PdP (Player damage isnt reliable atm, so its only Player damage Player instead of versus) | `boolean` | true |
| `pvpDamageMultiplier` | Damage Multiplier factor to other players | `float` | 0.2 |
| `allowMapChange` | Allows connected players to change the map, otherwise they get kicked | `boolean` | true |
| `maxItemsPerPlayer` | Max concurrent items a player is allowed to spawn (will despawn the oldest one once the limit is reached) | `number` | 250 |
| `maxCreaturesPerPlayer` | Same as `maxItemsPerPlayer` but for creatures | `number` | 15 |
