using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubjectManager : FageEventDispatcher {
	private	static Dictionary<string, SubjectInfo> _dictiopnary;

	void Awake() {
		_dictiopnary = new Dictionary<string, SubjectInfo> ();
		_dictiopnary.Add (SubjectCode.C_KOR_A.ToString (), new SubjectInfo(SubjectCode.C_KOR_A,		"국어", "A", 1, 100));
		_dictiopnary.Add (SubjectCode.C_KOR_B.ToString (), new SubjectInfo(SubjectCode.C_KOR_B,		"국어", "B", 2, 100));
		_dictiopnary.Add (SubjectCode.C_MAT_A.ToString (), new SubjectInfo(SubjectCode.C_MAT_A,		"수학", "A", 3, 100));
		_dictiopnary.Add (SubjectCode.C_MAT_B.ToString (), new SubjectInfo(SubjectCode.C_MAT_B,		"수학", "B", 4, 100));
		_dictiopnary.Add (SubjectCode.C_ENG.ToString (), new SubjectInfo(SubjectCode.C_ENG,			"영어", "", 5, 100));
		_dictiopnary.Add (SubjectCode.C_PHY_I.ToString (), new SubjectInfo(SubjectCode.C_PHY_I,		"물리", "I", 6, 50));
		_dictiopnary.Add (SubjectCode.C_PHY_II.ToString (), new SubjectInfo(SubjectCode.C_PHY_II,	"물리", "II", 7, 50));
		_dictiopnary.Add (SubjectCode.C_CHM_I.ToString (), new SubjectInfo(SubjectCode.C_CHM_I,		"화학", "I", 8, 50));
		_dictiopnary.Add (SubjectCode.C_CHM_II.ToString (), new SubjectInfo(SubjectCode.C_CHM_II,	"화학", "II", 9, 50));
		_dictiopnary.Add (SubjectCode.C_BIO_I.ToString (), new SubjectInfo(SubjectCode.C_BIO_I,		"생명과학", "I", 10, 50));
		_dictiopnary.Add (SubjectCode.C_BIO_II.ToString (), new SubjectInfo(SubjectCode.C_BIO_II,	"생명과학", "II", 11, 50));
		_dictiopnary.Add (SubjectCode.C_GEO_I.ToString (), new SubjectInfo(SubjectCode.C_GEO_I,		"지구과학", "I", 12, 50));
		_dictiopnary.Add (SubjectCode.C_GEO_II.ToString (), new SubjectInfo(SubjectCode.C_GEO_II,	"지구과학", "II", 13, 50));
		_dictiopnary.Add (SubjectCode.C_CUL.ToString (), new SubjectInfo(SubjectCode.C_CUL,			"사회문화", "", 14, 50));
		_dictiopnary.Add (SubjectCode.C_LIF.ToString (), new SubjectInfo(SubjectCode.C_LIF,			"생활과 윤리", "", 15, 50));
		_dictiopnary.Add (SubjectCode.C_MAP_K.ToString (), new SubjectInfo(SubjectCode.C_MAP_K,		"한국지리", "", 16, 50));
		_dictiopnary.Add (SubjectCode.C_REL.ToString (), new SubjectInfo(SubjectCode.C_REL,			"윤리와 사상", "", 17, 50));
		_dictiopnary.Add (SubjectCode.C_HIS_K.ToString (), new SubjectInfo(SubjectCode.C_HIS_K,		"한국사", "", 18, 50));
		_dictiopnary.Add (SubjectCode.C_MAP_W.ToString (), new SubjectInfo(SubjectCode.C_MAP_W,		"세계지리", "", 19, 50));
		_dictiopnary.Add (SubjectCode.C_HIS_A.ToString (), new SubjectInfo(SubjectCode.C_HIS_A,		"동아시아사", "", 20, 50));
		_dictiopnary.Add (SubjectCode.C_LAW.ToString (), new SubjectInfo(SubjectCode.C_LAW,			"법과정치", "", 21, 50));
		_dictiopnary.Add (SubjectCode.C_HIS_W.ToString (), new SubjectInfo(SubjectCode.C_HIS_W,		"세계사", "", 22, 50));
		_dictiopnary.Add (SubjectCode.C_ECO.ToString (), new SubjectInfo(SubjectCode.C_ECO,			"경제", "", 23, 50));

		_dictiopnary.Add (SubjectCode.B_KOR.ToString (), new SubjectInfo(SubjectCode.B_KOR,		"국어", "", 1, 100));
		_dictiopnary.Add (SubjectCode.B_MAT_X.ToString (), new SubjectInfo(SubjectCode.B_MAT_X,	"수학", "가", 2, 100));
		_dictiopnary.Add (SubjectCode.B_MAT_Y.ToString (), new SubjectInfo(SubjectCode.B_MAT_Y,	"수학", "나", 3, 100));
		_dictiopnary.Add (SubjectCode.B_ENG.ToString (), new SubjectInfo(SubjectCode.B_ENG,		"영어", "", 4, 100));
		_dictiopnary.Add (SubjectCode.B_HIS_K.ToString (), new SubjectInfo(SubjectCode.B_HIS_K,	"한국사", "", 5, 50));
		_dictiopnary.Add (SubjectCode.B_PHY_I.ToString (), new SubjectInfo(SubjectCode.B_PHY_I,	"물리", "I", 6, 50));
		_dictiopnary.Add (SubjectCode.B_CHM_I.ToString (), new SubjectInfo(SubjectCode.B_CHM_I,	"화학", "I", 7, 50));
		_dictiopnary.Add (SubjectCode.B_BIO_I.ToString (), new SubjectInfo(SubjectCode.B_BIO_I,	"생명과학", "I", 8, 50));
		_dictiopnary.Add (SubjectCode.B_GEO_I.ToString (), new SubjectInfo(SubjectCode.B_GEO_I,	"지구과학", "I", 9, 50));
		_dictiopnary.Add (SubjectCode.B_CUL.ToString (), new SubjectInfo(SubjectCode.B_CUL,		"사회문화", "", 10, 50));
		_dictiopnary.Add (SubjectCode.B_LIF.ToString (), new SubjectInfo(SubjectCode.B_LIF,		"생활과 윤리", "", 11, 50));
		_dictiopnary.Add (SubjectCode.B_MAP_K.ToString (), new SubjectInfo(SubjectCode.B_MAP_K,	"한국지리", "", 12, 50));
		_dictiopnary.Add (SubjectCode.B_REL.ToString (), new SubjectInfo(SubjectCode.B_REL,		"윤리와 사상", "", 13, 50));
		_dictiopnary.Add (SubjectCode.B_MAP_W.ToString (), new SubjectInfo(SubjectCode.B_MAP_W,	"세계지리", "", 14, 50));
		_dictiopnary.Add (SubjectCode.B_HIS_A.ToString (), new SubjectInfo(SubjectCode.B_HIS_A,	"동아시아사", "", 15, 50));
		_dictiopnary.Add (SubjectCode.B_LAW.ToString (), new SubjectInfo(SubjectCode.B_LAW,		"법과정치", "", 16, 50));
		_dictiopnary.Add (SubjectCode.B_HIS_W.ToString (), new SubjectInfo(SubjectCode.B_HIS_W,	"세계사", "", 17, 50));
		_dictiopnary.Add (SubjectCode.B_ECO.ToString (), new SubjectInfo(SubjectCode.B_ECO,		"경제", "", 18, 50));

		_dictiopnary.Add (SubjectCode.A_KOR.ToString (), new SubjectInfo(SubjectCode.A_KOR,		"국어", "", 1, 100));
		_dictiopnary.Add (SubjectCode.A_MAT.ToString (), new SubjectInfo(SubjectCode.A_MAT,		"수학", "", 2, 100));
		_dictiopnary.Add (SubjectCode.A_ENG.ToString (), new SubjectInfo(SubjectCode.A_ENG,		"영어", "", 3, 100));
		_dictiopnary.Add (SubjectCode.A_HIS_K.ToString (), new SubjectInfo(SubjectCode.A_HIS_K,	"한국사", "", 4, 50));
		_dictiopnary.Add (SubjectCode.A_PHY.ToString (), new SubjectInfo(SubjectCode.A_PHY,		"물리", "", 5, 50));
		_dictiopnary.Add (SubjectCode.A_CHM.ToString (), new SubjectInfo(SubjectCode.A_CHM,		"화학", "", 6, 50));
		_dictiopnary.Add (SubjectCode.A_BIO.ToString (), new SubjectInfo(SubjectCode.A_BIO,		"생명과학", "", 7, 50));
		_dictiopnary.Add (SubjectCode.A_GEO.ToString (), new SubjectInfo(SubjectCode.A_GEO,		"지구과학", "", 8, 50));
		_dictiopnary.Add (SubjectCode.A_NOR.ToString (), new SubjectInfo(SubjectCode.A_NOR,		"일반사회", "", 9, 50));
		_dictiopnary.Add (SubjectCode.A_MAP.ToString (), new SubjectInfo(SubjectCode.A_MAP,		"지리", "", 10, 50));
		_dictiopnary.Add (SubjectCode.A_LIF.ToString (), new SubjectInfo(SubjectCode.A_LIF,		"생활과 윤리", "", 11, 50));
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
			for (int j = 0 ; j < PROVIDER ; j++) {
				_cuts[i][j] = "-";
			}
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

	public	string GetGradeEncode(int score) {
		int avr = GetGradeAvr (score);
		int[] grades = GetGrades (score);

		string str = avr.ToString ();
		foreach (int j in grades) {
			str += j.ToString();
		}
		return str;
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

		if (avr[0] < 1f) {
			return 0;
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