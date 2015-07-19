using UnityEngine;
using System.Collections;

public class RGUpdateRetry : FageState {
	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		RGUpdate _fsm = stateMachine as RGUpdate;
		_fsm.SetMessage ("네트워크 오류\n통신 상태를 점검하신 후 재시도 바랍니다", true);
	}
	
	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}
	
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
	}
}
