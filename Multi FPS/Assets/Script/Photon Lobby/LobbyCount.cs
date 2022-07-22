using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCount : MonoBehaviour
{
    public int count;

    public void Selected()
    {
        Data.count = count;
    }
}
