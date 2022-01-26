using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 3.0f;
    public float rotSpeed = 200.0f;
    public GameObject cameraRig;
    public Transform myCharacter;
    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Rotate();
    }

    //이동 기능
    void Move()
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

        if(magnitude > 0)
        {
            myCharacter.rotation = Quaternion.LookRotation(dir);
        }

        //애니메이터 블렌드 트리 변수에 벡터의 크기 전달
        anim.SetFloat("Speed", magnitude);
    }

    //회전 기능
    void Rotate()
    {

    }
}
