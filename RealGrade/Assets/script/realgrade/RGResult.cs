using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class RGResult : FageEventDispatcher {
	public	Color				colorFirst;
	public	Color				colorOther;
	public	Text				textCoin;
	public	Transform			grid;
	public	RGResultSubject		itemPrefab;
	public	RGResultSubject[]	subjects;

	void OnEnable() {
		int total = 0;
		for (int i = 1 ; i <= 20 ; i++) {
			if (!PlayerPrefs.HasKey("subject"+i.ToString())) {
				total = i - 1;
				break;
			}
		}
		
		subjects = new RGResultSubject[total];
		for (int i = 1 ; i <= total ; i++) {
			RGResultSubject item = GameObject.Instantiate<RGResultSubject>(itemPrefab);
			item.transform.SetParent(grid, false);
			subjects[i-1] = item;
			
			string code = PlayerPrefs.GetString("subject"+i.ToString());
			SubjectInfo info = SubjectManager.Find(code);
			item.SetData (info, PlayerPrefs.GetInt ("grade" + i.ToString ()), colorFirst, colorOther);
		}

//		for (int i = 1; i <= 5; i++) {
//			string code = PlayerPrefs.GetString ("subject" + i.ToString ());
//			SubjectInfo info = SubjectManager.Find (code);
//			subjects [i - 1].SetData (info, PlayerPrefs.GetInt ("grade" + i.ToString ()), colorFirst, colorOther);
//		}
	}

	void Update() {
		int coin = PlayerPrefs.GetInt("coin");
		textCoin.text = coin.ToString();

		if (Input.GetKey (KeyCode.Escape)) {
			OnClickRetry();
		}
	}

	void OnDisable() {
		int total = subjects.Length;
		for (int i = total-1 ; i >= 0 ; i--) {
			GameObject.Destroy(subjects[i].gameObject);
		}
		subjects = null;
	}

	public	void OnClickRetry() {
		switch (PlayerPrefs.GetInt("selected")) {
		case 1:
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT1))); break;
		case 2:
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT2))); break;
		case 3:
			DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT3))); break;
		}
	}

	public	void OnClickAd() {
		ShowOptions op = new ShowOptions();
		op.resultCallback = OnAds;
		Advertisement.Show(null, op);
		FageAnalytics.LogAdEvent(FageAnalytics.ACTION_SHOW, PlayerPrefs.GetInt("coin"));
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
		int coin = PlayerPrefs.GetInt("coin");
		switch(result) {
		case ShowResult.Finished:
			coin = Mathf.Min(coin+3,999);
			PlayerPrefs.SetInt("coin", coin);
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
