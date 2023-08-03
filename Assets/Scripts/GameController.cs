using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] levelPlay;
    public Player player;
    public Level[] levelButton;
    public int[] countLevel;
    public GameObject play, setting, onMusic, offMusic, listLevel, _win, _lose, start;
    public TextMeshProUGUI text;
    public bool isPlay;
    public int idxLv, count;
    public float speed;
    public class PlayerData
    {
        public bool _isMusic;
        public int[] levels;
    }

    private string filePath;
    public PlayerData loadedData;
    // Start is called before the first frame update
    void Start()
    {
        // Đặt đường dẫn của tệp JSON, ví dụ: "data.json" trong thư mục gốc của ứng dụng di động
        filePath = Path.Combine(Application.persistentDataPath, "data.json");

        //Ghi dữ liệu JSON cho lần chạy đầu tiên
        if (!File.Exists(filePath))
        {
            PlayerData dataToSave = new PlayerData
            {
                _isMusic = true,
                levels = new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            SaveDataToJson(dataToSave);
        }

        // Đọc dữ liệu JSON
        loadedData = LoadDataFromJson();
        if (loadedData != null)
        {
            Debug.Log("Is Music On: " + loadedData._isMusic);
            Debug.Log("Levels: " + string.Join(", ", loadedData.levels));
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        if (loadedData._isMusic)
            onMusic.SetActive(true);
        else
            offMusic.SetActive(true);
        isPlay = false;
        speed = 5f;
        setLevel();
    }
    public void setLevel()
    {
        for (int i=0; i<loadedData.levels.Length; i++)
        {
            if (loadedData.levels[i] == 0)
                levelButton[i].setLock(true);
            else
                levelButton[i].setLock(false);
        }    
    }    
    public bool getLevelPlayer(int idx)
    {
        if (loadedData.levels[idx]==1)
        {
            play.SetActive(true);
            listLevel.SetActive(false);
            count = 0;
            text.text = "" + count + " / " + countLevel[idx];
            idxLv = idx;
            levelPlay[idx].SetActive(true);
            return true;
        }
        return false;
    }    
    public void checkPoint()
    {
        count++;
        text.text = "" + count + " / " + countLevel[idxLv];
        if (count > countLevel[idxLv])
        {
            player.rb.velocity = new Vector2(0, 0);
            player.velocity = new Vector2(0, 0);
            _lose.SetActive(true);
        }    
    }   
    public void getUpLevel()
    {
        levelPlay[idxLv].SetActive(false);
        idxLv++;
        levelPlay[idxLv].SetActive(true);
        loadedData.levels[idxLv] = 1;
        levelButton[idxLv].setLock(false);
        saveSetting();
    }
    public void changeSpeed(bool sta)
    {
        if (!sta && speed > 0)
            speed -= 1f;
        else
            speed += 1f;
    }    
    public void replacePlay()
    {
        player.resetPlay();
        count = 0;
        text.text = "" + count + " / " + countLevel[idxLv];
    }    
    public void saveSetting()
    {
        SaveDataToJson(loadedData);
    }
    void SaveDataToJson(PlayerData data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, jsonData);
    }
    PlayerData LoadDataFromJson()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
            return null;
        }
    }
}
