using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RGUpdate : FageStateMachine {
	public	Slider	progressBar;
	public	Text	message;
	public	Button	buttonRetry;
	private	float	_target;
	private	string	_temp;

	void OnEnable() {
		_target = 0;
	}

	void OnDisable() {
		SetPercentageNow (0);
		ReserveState ("RGUpdateRequest", true);
	}

	void Update() {
		float temp =_target -  progressBar.value;
		if (Mathf.Abs (temp) > 0.001) {
			progressBar.value += temp * 0.08f;
		} else {
			progressBar.value = _target;
		}

		if ((_temp != null) && (progressBar.value == _target)) {
			string r = _temp;
			_temp = null;
			base.ReserveState (r);
		}
	}

	public	bool IsAnimating() {
		return (progressBar.value != _target);
	}

	public	void SetPercentageNow(int percentage) {
		progressBar.value = Mathf.Clamp ((float)percentage / 100f, 0f, 1f);
	}

	public	void SetPercentage(int percentage) {
		_target = Mathf.Clamp((float)percentage / 100f, 0f, 1f);
	}

	public	void SetMessage(string msg, bool visibleRetry = false) {
		message.text = msg;
		buttonRetry.gameObject.SetActive (visibleRetry);
	}

	public	void OnClickRetry() {
		SetState ("RGUpdateRequest");
	}

	public override void ReserveState (string id) {
		_temp = id;
	}
}
