using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName, RoomPerson;
    public Button RoomJoin, RoomCreate;

    public GameObject RoomPrefab;
    public Transform RoomContent;

    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if(RoomName.text.Length > 0)
        {
            RoomJoin.interactable = true;
        }
        else
        {
            RoomJoin.interactable = false;
        }

        if(RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
        {
            RoomCreate.interactable = true;
        }
        else
        {
            RoomCreate.interactable = false;
        }   
    }

    public void OnClickCreateRoom()
    {
        // 룸 옵션을 설정합니다.
        RoomOptions room = new RoomOptions();

        // 최대 접속자의 수를 설정합니다.
        room.MaxPlayers = byte.Parse(RoomPerson.text);

        // 룸의 오픈 여부를 설정합니다.
        room.IsOpen = true;

        // 로비에서 룸 목록을 노출시킬 지 설정합니다.
        room.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }

    // 룸이 생성되었을 때 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    public void AllDeleteRoom()
    {
        // Transform 오브젝트에 있는 하위 오브젝트에 접근해서 전체 룸을 삭제합니다.
        foreach(Transform trans in RoomContent)
        {
            // Transform을 가지고 있는 게임 오브젝트를 삭제합니다.
            Destroy(trans.gameObject);
        }  
    }

    // 룸에 입장한 후에 호출되는 함수입니다.
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void RoomCreateObject()
    {
        // RoomCatalog에 여러 개의 value값이 들어가 있다면 RoomInfo에 넣어줍니다.
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            // 룸을 생성합니다.
            GameObject room = Instantiate(RoomPrefab);

            // RoomContent의 하위 오브젝트로 설정합니다.
            room.transform.SetParent(RoomContent);

            // 룸 정보를 입력합니다.
            room.GetComponent<Infomation>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }  
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            // 해당 이름이 RoomCatalog의 key 값으로 설정되어 있다면
            if(RoomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : 룸에서 삭제가 되었을 때
                if (roomList[i].RemovedFromList)
                {
                    // 딕셔너리에 있는 데이터를 삭제합니다.
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }

            // 그렇지 않으면 roomInfo를 RoomCatalog에 추가합니다.
            RoomCatalog[roomList[i].Name] = roomList[i]; 
        }
    }

    // 룸으로 입장이 실패했을 때 호출되는 콜백 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // 네트워크 연결이 실패했을 때 return 코드 번호를 이용해서 에러를 검출합니다.
        // 룸이 생성되지 않았을 때 호출합니다.
        Debug.Log($"JoinRoom Failed {returnCode} : {message}");
    }

    // 해당 로비에 룸 목록의 변경 사항이 있다면 호출되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        RoomCreateObject();
    }
}
