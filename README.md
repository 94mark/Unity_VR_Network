# VR Networkgame 제작

## 1. 프로젝트 개요

### 1.1 개발 인원/기간 및 포지션
- 1인, 총 5일 소요
- 프로그래밍 (VR환경 구축 및 개발)
### 1.2 개발 환경
- Unity 2020.3.16f
- 멀티 환경 : Photon Unity Network 2, Photon Voice 2 
- VR HMD : Oculus Quest 2
- 언어 : C#
- OS : Window 10
			
## 2. 개발 단계
- 기획의도 : VR 탁구 게임 'Eleven Table Tennis'을 벤치마키하여 VR 멀티플레이 게임 제작을 목표
-  Photon 서버 세팅
-  게임 플레이 관련 이동 및 회전 기능/데이터 동기화
- RPC를 이용해 공격 애니메이션, 피격 및 체력 동기화
- 음성 채팅 기능 구현

## 3. 핵심 구현 내용 
### 3.1 Photon Server Settings(Photon PUN2)
- PhotonNetwork.AutomaticallySyncScene 속성에 true를 넣어 게임 참여 시 마스터 클라이언트가 구성한 scene에 자동으로 동기화
- PhotonNetwork.ConnectUsingSettings() 함수를 호출하여 네임 서버 - 마스터 서버로 접속 시도
- PhotonNetwork.JoinLobby() 함수를 호출하여 로비에 조인
- RoomOptions 객체를 JoinedOrCreateRoom() 함수에 넘겨 게임 서버로 접속 시도
- Random.insideUnitCircle을 사용해 게임 서버에 접속한 캐릭터가 특정 반경 내 랜덤한 위치에서 생성되도록 구현
### 3.2 위치와 회전 동기화(Network synchronization)
- 캐릭터 메카님 애니메이션 구성(idle, walk, attack)
- 캐릭티 닉네임 Text UI 동기화, Canvas가 Main Camera를 바라보도록 방향 조정
### 3.3 RPC(Remote Procedure Call)를 이용해 이벤트 발생 
-  Photon View 컴포넌트를 통해 접속 중인 모든 클라이언트에서 동일하게 특정 Photon View 캐릭터의 함수를 실행하는 원격 호출 방식 사용
- MonoBehaviourPun 클래스 상속, [PunRPC] 애트리뷰트 선언, AttackAnimation() 함수 간접 호출
- OnTriggerEnter() 이벤트 함수로 Box Collider  트리거 충돌 여부 판단, 캐릭터 체력 관련 Damaged() 함수 호출
### 3.4 음성 채팅 기능 구현
-  Photon Voice Network, Use Primary Recorder
## 4. 문제 해결 내용
### 4.1 상대방 캐릭터가 조금씩 끊기는 듯한 움직임 문제 발생
- 로컬 클라이언트의 가변적인 프레임 속도와 네트워크 상의 데이터 송수신 빈도와의 차이 때문에 발생하는 현상
- 데이터 송수신 빈도를 높이면 데이터 이용량이 많아지고, 사용자의 네트워크 환경에 의해 지연 시간 (Latency)이 발생하여 좋은 해결책이 아님
- 이동값과 회전값을 서버에서 전달받은 값으로 선형 보간(Lerp)해 동기화하는 식으로 해결
- Time.deltaTime * 20f으로 보간해 실제 프레임 시간에 비례하도록 설정
### 4.2 공격 애니메이션 함수가 실행되지 않는 상황에서도 데미지가 발생하는 문제
- 무기에 Trigger가 계속 활성화되어 공격하지 않는 상황에서도 무기가 다른 오브젝트에 닿으면 데미지를 주는 문제 원인 파악
- 일반적인 상황에서는 Collider를 비활성화해두었다가 공격 애니메이션이 실행되는 순간에만 활성화시키는 이벤트 함수 구현 
- 애니메이션 동작 프레임에 함수 이벤트 키 추가(26프레임 : ActivateCollider, 29프레임 : DeActiveCollider)
- 트리거 충돌 시 체력값 처리 기능 구현
- 공격 동작 실행 후 다시 공격 애니메이션이 실행되기 전 트리거 충돌이 발생되지 않도록 Box Collider를 비활성화하는 코드를 추가하여 예외 처리
