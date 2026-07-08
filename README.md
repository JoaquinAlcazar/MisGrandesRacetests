# MisGrandesRacetests
   
Mis Grandes Racestest (or MGR) is a knock-off copy of Horse Race Tests (HRT), a simple race simulator which consists on races of various "horses" which move in one direction and have to stumble their way around to the goal, whoever gets there first, wins.
  
This personal version of the game is made from scratch and replaces the original horses with human characters. All characters also follow the simple behaviour of going forwards and changing their direction when hitting a wall.

The game isn't complete and lacks of a playable build, but it can still be tested. To test the game, open any map scene (except WinMap) and hit the Unity play button.

### Gameplay  
The loop goes as it follows:  
1. The game selects 6 random characters and spawns them, (It reads the information from PlayerPrefs where all racers are stored) adding +1 to the counter of times a given character has been selected.  
2. After being spawned, all characters start moving forward in a random direction. Spawnpoints are always closed from the rest of the map with a barrier that disappears 3 seconds after the characters are spawned.  
3. Characters move in a straight line until they collide with anything, chainging their direction. To maintain the "wacky" and randomness of the original HRT, the hitboxes are corresponding to the character instead of a large box or circle. (This may cause sometimes a character getting stuck with another character).  
4. Once a character reaches the goal, a "Shibieuro" in this case, the game pauses, and the camera approaches the character who first touched the Shibieuro.  
5. The victory scene is loaded (WinMap), where it is shown the victor in a large image, the character's name, its total wins-losses, and the rest of the cast below.
  
### Meet the racers!!  
- Barkv (ZKV)  
- Chamusquino (BRN)  
- Eleven Ways to Win (UND)  
- Fel Felxtapo (FXT)  
- Glitter Chiikawa (GLI)  
- Ikaro (IKR)  
- Kime, Don Kime (KME)  
- Midnight Lullabby (ABB)  
- Opportune Minted (MNT)  
- Shining Binnacle (SHB)  
- White Nights (NGT)  
- Zealous Ephemeral Thought (ZTA)  

If you want to see some footage, you can refer [here](https://x.com/MaybeItsKimine/status/1927695110289920003?s=20)

Game backgrounds, music, sound effects, and "Lei Heng" belong to Project Moon (specifically Limbus Company)  
Character sprites belong to ShibiDayo
  
# Fun Facts  
The map Purgatorio1 will only spawn the character Shining Binnacle
