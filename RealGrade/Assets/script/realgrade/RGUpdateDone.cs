using UnityEngine;
using System.Collections;

public class RGUpdateDone : FageState {
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		FageEventDispatcher.DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.RESULT)));
	}
}
