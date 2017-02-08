
## Microsoft Bot Framework 시작해보기 (2) Hello Bot Framework

Microsoft Bot Framework는 다양한 형태의 상호작용과 기능을 명시적으로 출력할 수 있는 기능들이 포함되어 있는 Framework로 Bot를 만들기 위한 좋은 시작점이 될 것입니다. 

Bot Framework는 다음과 같은 요소들이 포함되어 있습니다. 
- 각각 격리되어 있는 대화의 생성
- 예/아니오, String, Number, Emun 형태의 간단한 대화
- 자연어 처리를 위한 LUIS 연동
- SDK 소스는 Github를 통해서 공개되어 있다. https://github.com/Microsoft/botbuilder

Microsoft/BotBuilder
BotBuilder - The Microsoft Bot Builder SDK is one of three main components of the Microsoft Bot Framework. The Microsoft ...
github.com

Bot Framework는 C#과 Node.js 두 가지 개발 환경을 지원하고 있는데 여기서는 C#을 기준으로 설명할 예정입니다. 

### Bot Connector
Bot Connector는 다양한 통신 채널에 개발한 Bot을 연결할 수 있게 해 줍니다. Bot 또는 에이전트를 개발하고 나서 Microsoft Bot Framework와 호환되는 API를 노출했을 경우에 Bot Framework Connector는 사용자에게 Bot의 메시지를 전달하는 작업을 합니다. 

Microsoft Bot Connector를 사용하기 위해서는 기본적으로 다음과 요소가 필요합니다. 
1. Microsoft Account
  Bot을 등록하거나 Bot Framework 개발자 포탈에 접속할 때 필요합니다. 
2. REST 기반으로 접속 가능한 Callback 서비스로 연결할 수 있는 end point. 
3. 하나 이상의 커뮤니케이션 서비스(Skype, Slack 등등... )에 가입

### .NET에서 시작하기
Bot Framework를 사용해서 개발하기 위해서는 필요한 환경을 먼저 설치해야 합니다.

1. 필수 소프트웨어 설치 
- Visual Studio 2015 http://www.visualstudio.com 
Any Developer, Any App, Any Platform | Visual Studio
App development made easy with Visual Studio: Developer tools and services for any platform and any language. IDE, Devops, code e...
www.visualstudio.com

무료 버전인 Community Edition도 사용 가능하다. 
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-1.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-1.png)

Any Developer, Any App, Any Platform | Visual Studio
App development made easy with Visual Studio: Developer tools and services for any platform and any language. IDE, Devops, code e...
www.visualstudio.com

- Bot Framework 탬플릿 설치
- http://aka.ms/bf-bc-vstemplate 에서 탬플릿을 먼저 다운로드 받습니다.
- 다운받은 파일을 "%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\" 에 복사합니다.
 - 마지막으로 Visual Studio를 열고 C# 프로젝트를 선택해 보면 Bot Application이 포함된 것을 볼 수 있습니다. 
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-2.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-2.png)



2. Bot 실행하기
Bot 기본 템플릿에서는 샘플로 Controllers\MessagesController.cs 파일안에 Post 메소드가 이미 작성되어 있다. 여기에는 사용자가 말한 내용의 길이를 회신하는 간단한 코드가 작성되어 있다. 

~~~
[BotAuthentication]
public class MessagesController : ApiController
{
        <summary>
        POST: api/Messages
        Receive a message from a user and reply to it
        </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Type == ActivityTypes.Message)
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
                // return our reply to the user
                Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
}
~~~

이 샘플을 바로 실행 시켜보면 다음과 같이 나타나면서 아무런 동작을 하지 않는 것을 볼 수 있다. 
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-3.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-3.png)


3. Emulator
 Bot 응용 프로그램을 테스트 하기 위해서는 에뮬레이터를 설치해야 합니다.
Bot 에뮬레이터는 https://download.botframework.com/bf-v3/tools/emulator/publish.htm 에서 다운로드 받아 설치 할 수 있습니다.

Bot Emulator를 실행하고 나면 Visual Studio에서 방금 생성된 Bot 응용 프로그램일 실행하고 나서 해당 Url을 Bot Emulator에 복사한다. 그리고 MicrosoftAppId, MicrosoftApppassword 란은 비워둡니다.
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-4.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-4.png)


이제 Bot에 Hello라고 입력하면 You send Hello which was 5 characters라고 리턴되는 것을 볼 수 있습니다.
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-5.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/2-5.png)

이렇게 Bot Framework을 사용해서 Bot을 생성하고 테스트 하는 방법까지 살펴 보았습니다. 
다음 포스팅에서는 만들어진 봇을 어떻게 배포하고 디렉토리에 등록하는지 보겠습니다. 