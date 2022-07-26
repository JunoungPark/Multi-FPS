using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonControl : MonoBehaviourPun//, IPunObservable
{
    public float speed;
    public float angleSpeed;

    public GameObject effect;

    public Camera cam;

    public int health = 100;

    RaycastHit hit;

    public LayerMask layer;
    void Start()
    {
        // 현재 플레이어가 나 자신이라면
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(health);
        }
        else
        {
            // Network player, receive data
            this.health = (int)stream.ReceiveNext();
        }
    }*/
    void Update()
    {
        if (!photonView.IsMine) return;

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(dir.normalized * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime, 0);

        if (health <= 0)
        {
            PhotonNetwork.LeaveRoom();

            PhotonNetwork.Disconnect();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                PhotonControl control = hit.transform.GetComponent<PhotonControl>();

                if (control == null) return;

                control.photonView.RPC("Damage", RpcTarget.All, hit.point);

            }
        }
    }
    
    [PunRPC]
    
    public void Damage(Vector3 direction)
    {
        GameObject hiteffect = Instantiate(effect);

        hiteffect.transform.position = direction;

        Destroy(hiteffect, 0.1f);

        health -= 10;

    }
}
