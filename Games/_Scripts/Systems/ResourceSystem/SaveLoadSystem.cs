using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void Save(int dataId)
    {
        string nameId = "save" + dataId + ".txt";
        GameObject playerObject = GameObject.FindWithTag("Player");
        PlayerStatus playerStatus = playerObject.GetComponent<PlayerStatus>();
        Vector2 playerPosition = playerObject.transform.position;

        PlayerData playerData = new PlayerData
        {
            PlayerPosition = playerPosition,
            Heath = playerStatus.CurrentHealth,
            Mana = playerStatus.CurrentMana,
            Level = 0,
            LevelProgress = 0,
        };

        string saveData = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + nameId, saveData);
    }

    void Load(int dataId)
    {
        string nameId = "save" + dataId;
    }

    public class PlayerData
    {
        public Vector2 PlayerPosition;
        public float Heath;
        public int Mana;
        public int Level;
        public int LevelProgress;
    }
}
