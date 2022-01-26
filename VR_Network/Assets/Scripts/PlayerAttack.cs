using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviourPun
{
    public Animator anim;
    public float maxHP = 10;
    public float attackPower = 2;
    public Slider hpSlider;
    public BoxCollider weaponCol;

    float curHP = 0;

    void Start()
    {
        //���� ü���� �ִ� ü������ ä��
        curHP = maxHP;
        hpSlider.value = curHP / maxHP;
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
