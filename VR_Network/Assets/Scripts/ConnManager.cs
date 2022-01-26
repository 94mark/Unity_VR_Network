using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Photon API를 사용하기 위한 네임스페이스
using Photon.Pun;
using Photon.Realtime;

//네트워크 처리 클래스
public class ConnManager : MonoBehaviourPunCallbacks
{
    //시작할 때 서버에 접속
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";

        //게임에서 사용할 사용자의 이름을 랜덤으로 정한다
        int num = Random.Range(0, 1000);
        PhotonNetwork.NickName = "Player " + num;

        //게임에 참여하면 마스터 클라이언트가 구성한 씬에 자동으로 동기화하도록 한다
        PhotonNetwork.AutomaticallySyncScene = true;

        //서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
