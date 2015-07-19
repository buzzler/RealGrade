using UnityEngine;
using System.Collections;

public class RGUpdateRequest : FageState {
	private RGUpdate	_fsm;

	public override void AfterSwitch (FageStateMachine stateMachine, string beforeId) {
		base.AfterSwitch (stateMachine, beforeId);
		_fsm = stateMachine as RGUpdate;
		_fsm.SetPercentage (33);
		_fsm.SetMessage ("접속 중");

		FageEventDispatcher.AddEventListener (FageEvent.WEB_RESPONSE, onResponse);
		FageEventDispatcher.AddEventListener (FageEvent.SENSOR_OFFLINE, OnOffline);

		FageWebRequest request = new FageWebRequest ("RGUpdateRequest", "http://file.unityscene.com/rg/raw.txt");
		FageEventDispatcher.DispatchEvent (new FageEvent(FageEvent.WEB_REQUEST, request));
	}

	public override void BeforeSwitch (FageStateMachine stateMachine, string afterId) {
		base.BeforeSwitch (stateMachine, afterId);
		FageEventDispatcher.RemoveEventListener (FageEvent.WEB_RESPONSE, onResponse);
		FageEventDispatcher.RemoveEventListener (FageEvent.SENSOR_OFFLINE, OnOffline);
	}

	public override void Excute (FageStateMachine stateMachine) {
		base.Excute (stateMachine);
	}

	private	void onResponse(FageEvent fevent) {
		string raw = (fevent.data as FageWebResponse).www.text;
		PlayerPrefs.SetString ("raw", raw);

		_fsm.ReserveState ("RGUpdateParsing");
	}

	private	void OnOffline(FageEvent fevent) {
		_fsm.ReserveState ("RGUpdateRetry");
	}
}
