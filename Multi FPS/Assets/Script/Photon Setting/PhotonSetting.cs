using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonSetting : MonoBehaviourPunCallbacks
{
    public InputField email;
    public InputField password;
    public InputField username;
    public InputField region;

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = username.text;

        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUpManager.Show("로그인 실패");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        PopUpManager.Show("회원 가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        PopUpManager.Show("회원 가입 실패");
    }

    public void SignUp()
    {
        // RegisterPlayFabUserRequest : Playfab에 유저를 등록하기 위한 클래스
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = username.text
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request, // 회원 가입에 대한 정보
            SignUpSuccess, // 회원 가입이 성공했을 때 호출되는 함수
            SignUpFailure // 회원 가입이 실패했을 때 호출되는 함수 
        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
             request,
             LoginSuccess,
             LoginFailure
        );
    }
}
