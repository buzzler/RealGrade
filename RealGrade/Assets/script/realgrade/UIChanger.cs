using UnityEngine;
using System.Collections.Generic;

public	enum RGUI {
	NONE = 0,
	SPLASH,
	SELECT,
	INPUT,
	UPDATE,
	RESULT,
	CHARGE
}

public class UIChanger : FageEventDispatcher {
	public	static string CHANGE = "uichange";

	public	GameObject						goSplash;
	public	GameObject						goSelect;
	public	GameObject						goInput;
	public	GameObject						goUpdate;
	public	GameObject 						goResult;
	public	GameObject						goCharge;
	public	Dictionary<RGUI, GameObject>	goDictionary;

	void Awake() {
		goDictionary = new Dictionary<RGUI, GameObject> ();
		goDictionary.Add (RGUI.SPLASH, goSplash);
		goDictionary.Add (RGUI.SELECT, goSelect);
		goDictionary.Add (RGUI.INPUT, goInput);
		goDictionary.Add (RGUI.UPDATE, goUpdate);
		goDictionary.Add (RGUI.RESULT, goResult);
		goDictionary.Add (RGUI.CHARGE, goCharge);
	}

	void OnEnable() {
		AddEventListener (CHANGE, OnRequest);
	}

	void OnDisable() {
		RemoveEventListener (CHANGE, OnRequest);
	}

	private void OnRequest(FageEvent fevent) {
		UIChangerReqeust request = fevent.data as UIChangerReqeust;
		foreach (RGUI key in goDictionary.Keys) {
			goDictionary[key].SetActive(key == request.target);
		}
	}
}

public	class UIChangerReqeust {
	private	RGUI _target;
	public	RGUI target { get { return _target; } }

	public	UIChangerReqeust(RGUI target) {
		_target = target;
	}
}