	
## Microsoft Bot Framework 시작해보기 (4) Bot 등록과 스카이프에서 테스트 하기 

테슽트할 Bot을 만들어서 배포하는 과정이 끝나고 나면 이제 Bot을 디렉토리에 등록하는 과정이 남아 있습니다. 디렉토리 등록 페이지는 아래 사이트에 접속해서 Register a bot 메뉴를 통해서 찾아 볼 수 있습니다. 
https://dev.botframework.com 
Bot을 등록해야 하는데 몇 몇 항목들은 필수 항목들이기 때문에 반드시 입력해야 합니다. 
이름이나 설명은 잘 달아주면 되고 Bot을 배포한 URL은 정확하게 입력해 주어야 합니다. 
저 같은 경우에는 https://winkeybot.azurewebsites.net 에 배포 했다. 하지만 그렇다고 해서 그렇게 입력하면 나중에 연결이 되지 않습니다. 실제로 Bot이 통신하고 있는 경로는 해당 URL 하위에 /api/messages 이다. 그래서 Full URL로 입력하면 아래와 같이 입력헤야 합니다. 

https://winkeybot.azurewebsites.net/api/messages

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-1.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-1.png)

URL을 입력하고 나서는 Generate Microsoft App ID and password 버튼을 눌러서 Microsoft App ID를 입력해야 합니다. 이때 App Password도 함께 생성되는데 이 때 한번만 보여주기 때문에 따로 잘 보관해야 합니다. 

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-2.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-2.png)

마지막으로 Publisher profile을 입력해야 하는데 프라이버시 정책과 약관을 웹에 게시해 놓고 해당 URL을 입력해야 합니다. 이 부분은 필수 입력이기 때문에 반드시 입력해야 합니다. 

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-3.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-3.png)

여기까지 입력했으면 변경 내용을 저장합니다. 
 이제 다시 Visual Studio에서 해당 Bot 의 소스코드 중에 설정 파일에 App ID, App password를 설정해야 합니다. 
 Web.config 파일을 열어서 MicrosoftAppId, MicrosoftAppPassword 부분을 수정합니다. 

~~~
<? xml version = "1.0" encoding = "utf-8" ?>
       < !--
       For more information on how to configure your ASP.NET application, please visit
       http://go.microsoft.com/fwlink/?LinkId=301879
-->
< configuration >
< appSettings >
        < !--update these with your appid and one of your appsecret keys-->
        < add key = "MicrosoftAppId" value = "[GUID]" />
        < add key = "MicrosoftAppPassword" value = "[PASSWORD]" />
</ appSettings >
~~~


이제 Test connection to Bot framewok에 있는 Test 버턴을 눌렀을 때 잘 연결이 

"Endpoint authorzation succeeded." 

메시지가 나오면 연결이 잘 이뤄진 상태가 됩니다. 

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-4.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-4.png)


이제 마지막으로 스카이프를 선택해서 Bot Framework을 바로 테스트해 볼 수 있습니다.

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-5.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-5.png)

스카이프가 잘 실행 되면 아래와 같이 바로 테스트해 볼 수 있습니다. 

![https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-6.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/4-6.png)


여기까지 기본적인 사용법이었습니다. 
감사합니다. 