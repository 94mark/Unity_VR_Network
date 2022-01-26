using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 3.0f;
    public float rotSpeed = 200.0f;
    public GameObject cameraRig;
    public Transform myCharacter;
    public Animator anim;

    //�������� ���� �����͸� ������ ����
    Vector3 setPos;
    Quaternion setRot;
    float dir_speed = 0;

    void Start()
    {
        //������� ������Ʈ�� ���� ī�޶� ��ġ�� Ȱ��ȭ
        cameraRig.SetActive(photonView.IsMine);
    }

    void Update()
    {
        Move();
        Rotate();
    }

    //�̵� ���
    void Move()
    {
        if(photonView.IsMine)
        {
            //�޼� �潺ƽ�� ���� ���� ������ ĳ������ �̵� ������ ���Ѵ�
            Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
            Vector3 dir = new Vector3(stickPos.x, 0, stickPos.y);
            dir.Normalize();

            //ĳ������ �̵� ���� ���͸� ī�޶� �ٶ󺸴� ������ �������� �ϵ��� ����
            dir = cameraRig.transform.TransformDirection(dir);
            transform.position += dir * moveSpeed * Time.deltaTime;

            //����, �޼� �潺ƽ�� ����̸� �� �������� ĳ���͸� ȸ����Ų��
            float magnitude = dir.magnitude;

            if (magnitude > 0)
            {
                myCharacter.rotation = Quaternion.LookRotation(dir);
            }

            //�ִϸ����� ���� Ʈ�� ������ ������ ũ�� ����
            anim.SetFloat("Speed", magnitude);
        }
        else
        {
            //��ü ������Ʈ�� ��ġ ���� ĳ������ ȸ�� ���� �������� ���޹��� ������ ���� ������ ����ȭ
            transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * 20.0f);
            myCharacter.rotation = Quaternion.Lerp(myCharacter.rotation, setRot, Time.deltaTime * 20.0f);

            //�������� ���޹��� ������ �ִϸ����� �Ķ���� ���� ����ȭ
            anim.SetFloat("Speed", dir_speed);
        }
    }

    //ȸ�� ���
    void Rotate()
    {
        if(photonView.IsMine)
        {
            //�������� ���� ������ �¿� ���⸦ ������Ų��
            float rotH = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
            //CameraRig ������Ʈ�� ȸ����Ų��
            cameraRig.transform.eulerAngles += new Vector3(0, rotH, 0) * rotSpeed * Time.deltaTime;
        }
    }

    //������ ����ȭ�� ���� ������ ���� �� ���� ���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //���� �����͸� �����ϴ� ��Ȳ�̶��
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(myCharacter.rotation);
            stream.SendNext(anim.GetFloat("Speed"));
        }
        //���� �����͸� �����ϴ� ��Ȳ�̶��
        else if(stream.IsReading)
        {
            setPos = (Vector3)stream.ReceiveNext();
            setRot = (Quaternion)stream.ReceiveNext();
            dir_speed = (float)stream.ReceiveNext();
        }
    }
}
