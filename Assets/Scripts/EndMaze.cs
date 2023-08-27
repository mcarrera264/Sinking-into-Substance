using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMaze : MonoBehaviour
{

    public int i = 0;
    public GameObject[] go;
    
    public void SetNextPhase()
    {
        go[i].SetActive(false);
        i += 1;
        go[i].SetActive(true);

    }
}
