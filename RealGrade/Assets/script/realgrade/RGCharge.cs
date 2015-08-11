using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class RGCharge : FageStateMachine {

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			switch (PlayerPrefs.GetInt("selected")) {
			case 1:
				DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT1))); break;
			case 2:
				DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT2))); break;
			case 3:
				DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT3))); break;
			}
		}
	}

	public	void OnClickShow() {
		ShowOptions op = new ShowOptions();
		op.resultCallback = OnAds;
		Advertisement.Show(null, op);
		FageAnalytics.LogAdEvent(FageAnalytics.ACTION_SHOW, PlayerPrefs.GetInt("coin"));
	}

	void OnAds (ShowResult result) {
		int coin = PlayerPrefs.GetInt("coin");
		switch(result) {
		case ShowResult.Finished:
			coin = Mathf.Min(coin+3, 999);
			PlayerPrefs.SetInt("coin", coin);
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.UPDATE)));
			FageAnalytics.LogAdEvent(FageAnalytics.ACTION_DONE, coin);
			FageAnalytics.LogCoinEvent(FageAnalytics.ACTION_GET, 3);
			break;
		case ShowResult.Skipped:
			FageAnalytics.LogAdEvent(FageAnalytics.ACTION_SKIP, coin);
			break;
		case ShowResult.Failed:
			FageAnalytics.LogAdEvent(FageAnalytics.ACTION_FAIL, coin);
			break;
		}
	}
}
