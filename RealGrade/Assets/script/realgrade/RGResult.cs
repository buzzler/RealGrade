using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class RGResult : FageEventDispatcher {
	public	Color				colorFirst;
	public	Color				colorOther;
	public	Text				textCoin;
	public	RGResultSubject[] subjects;

	void OnEnable() {
		for (int i = 1; i <= 5; i++) {
			string code = PlayerPrefs.GetString ("subject" + i.ToString ());
			SubjectInfo info = SubjectManager.Find (code);
			subjects [i - 1].SetData (info, PlayerPrefs.GetInt ("grade" + i.ToString ()), colorFirst, colorOther);
		}
	}

	void Update() {
		int coin = PlayerPrefs.GetInt("coin");
		textCoin.text = coin.ToString();

		if (Input.GetKey (KeyCode.Escape)) {
			OnClickRetry();
		}
	}

	void OnDisable() {
	}

	public	void OnClickRetry() {
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
	}

	public	void OnClickAd() {
		ShowOptions op = new ShowOptions();
		op.resultCallback = OnAds;
		Advertisement.Show(null, op);
	}

	public	void OnClickRefresh() {
		int coin = PlayerPrefs.GetInt("coin");
		if (coin > 0) {
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.UPDATE)));
		} else {
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.CHARGE)));
		}
	}

	void OnAds (ShowResult result) {
		switch(result) {
		case ShowResult.Finished:
			int coin = PlayerPrefs.GetInt("coin");
			PlayerPrefs.SetInt("coin", Mathf.Min(coin+3,999));
			break;
		case ShowResult.Skipped:
		case ShowResult.Failed:
			break;
		}
	}
}
