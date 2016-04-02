using UnityEngine;
using System.Collections;

public class FageAnalytics : FageEventDispatcher {
	private	static FageAnalytics _this;
	public	GoogleAnalyticsV4 google;

	public	const string CATEGORY_CLASS		= "Class";
	public	const string CATEGORY_SUBJECT	= "Subject";
	public	const string CATEGORY_AD		= "Advertisement";
	public	const string CATEGORY_COIN		= "Coin";

	public	const string ACTION_SELECT		= "Select";
	public	const string ACTION_SCORE		= "Score";

	public	const string ACTION_SHOW		= "Show";
	public	const string ACTION_SKIP		= "Skip";
	public	const string ACTION_DONE		= "Done";
	public	const string ACTION_FAIL		= "Fail";

	public	const string ACTION_SPEND		= "Spend";
	public	const string ACTION_GET			= "Get";

	void Awake() {
		_this = this;
		google.StartSession();
	}

	void ApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			google.StopSession();
		} else {
			google.StartSession();
		}
	}

	void OnApplicationQuit() {
		google.StopSession();
	}
	
	public	static void LogScreen(RGUI ui) {
		if (_this) {
			_this.google.LogScreen(ui.ToString());
		}
	}

	public	static void LogClassEvent(long value) {
		if (_this) {
			_this.google.LogEvent(CATEGORY_CLASS, ACTION_SELECT, ACTION_SELECT+" "+CATEGORY_CLASS, value);
		}
	}

	public	static void LogSubjectSelectEvent(string subjectName, long value) {
		if (_this) {
			_this.google.LogEvent(CATEGORY_SUBJECT, ACTION_SELECT, subjectName, value);
		}
	}

	public	static void LogSubjectScoreEvent(string subjectName, long value) {
		if (_this) {
			_this.google.LogEvent(CATEGORY_SUBJECT, ACTION_SCORE, subjectName, value);
		}
	}

	public	static void LogAdEvent(string action, long value) {
		if (_this) {
			_this.google.LogEvent(CATEGORY_AD, action, action+ " "+CATEGORY_AD, value);
		}
	}

	public	static void LogCoinEvent(string action, long value) {
		if (_this) {
			_this.google.LogEvent(CATEGORY_COIN, action, action+" "+CATEGORY_COIN, value);
		}
	}
}
