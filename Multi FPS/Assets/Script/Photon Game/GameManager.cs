using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    void Start()
    {
        PhotonNetwork.Instantiate("Character", new Vector3(Random.Range(0, 5), 1, Random.Range(0, 5)),Quaternion.identity);
    }

}
