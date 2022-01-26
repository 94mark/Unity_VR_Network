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
            //사용자 자신의 캐릭터일 때만, 자신의 오브젝트의 AttackAnimation 함수를 실행한다
            if(photonView.IsMine)
            {
                photonView.RPC("AttackAnimation", RpcTarget.AllBuffered);
            }
        }
    }

    //공격 애니메이션 호출 함수 +RPC 애트리뷰트
    [PunRPC]
    public void AttackAnimation()
    {
        anim.SetTrigger("Attack");
    }
}
