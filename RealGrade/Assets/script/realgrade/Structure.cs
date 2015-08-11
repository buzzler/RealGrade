using UnityEngine;
using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public	class GodRoot {
	public	static GodRoot instance;

	public	int				year;
	public	Notify[]		notifies;
	public	ExamGroup[]		examGroups;
	public	SubjectGroup[]	subjectGroups;

	public	GodRoot(string source) {
		XmlTextReader reader = new XmlTextReader(new StringReader(source));
		reader.Read();
		if ((reader.NodeType != XmlNodeType.Element) || (reader.Name != "GodRoot")){
			throw new UnityException();
		}
		year = int.Parse(reader.GetAttribute("year"));

		ArrayList listNotifies = new ArrayList();
		ArrayList listExamGroups = new ArrayList();
		ArrayList listSubjectGroups = new ArrayList();

		while (reader.Read()) {
			if (reader.NodeType == XmlNodeType.Element) {
				switch (reader.Name) {
				case "Notify":
					listNotifies.Add(new Notify(reader));
					break;
				case "ExamGroup":
					listExamGroups.Add(new ExamGroup(reader));
					break;
				case "SubjectGroup":
					listSubjectGroups.Add(new SubjectGroup(reader));
					break;
				}
			}
		}

		notifies = listNotifies.ToArray(typeof(Notify)) as Notify[];
		examGroups = listExamGroups.ToArray(typeof(ExamGroup)) as ExamGroup[];
		subjectGroups = listSubjectGroups.ToArray(typeof(SubjectGroup)) as SubjectGroup[];
		instance = this;
	}
}

public	class Notify {
	public	string expiredate;
	public	string message;

	public	Notify(XmlTextReader reader) {
		expiredate = reader.GetAttribute("expiredate");
		while (reader.Read()) {
			if (reader.NodeType == XmlNodeType.EndElement) {
				break;
			} else if (reader.NodeType == XmlNodeType.CDATA) {
				message = reader.Value.Trim();
			}
		}
	}
}

public	class ExamGroup {
	public	string	id;
	public	string	name;
	public	Exam[]	exams;

	public	ExamGroup(XmlTextReader reader) {
		id = reader.GetAttribute("id");
		name = reader.GetAttribute("name");

		ArrayList listExams = new ArrayList();
		while (reader.Read()) {
			if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "ExamGroup")) {
				break;
			} else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Exam")) {
				listExams.Add(new Exam(reader));
			}
		}
		exams = listExams.ToArray(typeof(Exam)) as Exam[];
	}
}

public	class Exam {
	public	string		id;
	public	string		name;
	public	DateTime	date;

	public	Exam(XmlTextReader reader) {
		id = reader.GetAttribute("id");
		name = reader.GetAttribute("name");

		string D = id.Substring(id.IndexOf("_")+1);
		int y = int.Parse(D.Substring(0, 4));
		int m = int.Parse(D.Substring(4, 2));
		int d = int.Parse(D.Substring(6, 2));
		date = new DateTime(y, m, d);
	}
}

public	class SubjectGroup {
	public	string			id;
	public	string			name;
	public	int				numSelect;
	public	int				maxScore;
	public	string			url;
	public	SubjectGroup[]	subjectGroups;
	public	Subject[]		subjects;

	public	SubjectGroup(XmlTextReader reader) {
		id = reader.GetAttribute("id");
		name = reader.GetAttribute("name");
		numSelect = int.Parse(reader.GetAttribute("numSelect"));
		maxScore = int.Parse(reader.GetAttribute("maxScore"));
		string link = reader.GetAttribute("url");
		if (!string.IsNullOrEmpty(link)) {
			url = link;
		}

		ArrayList listSubjectGroups = new ArrayList();
		ArrayList listSubjects = new ArrayList();
		while (reader.Read()) {
			if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "SubjectGroup")) {
				break;
			} else if (reader.NodeType == XmlNodeType.Element) {
				if (reader.Name == "SubjectGroup") {
					listSubjectGroups.Add(new SubjectGroup(reader));
				} else if (reader.Name == "Subject") {
					listSubjects.Add(new Subject(reader));
				}
			}
		}
		subjectGroups = listSubjectGroups.ToArray(typeof(SubjectGroup)) as SubjectGroup[];
		subjects = listSubjects.ToArray(typeof(Subject)) as Subject[];
	}

	public	int GetSubjectCount() {
		int total = 0;
		foreach (SubjectGroup sg in subjectGroups) {
			total += sg.GetSubjectCount();
		}
		total += subjects.Length;
		return total;
	}
}

public	class Subject {
	public	string			id;
	public	string			name;
	public	string			suffix;
	public	int				maxScore;

	public	Subject(XmlTextReader reader) {
		id = reader.GetAttribute("id");
		name = reader.GetAttribute("name");
		suffix = reader.GetAttribute("suffix");
		maxScore = int.Parse(reader.GetAttribute("maxScore"));
	}
}