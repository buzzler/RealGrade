#!/usr/bin/python
# -*- coding: utf-8 -*-
import re
import urllib2
import ftplib

SUBJECT_MAP = {
    '국어A': 'KOR_A', 
    '국어B': 'KOR_B',
    '수학A': 'MAT_A',
    '수학B': 'MAT_B',
    '영어':  'ENG',

    '물리Ⅰ': 'PHY_I',
    '물리Ⅱ': 'PHY_II',
    '화학Ⅰ': 'CHM_I',
    '화학Ⅱ': 'CHM_II',
    '생명과학Ⅰ': 'BIO_I',
    '생명과학Ⅱ': 'BIO_II',
    '지구과학Ⅰ': 'GEO_I',
    '지구과학Ⅱ': 'GEO_II',

    '생활과윤리': 'LIF',
    '윤리와사상': 'REL',
    '한국사': 'HIS_K',
    '세계사': 'HIS_W',
    '동아시아사': 'HIS_A',
    '한국지리': 'MAP_K',
    '세계지리': 'MAP_W',
    '법과정치': 'LAW',
    '경제': 'ECO',
    '사회문화': 'CUL',
}

def get_page():
    data = urllib2.urlopen('https://m.search.daum.net/search?w=tot&q=%EA%B3%A03+7%EC%9B%94+%EB%AA%A8%EC%9D%98%EA%B3%A0%EC%82%AC&rtmaxcoll=0SP&DA=0SP').read()
    return data

def get_subject(name):
    index = name.find(' ') 
    return name[0:index]

def run():
    page = get_page()

    offset = 0

    csv = open('result.csv', 'w')

    while True:
        mat = re.search("<caption class=\"screen_hide\">(.*?)</caption>(.*?)</table>", page[offset:], re.DOTALL)
        if not mat:
            break
        subject = get_subject(mat.group(1))
        section = mat.group(2)
        offset += mat.end(2)

        #print subject, SUBJECT_MAP[subject]
        csv.write("%s\n" % (SUBJECT_MAP[subject]))
        sec_offset = 0
        while True:
            mat_sec = re.search("<td class=\"screen_out\">([0-9])</td>(.*?)</tr>", section[sec_offset:], re.DOTALL)
            if not mat_sec:
                break
            grade = mat_sec.group(1)
            companies = mat_sec.group(2) # <td>(.*?)</td> 업체 7개 
            scores = re.findall("<td>(.*?)</td>", companies)
            #print grade, scores
            csv.write("%s,%s\n" % (grade, ','.join(scores)))
            sec_offset += mat_sec.end(2)
        
    csv.close()    
    
    f = open('result.csv', 'r')
    ftp = ftplib.FTP('bbulgithub.cafe24.com')
    ftp.login(user='bbulgithub', passwd='qwer!234')
    ftp.storlines('STOR result.csv', f)
    ftp.close()
    print 'Upload "result.csv" done'
    f.close()

if __name__ == '__main__':
    run()
    print 'Crawling has been completed'
