using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider weaponCol;

    void Start()
    {
        //������ �浹 ������ ��Ȱ��ȭ�Ѵ�
        DeactivateCollider();
    }

    //�ݶ��̴� Ȱ��ȭ �Լ�
    public void ActiveCollider()
    {
        weaponCol.enabled = true;
    }

    //�ݶ��̴� ��Ȱ��ȭ �Լ�
    public void DeactivateCollider()
    {
        weaponCol.enabled = false;
    }
}
