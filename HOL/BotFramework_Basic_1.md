## Microsoft Bot Framework 시작해보기 (1)

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-1.jpg](https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-1.jpg)<br>
<그림>Microsoft는 Build 2016 에서 Conversation of Platform을 설명하면서 Bot Framework를 설명했다.

Microsoft CEO인 사티아 나딜라는 4월에 있었던 개발자 행사에서 봇(Bot)이 앱 다음의 자리를 차지하게 될 것이라고 이야기 한 바 있다. 사실 봇이라는 개념은 소프트웨어적이던 하드웨어 적이든 실시간으로 스스로 움직이는 능동적인 요소가 포함되는 기술에 로봇이라는 개념을 입혀놓은 형태이다. 
 검색 업체들은 이미 인터넷 봇을 만들어 전세계 홈페이지를 훌고 다니고 있기도 하다. 

시리(Siri), 구글 어시스던트 그리고 Microsoft 코타나등도 개인비서이기도 하지만 봇이라고도 할 수 있다. 

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-2.jpg](https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-2.jpg)
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-3.jpg](https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-3.jpg)<br>
<그림>Microsoft의 코타나와 애플의 시리

 하지만 그런 특정 용도로 봇이 제한적으로 활용되는게 아니라 지금은 광범위하게 활용될 시점에 와 있다. 물론 지금 시점에서는 완전히 와 닿지 않을 수도 있지만 최근 들어 새로운 기술에 나오고 시장에 적응되는 시점까지 3~4년 정도의 시간이 걸리는 점에 미루어 볼때 어느 순간 훅 하고 또 우리 곁에 와 있는 기술이 될 것이다. 

### Microsoft Bot Framework
 이런 와중에 Microsoft는 Bot을 만들 수 있는 Bot Framework을 발표 했다. Bot Framework을 사용하면 원하는 형태의 Bot을 비교적 간단하게 개발하고 배포할 수 있을 뿐 아니라 Skype, 페이스북 메신저 이외에도 다양한 채널을 통해서 접근할 수 있다. 

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-4.jpg](https://github.com/KoreaEva/Bot/blob/master/HOL/images/1-4.gif)<br>
<그림> Microsoft Bot Framework

 Microsoft Bot Framework는 3가지 요소로 구성되어 있는데 아래와 같다. 
 - Bot Connector
   Bot이 사용하는 채널을 지정하는 것으로 일반적인 문자(SMS) 이외에도 Skype, Slack 과 같ㅇ이 다양한 채널을 사용할 수 있다. 

 - Bot Builder SDKs
   Bot을 개발할 수 있는 SDK로 Bot에 필요한 API를 제공해 준다. 개발은 C#, Node.js 두 가지를 지원하고 있으며 Chat emulator 사용해서 미리 테스트 해볼 수 있다. 또 필요하다면 Cognitive 서비스와 함께 사용할 수 있다. 
 - Bot Directory
   개발된 Bot들은 웹 서비스 형태로 배포 되는데 배포된 Bot들의 리스트를 가지고 있는 디렉토리 서비스이다. 여기에서 필요한 Bot을 찾을 수 있다. 

 Microsoft Bot 과 관련된 추가적인 정보는 https://dev.botframework.com/ 에서 볼 수 있다. 
다음 포스팅에서는 Bot의 간단한 개발 과정을 보여드리려고 한다. ^^