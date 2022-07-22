using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Button connect;
    public Text currentRegion;
    public Text currentLobby;

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        currentRegion.text = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;

        switch (Data.count)
        {
            case 0 : currentLobby.text = "First Lobby";
                break;
            case 1 : currentLobby.text = "Second Lobby";
                break;
            case 2 : currentLobby.text = "Third Lobby";
                break;
        }
    }

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    // 로비에 접속했는지 확인하는 함수입니다.
    public override void OnConnectedToMaster()
    {
        switch(Data.count)
        {
            case 0 : PhotonNetwork.JoinLobby(new TypedLobby("Lobby 1", LobbyType.Default));
                break;
            case 1 : PhotonNetwork.JoinLobby(new TypedLobby("Lobby 2", LobbyType.Default));
                break;
            case 2 : PhotonNetwork.JoinLobby(new TypedLobby("Lobby 3", LobbyType.Default));
                break;
        }
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        // PhotonNetwork.LoadLevel을 사용하는 이유는 씬 동기화를 맞추기 위해 사용합니다.
        // 일반 LoadLevel은 씬 동기화가 되지 않습니다.
        PhotonNetwork.LoadLevel("Photon Room");
    }

}
