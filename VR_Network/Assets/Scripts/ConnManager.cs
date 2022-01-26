using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Photon API�� ����ϱ� ���� ���ӽ����̽�
using Photon.Pun;
using Photon.Realtime;

//��Ʈ��ũ ó�� Ŭ����
public class ConnManager : MonoBehaviourPunCallbacks
{
    //������ �� ������ ����
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";

        //���ӿ��� ����� ������� �̸��� �������� ���Ѵ�
        int num = Random.Range(0, 1000);
        PhotonNetwork.NickName = "Player " + num;

        //���ӿ� �����ϸ� ������ Ŭ���̾�Ʈ�� ������ ���� �ڵ����� ����ȭ�ϵ��� �Ѵ�
        PhotonNetwork.AutomaticallySyncScene = true;

        //���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
