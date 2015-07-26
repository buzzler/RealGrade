using UnityEngine;
using System.Collections;

public class RGUpdateDone : FageState {
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		int coin = PlayerPrefs.GetInt("coin");
		PlayerPrefs.SetInt("coin", coin-1);
		FageEventDispatcher.DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.RESULT)));
		FageAnalytics.LogCoinEvent(FageAnalytics.ACTION_SPEND, 1);
	}
}
