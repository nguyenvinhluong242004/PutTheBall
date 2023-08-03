using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject isLock;
    public void setLock(bool sta)
    {
        if (sta)
            isLock.SetActive(true);
        else
            isLock.SetActive(false);
    }    
}
