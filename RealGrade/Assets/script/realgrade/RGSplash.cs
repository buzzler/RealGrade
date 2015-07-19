using UnityEngine;
using System.Collections;

public class RGSplash : FageStateMachine {
	private	float _timeNext;
	private	float _timeBegin;

	void Awake () {
		_timeNext = 2f;
		_timeBegin = Time.time;
	}

	void Update () {
		if ((Time.time - _timeBegin) > _timeNext) {
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
		}
	}
}
