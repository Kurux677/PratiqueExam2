using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad : MonoBehaviour {
    PlayerAction playerAction;


    private void Start() {
        playerAction = FindObjectOfType<PlayerAction>();
        
    }
   
	
	
    public void SaveGame() {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "gameInfo.dat", FileMode.Create);
        PlayerData playerDataToSave = new PlayerData();
        playerDataToSave.gold = playerAction.GetGoldNumber();
        playerDataToSave.oil = playerAction.GetOilNumber();
        playerDataToSave.money = playerAction.GetAvailableCash();
        bf.Serialize(file, playerDataToSave);
        file.Close();

    }

    public void LoadGame() {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "gameInfo.dat"))
        {
            throw new Exception("Game file doesn't exist");
        }
        FileStream file = File.Open(Application.persistentDataPath + "gameInfo.dat", FileMode.Open);
        PlayerData playerDataToLoad = (PlayerData)bf.Deserialize(file);
        file.Close();
        playerAction.SetValueAfterLoad(playerDataToLoad.gold, playerDataToLoad.oil, playerDataToLoad.money);


    }
    [Serializable]
    class PlayerData
    {
        public int gold;
        public int oil;
        public float money;
       
    }
}
