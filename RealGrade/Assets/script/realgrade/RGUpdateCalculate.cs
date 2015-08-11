using UnityEngine;
using System.Collections;

public class RGUpdateCalculate : FageState {
	private bool _calculated;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		RGUpdate fsm = stateMachine as RGUpdate;
		fsm.SetPercentage (99);
		fsm.SetMessage ("등급 산출 중");
		_calculated = false;
	}
	
	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
	}
	
	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
		RGUpdate fsm = stateMachine as RGUpdate;
		if (_calculated != true) {
			int total = 0;
			for (int i = 1 ; i <= 20 ; i++) {
				if (!PlayerPrefs.HasKey("subject"+i.ToString())) {
					total = i - 1;
					break;
				}
			}

			for (int i = 1; i <= total; i++) {
				string code = PlayerPrefs.GetString ("subject" + i.ToString ());
				int score = PlayerPrefs.GetInt (code);
				SubjectInfo info = SubjectManager.Find (code);

				PlayerPrefs.SetInt ("grade" + i.ToString (), info.GetGradeEncode (score));
			}

			fsm.ReserveState ("RGUpdateDone");
			_calculated = true;
		}
	}
}
