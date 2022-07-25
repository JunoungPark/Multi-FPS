using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public Texture2D cursorImage;
    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
        PhotonNetwork.Instantiate("Character", new Vector3(Random.Range(0, 5), 1, Random.Range(0, 5)),Quaternion.identity);
    }

}
