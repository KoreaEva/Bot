# Pre-Training Tool #

Chatbot을 개발할 때 많은 시나리오에서 자연어 처리가 필요하다. Microsoft는 자연어 처리를 위해서 [LUIS](http://luis.ai)라는 이름의 서비스를 제공하고 있다. 
LUIS에서는 Intent와 Entity를 학습 시켜야 하며 문장 단위로 학습을 시켜야 한다. 
 학습 시키는 과정은 아래 강좌를 참조할 수 있다. 

 [https://www.youtube.com/watch?v=jWeLajon9M8](https://www.youtube.com/watch?v=jWeLajon9M8) <-- 이 동영상 이후 웹 사이트의 인터페이스가 바뀌었다. 

 문제는 대량의 단어를 한꺼번에 학습시키는 데 있다. 
 PTT(Pre-Training Tool)은 문장과 단어를 엑셀을 기반으로 대량으로 입력한 다음 한번에 LUIS가 사용하는 JSON 포멧으로 만들어 준다. 

 ## 기본 템플릿 ##
 
 기본 템플릿 파일은 아래 링크에서 참조할 수 있다. 
 [https://github.com/KoreaEva/Bot/blob/master/Tools/LUIS%20Pre-Traning%20Tool/LUIS_Data.xlsx](https://github.com/KoreaEva/Bot/blob/master/Tools/LUIS%20Pre-Traning%20Tool/LUIS_Data.xlsx)

 ## 소스코드 ##

 소스코드는 아래 링크에서 참조할 수 있다.<br>
[https://github.com/KoreaEva/Bot/tree/master/Tools/LUIS%20Pre-Traning%20Tool/YoungWook.LUIS.PTT](https://github.com/KoreaEva/Bot/tree/master/Tools/LUIS%20Pre-Traning%20Tool/YoungWook.LUIS.PTT)
 
 ## 오류시 대처방안 ##

 만약 실행하다가 엑셀 파일을 읽어들이다가 문제가 생긴다면 아래 링크에 있는 Microsoft Access Database Engine 2010 재배포 가능 패키지를 설치해 주어야 한다. 
 주의할 점은 Visual Studio는 32비트 버전이기 때문에 64비트 윈도우 버전을 사용하더라도 32비트 버전을 다운 받아서 설치해야 한다.

 [https://www.microsoft.com/ko-KR/download/details.aspx?id=13255](https://www.microsoft.com/ko-KR/download/details.aspx?id=13255)