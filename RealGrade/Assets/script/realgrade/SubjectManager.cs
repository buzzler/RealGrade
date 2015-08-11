using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubjectManager : FageEventDispatcher {
	private	static Dictionary<string, SubjectInfo> _dictiopnary;

	void Awake() {
		_dictiopnary = new Dictionary<string, SubjectInfo> ();
		_dictiopnary.Add (SubjectCode.KOR.ToString (), new SubjectInfo(SubjectCode.KOR,		"국어", "", 1, 100));
		_dictiopnary.Add (SubjectCode.KOR_A.ToString (), new SubjectInfo(SubjectCode.KOR_A,	"국어", "A", 2, 100));
		_dictiopnary.Add (SubjectCode.KOR_B.ToString (), new SubjectInfo(SubjectCode.KOR_B,	"국어", "B", 3, 100));

		_dictiopnary.Add (SubjectCode.MAT.ToString (), new SubjectInfo(SubjectCode.MAT,		"수학", "", 4, 100));
		_dictiopnary.Add (SubjectCode.MAT_A.ToString (), new SubjectInfo(SubjectCode.MAT_A,	"수학", "A", 4, 100));
		_dictiopnary.Add (SubjectCode.MAT_B.ToString (), new SubjectInfo(SubjectCode.MAT_B,	"수학", "B", 5, 100));
		_dictiopnary.Add (SubjectCode.MAT_X.ToString (), new SubjectInfo(SubjectCode.MAT_X,	"수학", "가", 6, 100));
		_dictiopnary.Add (SubjectCode.MAT_Y.ToString (), new SubjectInfo(SubjectCode.MAT_Y,	"수학", "나", 7, 100));
		_dictiopnary.Add (SubjectCode.ENG.ToString (), new SubjectInfo(SubjectCode.ENG,		"영어", "", 8, 100));

		_dictiopnary.Add (SubjectCode.PHY.ToString (), new SubjectInfo(SubjectCode.PHY,		"물리", "", 9, 50));
		_dictiopnary.Add (SubjectCode.PHY_I.ToString (), new SubjectInfo(SubjectCode.PHY_I,	"물리", "I", 10, 50));
		_dictiopnary.Add (SubjectCode.PHY_II.ToString (), new SubjectInfo(SubjectCode.PHY_II,"물리", "II", 11, 50));

		_dictiopnary.Add (SubjectCode.CHM.ToString (), new SubjectInfo(SubjectCode.CHM,		"화학", "", 12, 50));
		_dictiopnary.Add (SubjectCode.CHM_I.ToString (), new SubjectInfo(SubjectCode.CHM_I,	"화학", "I", 13, 50));
		_dictiopnary.Add (SubjectCode.CHM_II.ToString (), new SubjectInfo(SubjectCode.CHM_II,"화학", "II", 14, 50));

		_dictiopnary.Add (SubjectCode.BIO.ToString (), new SubjectInfo(SubjectCode.BIO,		"생명과학", "", 15, 50));
		_dictiopnary.Add (SubjectCode.BIO_I.ToString (), new SubjectInfo(SubjectCode.BIO_I,	"생명과학", "I", 16, 50));
		_dictiopnary.Add (SubjectCode.BIO_II.ToString (), new SubjectInfo(SubjectCode.BIO_II,"생명과학", "II", 17, 50));

		_dictiopnary.Add (SubjectCode.GEO.ToString (), new SubjectInfo(SubjectCode.GEO,		"지구과학", "", 18, 50));
		_dictiopnary.Add (SubjectCode.GEO_I.ToString (), new SubjectInfo(SubjectCode.GEO_I,	"지구과학", "I", 19, 50));
		_dictiopnary.Add (SubjectCode.GEO_II.ToString (), new SubjectInfo(SubjectCode.GEO_II,"지구과학", "II", 20, 50));

		_dictiopnary.Add (SubjectCode.NOR.ToString (), new SubjectInfo(SubjectCode.NOR,		"일반사회", "", 21, 50));
		_dictiopnary.Add (SubjectCode.CUL.ToString (), new SubjectInfo(SubjectCode.CUL,		"사회문화", "", 22, 50));
		_dictiopnary.Add (SubjectCode.LIF.ToString (), new SubjectInfo(SubjectCode.LIF,		"생활과 윤리", "", 23, 50));
		_dictiopnary.Add (SubjectCode.MAP.ToString (), new SubjectInfo(SubjectCode.MAP,		"지리", "", 24, 50));
		_dictiopnary.Add (SubjectCode.MAP_K.ToString (), new SubjectInfo(SubjectCode.MAP_K,	"한국지리", "", 25, 50));
		_dictiopnary.Add (SubjectCode.REL.ToString (), new SubjectInfo(SubjectCode.REL,		"윤리와 사상", "", 26, 50));
		_dictiopnary.Add (SubjectCode.HIS_K.ToString (), new SubjectInfo(SubjectCode.HIS_K,	"한국사", "", 27, 50));

		_dictiopnary.Add (SubjectCode.MAP_W.ToString (), new SubjectInfo(SubjectCode.MAP_W,	"세계지리", "", 28, 50));
		_dictiopnary.Add (SubjectCode.HIS_A.ToString (), new SubjectInfo(SubjectCode.HIS_A,	"동아시아사", "", 29, 50));
		_dictiopnary.Add (SubjectCode.LAW.ToString (), new SubjectInfo(SubjectCode.LAW,		"법과정치", "", 30, 50));
		_dictiopnary.Add (SubjectCode.HIS_W.ToString (), new SubjectInfo(SubjectCode.HIS_W,	"세계사", "", 31, 50));
		_dictiopnary.Add (SubjectCode.ECO.ToString (), new SubjectInfo(SubjectCode.ECO,		"경제", "", 32, 50));
	}

	public	static SubjectInfo Find(string name) {
		if (_dictiopnary.ContainsKey (name)) {
			return _dictiopnary [name];
		} else {
			return null;
		}
	}
}

public	class SubjectInfo {
	public const int GRADES		= 8;
	public const int PROVIDER	= 7;

	private	SubjectCode _subject;
	private	string		_name;
	private	string		_tag;
	private	int			_priority;
	private	int			_max;
	private	string[][]	_cuts;

	public	SubjectCode subject { get { return _subject; } }
	public	string	name	{ get { return _name; } }
	public	string	tag		{ get { return _tag; } }
	public	int		priority{ get { return _priority; } }
	public	int		max		{ get { return _max; } }

	public	SubjectInfo(SubjectCode subject, string name, string tag, int priority, int max) {
		_subject = subject;
		_name = name;
		_tag = tag;
		_priority = priority;
		_max = max;
		_cuts = new string[GRADES][];
		for (int i = 0 ; i < GRADES ; i++) {
			_cuts[i] = new string[PROVIDER];
		}
	}

	public	void SetData(string line) {
		string[] fields = line.Split ("," [0]);
		int grade = int.Parse (fields [0]);
		SetData (grade, fields [1], fields [2], fields [3], fields [4], fields [5], fields [6], fields [7]); 
	}

	public	void SetData(int grade, string provider1, string provider2, string provider3, string provider4, string provider5, string provider6, string provider7) {
		int index = grade - 1;
		if ((index >= 0) && (index < GRADES)) {
			_cuts[index][0] = provider1;
			_cuts[index][1] = provider2;
			_cuts[index][2] = provider3;
			_cuts[index][3] = provider4;
			_cuts[index][4] = provider5;
			_cuts[index][5] = provider6;
			_cuts[index][6] = provider7;
		}
	}

	public	int GetGradeEncode(int score) {
		int avr = GetGradeAvr (score);
		int[] grades = GetGrades (score);

		float result = (float)avr * Mathf.Pow (10f, (float)PROVIDER);
		for (int i = 0; i < PROVIDER; i++) {
			result += (float)grades [i] * Mathf.Pow (10f, (float)(PROVIDER - (float)i - 1f));
		}

		return (int)result;
	}

	public	int GetGradeAvr(int score) {
		float[] avr = new float[GRADES];
		for (int i = 0; i < GRADES; i++) {
			float count = 0f;
			float sum = 0f;
			for (int j = 0; j < PROVIDER; j++) {
				if (string.IsNullOrEmpty (_cuts [i] [j]) || (_cuts [i] [j] == "-")) {
					continue;
				}
				sum += float.Parse (_cuts [i] [j]);
				count++;
			}
			avr [i] = sum / count;
		}

		for (int i = 0; i < GRADES; i++) {
			if ((float)score >= avr [i]) {
				return i + 1;
			}
		}
		return GRADES+1;
	}

	public	int[] GetGrades(int score) {
		int[] grades = new int[PROVIDER];
		for (int i = 0 ; i < PROVIDER ; i++) {
			for (int j = 0 ; j < GRADES ; j++) {
				if (string.IsNullOrEmpty(_cuts[j][i]) || (_cuts[j][i] == "-")) {
					grades[i] = 0;
				} else if (score >= float.Parse(_cuts[j][i])){
					grades[i] = j+1;
					break;
				}
			}
		}
		return grades;
	}
}