using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class RGSplash : FageStateMachine {
	public	Image		imageLoading;
	public	Text		textComplete;
	public	GameObject	groupLoading;
	public	GameObject	groupRetry;
	public	GameObject	groupComplete;

	void Awake () {
		if (!PlayerPrefs.HasKey("coin")) {
			PlayerPrefs.SetInt("coin", 4);
		}
		Invoke("OnClickRetry", 1f);
	}

	void OnEnable() {
		AddEventListener(FageEvent.SENSOR_ONLINE, OnOnline);
		AddEventListener(FageEvent.SENSOR_OFFLINE, OnOffline);
	}

	void OnDisable() {
		RemoveEventListener(FageEvent.SENSOR_ONLINE, OnOnline);
		RemoveEventListener(FageEvent.SENSOR_OFFLINE, OnOffline);
	}

	void Update () {
		if (groupLoading.activeSelf) {
			imageLoading.rectTransform.Rotate(new Vector3(0,0,-10));
		}
	}

	private	void OnOnline(FageEvent fevent) {
		groupLoading.SetActive(true);
		groupRetry.SetActive(false);
		groupComplete.SetActive(false);
		Invoke("Load", 1f);
	}

	private	void OnOffline(FageEvent fevent) {
		groupLoading.SetActive(false);
		groupRetry.SetActive(true);
		groupComplete.SetActive(false);
		CancelInvoke("Load");
	}

	private	void Load() {
		FageWebRequest request = new FageWebRequest(name, "http://file.unityscene.com/rg/meta.txt");
		AddEventListener(FageEvent.WEB_RESPONSE, OnResponse);
		DispatchEvent(new FageEvent(FageEvent.WEB_REQUEST, request));
	}

	private	void OnResponse(FageEvent fevent) {
		RemoveEventListener(FageEvent.WEB_RESPONSE, OnResponse);
		groupLoading.SetActive(false);
		groupRetry.SetActive(false);
		groupComplete.SetActive(true);

		FageWebResponse response = fevent.data as FageWebResponse;
		string[] lines = response.www.text.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n"[0]);

		PlayerPrefs.SetString("title3", lines[0]);
		PlayerPrefs.SetString("url3", lines[1]);

		// start temporary
		PlayerPrefs.SetInt("selected", 3);
		// end

		int selected = PlayerPrefs.GetInt("selected");
		textComplete.text = PlayerPrefs.GetString("title"+selected.ToString());
		Advertisement.Initialize("56128");
		Invoke("OnClickNext", 2f);
	}

	public	void OnClickRetry() {
		if (GameObject.FindObjectOfType<ConnectionManager>().IsOnline()) {
			groupLoading.SetActive(true);
			groupRetry.SetActive(false);
			groupComplete.SetActive(false);
			Invoke("Load", 1f);
		} else {
			groupLoading.SetActive(false);
			groupRetry.SetActive(true);
			groupComplete.SetActive(false);
		}
	}

	public	void OnClickNext() {
		DispatchEvent (new FageEvent (UIChanger.CHANGE, new UIChangerReqeust(RGUI.SELECT)));
		CancelInvoke("OnClickNext");
	}
}
