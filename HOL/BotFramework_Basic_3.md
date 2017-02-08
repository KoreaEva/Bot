## Microsoft Bot Framework 시작해보기 (3) Bot Application 배포

지난 포스팅까지는 간단한 Bot Application을 실행 시켜보고 Bot Emulator를 통해서 테스트해 보는 부분까지 진행해 보았습니다. 이번에는 만들어진 Bot Application을 Azure에 배포해 보도록 하겠습니다. 
 
 Visual Studio 에서 프로젝트 이름 위해서 마우스 오른쪽 버튼을 클릭하면 아래와 같은 메뉴가 나타납니다. 영문판에서는 Publish 그리고 한글 판에서는 게시라고 되어 있는 메뉴가 나타납니다. 해당 메뉴를 선택하게 되면 어디에 배포할 것인지를 선택할 수 있는 창이 나타납니다.<br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-1.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-1.png)


여기서는 Microsoft Azure App Service를 선택합니다. Microsoft Azure App Service는 PaaS 형태로 제공되는 웹 서버 입니다.<br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-2.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-2.png)


App Service를 선택하면 자세한 정보를 입력할 수 있는 창이 추가로 나타납니다. 기본적으로 Azure를 사용할 수 있는 계정이 준비되어 있어야 이후 부분은 계속 진행 해 볼 수 있습니다.<br>
 아래 그림에서는 Visual Studio Ultimate with MSDN 에서 제공하고 있는 Azure 계정을 사용하는 것을 볼 수 있습니다. 
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-3.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-3.png)


Web App Name은 웹서버의 이름을 정해야 합니다. 이 이름은 URL로 사용되기 때문에 중복되면 안됩니다. 이외 지역과 앱 서비스의 가격 정책등을 선택하면 기본 설정이 끝납니다.<br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-4.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-4.png)


이제 나머지 설정은 기본값으로 사용해도 됩니다. 추가로 배포 모드를 Debug로 할 것인지 Release로 할 것인지를 선택해야 합니다. <br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-5.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-5.png)<br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-6.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-6.png)


이제 마지막으로 Publish 버튼을 선택하면 바로 배포가 진행됩니다. <br>
배포과정은 그리 오래 걸리지 않습니다.<br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-7.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-7.png)<br>
![https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-8.png](https://github.com/KoreaEva/Bot/blob/master/HOL/images/3-8.png)


여기까지가 만들어진 Bot Application을 배포하는 과정이었습니다. 그냥 클릭만 잘하면 여기까지 진행 될 수 있었을 껍니다. 다음 포스팅에서는 이렇게 배포된 Bot Application을 디렉토리에 등록하는 방법을 진행해 보겠습니다.