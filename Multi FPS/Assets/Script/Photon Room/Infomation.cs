using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infomation : MonoBehaviour
{
    public Text roomData;

    public void SetInfo(string name, int current, int max)
    {
        roomData.text = name + " ( " + current + " / " + max + ")";
    }
}
