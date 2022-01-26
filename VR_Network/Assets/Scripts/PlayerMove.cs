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

    //서버에서 받은 데이터를 저장할 변수
    Vector3 setPos;
    Quaternion setRot;
    float dir_speed = 0;

    void Start()
    {
        //사용자의 오브젝트일 때만 카메라 장치를 활성화
        cameraRig.SetActive(photonView.IsMine);
    }

    void Update()
    {
        Move();
        Rotate();
    }

    //이동 기능
    void Move()
    {
        if(photonView.IsMine)
        {
            //왼손 썸스틱의 방향 값을 가져와 캐릭터의 이동 방향을 정한다
            Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
            Vector3 dir = new Vector3(stickPos.x, 0, stickPos.y);
            dir.Normalize();

            //캐릭터의 이동 방향 벡터를 카메라가 바라보는 방향을 정면으로 하도록 변경
            dir = cameraRig.transform.TransformDirection(dir);
            transform.position += dir * moveSpeed * Time.deltaTime;

            //만일, 왼손 썸스틱을 기울이면 그 방향으로 캐릭터를 회전시킨다
            float magnitude = dir.magnitude;

            if (magnitude > 0)
            {
                myCharacter.rotation = Quaternion.LookRotation(dir);
            }

            //애니메이터 블렌드 트리 변수에 벡터의 크기 전달
            anim.SetFloat("Speed", magnitude);
        }
        else
        {
            //전체 오브젝트의 위치 값과 캐릭터의 회전 값을 서버에서 전달받은 값으로 선형 보간해 동기화
            transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * 20.0f);
            myCharacter.rotation = Quaternion.Lerp(myCharacter.rotation, setRot, Time.deltaTime * 20.0f);

            //서버에서 전달받은 값으로 애니메이터 파라미터 값을 동기화
            anim.SetFloat("Speed", dir_speed);
        }
    }

    //회전 기능
    void Rotate()
    {
        if(photonView.IsMine)
        {
            //오른손의 방향 값에서 좌우 기울기를 누적시킨다
            float rotH = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
            //CameraRig 오브젝트를 회전시킨다
            cameraRig.transform.eulerAngles += new Vector3(0, rotH, 0) * rotSpeed * Time.deltaTime;
        }
    }

    //데이터 동기화를 위한 데이터 전송 및 수신 기능
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //만일 데이터를 전송하는 상황이라면
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(myCharacter.rotation);
            stream.SendNext(anim.GetFloat("Speed"));
        }
        //만일 데이터를 수신하는 상황이라면
        else if(stream.IsReading)
        {
            setPos = (Vector3)stream.ReceiveNext();
            setRot = (Quaternion)stream.ReceiveNext();
            dir_speed = (float)stream.ReceiveNext();
        }
    }
}
