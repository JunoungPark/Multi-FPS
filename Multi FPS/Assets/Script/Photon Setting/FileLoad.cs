using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileLoad : MonoBehaviour
{
    public Text textGUI;

    public string path;

    void Start()
    {
        textGUI.text = System.IO.File.ReadAllText(path);
    }
}
