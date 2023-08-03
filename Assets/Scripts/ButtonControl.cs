using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    GameController gameController;
    public int key;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    // 0->n: level  -1 play    -2 setting    -3 back   -4:onMussic -5 offMussic  -6: replace  -7 contrinue   -8 try -9 inc  -10 dec
    public void onClick()
    {
        Debug.Log(key);
        if (key >= 0)
        {
            if (gameController.getLevelPlayer(key))
                gameController.isPlay = true;
        }   
        else if (key == -1)
        {
            gameController.start.SetActive(false);
            gameController.listLevel.SetActive(true);
        }
        else if (key == -2)
        {
            gameController.start.SetActive(false);
            gameController.setting.SetActive(true);
        }
        else if (key == -3)
        {
            if(gameController.isPlay)
            {
                gameController.replacePlay();
                gameController.isPlay = false;
                gameController.levelPlay[gameController.idxLv].SetActive(false);
                gameController.play.SetActive(false);
                gameController.listLevel.SetActive(true);
            }
            else
            {
                gameController.setting.SetActive(false);
                gameController.listLevel.SetActive(false);
                gameController.start.SetActive(true);
            }
        }
        else if (key == -4)
        {
            gameController.onMusic.SetActive(false);
            gameController.offMusic.SetActive(true);
            gameController.loadedData._isMusic = false;
            gameController.saveSetting();
        }
        else if (key == -5)
        {
            gameController.offMusic.SetActive(false);
            gameController.onMusic.SetActive(true);
            gameController.loadedData._isMusic = true;
            gameController.saveSetting();
        }
        else if (key == -6)
        {
            gameController.replacePlay();
        }
        else if (key == -7)
        {
            if (gameController.idxLv < gameController.levelPlay.Length - 1)
            {
                gameController.getUpLevel();
                gameController._win.SetActive(false);
                gameController.replacePlay();
            }    
        }
        else if (key == -8)
        {
            gameController._lose.SetActive(false);
            gameController.replacePlay();
        }
        else if (key == -9)
        {
            gameController.changeSpeed(true);
        }
        else if (key == -10)
        {
            gameController.changeSpeed(false);
        }
    }
}
