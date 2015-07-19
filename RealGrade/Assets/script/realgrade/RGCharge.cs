using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class RGCharge : FageStateMachine {

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
		}
	}

	public	void OnClickShow() {
		ShowOptions op = new ShowOptions();
		op.resultCallback = OnAds;
		Advertisement.Show(null, op);
	}

	void OnAds (ShowResult result) {
		switch(result) {
		case ShowResult.Finished:
			int coin = PlayerPrefs.GetInt("coin");
			PlayerPrefs.SetInt("coin", Mathf.Min(coin+3, 999));
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.UPDATE)));
			break;
		case ShowResult.Skipped:
		case ShowResult.Failed:
			break;
		}
	}
}
