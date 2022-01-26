using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPun
{
    public Animator anim;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            //����� �ڽ��� ĳ������ ����, �ڽ��� ������Ʈ�� AttackAnimation �Լ��� �����Ѵ�
            if(photonView.IsMine)
            {
                photonView.RPC("AttackAnimation", RpcTarget.AllBuffered);
            }
        }
    }

    //���� �ִϸ��̼� ȣ�� �Լ� +RPC ��Ʈ����Ʈ
    [PunRPC]
    public void AttackAnimation()
    {
        anim.SetTrigger("Attack");
    }
}
