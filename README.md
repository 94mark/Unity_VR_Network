
VR Networkgame 제작
----
### 프로젝트 개요
	1. 개발 인원/기간 및 포지션
		- 1인, 총 5일 소요
		- 프로그래밍 (VR환경 구축 및 개발)
	2. 개발 환경
		- Unity 2021.3.16f
		- 멀티 환경 : Photon Unity Networking2
		- VR HMD : Oculus Quest 2
		- 언어 : C#
		- OS : Window 10
			
### 개발 단계
	1. 기획의도 : VR 탁구 게임 'Eleven Table Tennis'을 벤치마키하여 VR 멀티플레이 게임 제작을 목표
	2. Photon 서버 세팅
	3. 게임 플레이 관련 이동 및 회전 기능/데이터 동기화
	4. RPC를 이용해 공격 애니메이션, 피격 및 체력 동기화
	5. 음성 채팅 기능 구현

### 핵심 구현 내용 
	1. Photon Server Settings(Photon PUN2)
		- PhotonNetwork.AutomaticallySyncScene 속성에 true를 넣어 게임 참여 시 마스터 클라이언트가 구성한 scene에 자동으로 동기화
		- PhotonNetwork.ConnectUsingSettings() 함수를 호출하여 네임 서버 - 마스터 서버로 접속 시도
		- PhotonNetwork.JoinLobby() 함수를 호출하여 로비에 조인
		- RoomOptions 객체를 JoinedOrCreateRoom() 함수에 넘겨 게임 서버로 접속 시도
		- Random.insideUnitCircle을 사용해 게임 서버에 접속한 캐릭터가 특정 반경 내 랜덤한 위치에서 생성되도록 구현
	2. 위치와 회전 동기화(Network synchronization)
		- 캐릭터 메카님 애니메이션 구성(idle, walk, attack)
### 문제 해결 내용
	1. 
