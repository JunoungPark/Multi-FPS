using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Billboard : MonoBehaviourPun
{
    public Text nickName;

    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
