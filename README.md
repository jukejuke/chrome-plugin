# �������Chrome���������

��ǩ���ո�ָ����� WEB���

---

[toc]

***��������������ļ�manifest.json��background.js��content.js***
***[Github��https://github.com/jukejuke/chrome-plugin][1]***
### manifest.json
***��������ļ�***
```
{
	"name" : "FastRun",
	"version" : "1.0.1",
	"description" : "Launch APP ",
	"background" : { "scripts": ["background.js"] },
	"permissions" : [
		"nativeMessaging",
		"tabs",
		"file://*"
	],
	"content_scripts": [
    {
      "matches": ["file://*"],
      "js": ["content.js"]
    }
	],
	"minimum_chrome_version" : "6.0.0.0",
	"manifest_version": 2
}
```
http://xxx/* �� file://*
### background.js
***��̨�ļ�***
```
var port = null; 
var tempTabId = null;
chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
     if (request.type == "launch"){
     	
     	getCurrentTabId(connectToNativeHost);
       	//connectToNativeHost(tempTabId);
       	// ������Ϣ
       	sendResponse({tyep: 'MsgFromChrome', msg: 'Hello, I am chrome extension~'});
    }else if(request.type == "sendMsg"){
    	console.log("sendMsg...");
    	console.log(request.message);
    	getCurrentTabId(tabId);
    	
    	// ������Ϣ��ԭ��Ӧ�ã�C#)
    	port.postMessage({message: request.message,id:tempTabId});	              
    	// ������Ϣ
    	sendResponse({type: 'pageEvent', msg: 'Hello, I am chrome extension 2~'});                    
    }
	return true;
});


//onNativeDisconnect
function onDisconnected()
{
	console.log(chrome.runtime.lastError);
	console.log('disconnected from native app.');
	port = null;
}

function onNativeMessage(msg) {
	console.log('recieved message from native app: ' + JSON.stringify(msg));               
	// ������Ϣ��Content.js,106��getCurrentTabId��ȡ��tabId
    chrome.tabs.sendMessage(106, JSON.stringify(msg), function(response) { console.log(response)});
}

//connect to native host and get the communicatetion port
function connectToNativeHost(msg)
{
	var nativeHostName = "com.my_company.my_application";
	console.log(nativeHostName);
	console.log("***********************");
 	port = chrome.runtime.connectNative(nativeHostName);
	port.onMessage.addListener(onNativeMessage);
	port.onDisconnect.addListener(onDisconnected);
	// ������Ϣ��ԭ��Ӧ�ã�C#)
	port.postMessage({message: msg});	
 } 
 
 /**
 * ��ȡ��ǰѡ�id
 * @param callback - ��ȡ��id��Ҫִ�еĻص�����
 */
function getCurrentTabId(callback) {
    chrome.tabs.query({active: true, currentWindow: true}, function (tabs) {
        if (callback) {
            callback(tabs.length ? tabs[0].id: null);
        }
    });
}

function tabId(vTabId){
	console.log(vTabId);
	tempTabId = vTabId;
}
```

### content.js
***������ҳ�潻����Ƕ�뵽ҳ��***
```
var launch_message;
var port = null; 

// ����¼���������ȡWebҳ�淢����Ϣ,�¼���myCustomEvent
document.addEventListener('myCustomEvent', function(evt) {
	console.log("myCustomEvent*****");
	// ������Ϣ��background.js
  chrome.runtime.sendMessage({type: "launch", message: evt.detail}, function(response) {
    console.log(response)
    
    // ������Ϣ��ҳ�棬ҳ�洴��CustomEvent�¼�
    var evt = document.createEvent("CustomEvent");
	evt.initCustomEvent('pageEvent', true, false, response);
	// fire the event
	document.dispatchEvent(evt);
  });
}, false);
// ����¼���������ȡWebҳ�淢����Ϣ,�¼���sendMsg
document.addEventListener('sendMsg', function(evt) {
	console.log("sendMsg*****");
	// ������Ϣ��background.js
	chrome.runtime.sendMessage({type: "sendMsg", message: evt.detail}, function(response) {
  	console.log(response)
	});
}, false);

// �˷�ʽ����background.js���ܼ��ؿؼ�
//chrome.runtime.onMessege.addListener(function(request, sender, sendResponse) {
//	console.log("content receive msg ...");
//	console.log(request.message);
//});

// 1 2 ����background.js������Ϣ
// 1. Injected script script
addEventListener('message', function(event) {
    if (event.data && event.data.extensionMessage) {
        alert(event.data.extensionMessage);
    }
});
// 2. Content script which injects the script:
chrome.extension.onMessage.addListener(function(message) {
    postMessage({extensionMessage: message}, '*');
    return true;
});
```
### Webҳ��DEMO
```
<!DOCTYPE HTML>
<html>
<head>
<script>
function startApp() {
	var evt = document.createEvent("CustomEvent");
	evt.initCustomEvent('myCustomEvent', true, false, "im information");
	// fire the event
	document.dispatchEvent(evt);
}
function sendMsg() {
	var evt = document.createEvent("CustomEvent");
	evt.initCustomEvent('sendMsg', true, false, "test sendMsg");
	// fire the event
	document.dispatchEvent(evt);
}
function sendMsg2(){
	chrome.runtime.sendMessage({type: "sendMsg", message: "test sendMsg2"}, function(response) {
		console.log("************************");
  	console.log(response)
  	console.log("************************");
	});
}

document.addEventListener('pageEvent', function(evt) {
	console.log("pageEvent*****");
  console.log(evt);
}, false);

</script>
</head>
<body>

<button type="button" onClick="startApp()" id="startApp">startApp</button>
<button type="button" onClick="sendMsg()" id="sendMsg">sendMsg</button>
<button type="button" onClick="sendMsg2()" id="sendMsg2">sendMsg2</button>
</body>
</html>
```
### Native Messaging����
***����manifest.json �ŵ�c��NativeĿ¼��***
```
{
	"name": "com.my_company.my_application",
	"description": "Chrome sent message to native app.",
	"path": "C:\\MyApp.exe",
	"type": "stdio",
	"allowed_origins": [
		"chrome-extension://akldhikhjfejogdlcbfmhgipldfipgpp/"
	]
}
```
***path:c#Ӧ�ó���λ��***
***akldhikhjfejogdlcbfmhgipldfipgpp:��չ����ID***
***ע���ע��ԭ��Ӧ��***
```
Windows Registry Editor Version 5.00

[HKEY_CURRENT_USER\Software\Google\Chrome\NativeMessagingHosts\com.my_company.my_application]
@="C:\\\\Native\\\\manifest.json"
```

***c# ���򿪷�***
***Program.cs***
```
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    static class Program
    {
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
            {
                string chromeMessage = OpenStandardStreamIn();
                Application.Run(new Form1(chromeMessage));
            }
            else
            { Application.Run(new Form1()); }
        }
        
        // �ӱ�׼����ȡbackground.js������Ϣ
        private static string OpenStandardStreamIn()
        {
            //// We need to read first 4 bytes for length information  
            Stream stdin = Console.OpenStandardInput();
            int length = 0;
            byte[] bytes = new byte[4];
            stdin.Read(bytes, 0, 4);
            length = System.BitConverter.ToInt32(bytes, 0);

            string input = "";
            for (int i = 0; i < length; i++)
            {
                input += (char)stdin.ReadByte();
            }

            return input;
        }  
    }
}
```
***Form.cs***
```
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string name)
        {
            InitializeComponent();
            this.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendMsg(txtContent.Text);
        }
        
        // ���׼�����д�����ݣ����͵�background.js
        private void sendMsg(string content)
        {
            // {"text":"48BA4E4DA17F"} {"text":"48-BA-4E-4D-A1-7F"}
            string msg = "{\"tabId\":"+this.Text+",\"text\":\"" + content + "\"}";
           // Console.Write(msg.Length + msg);
            //msg = "{\"text\":\"48-BA-4E-4D-A1-7F,1C-4D-70-60-CB-32,00-50-56-C0-00-01,00-50-56-C0-00-08,1C-4D-70-60-CB-2E,1E-4D-70-60-CB-2E,1C-4D-70-60-CB-2F\"}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(msg);
 
            var stdout = Console.OpenStandardOutput();
            stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
            stdout.Write(bytes, 0, bytes.Length);
            stdout.Flush();
            //Console.ReadKey();
        }

        private void btnReceiveMsg_Click(object sender, EventArgs e)
        {
            txtReceiveMsg.Text = OpenStandardStreamIn();
        }

        // �ӱ�׼��������ȡ����
        private static string OpenStandardStreamIn()
        {
            //// We need to read first 4 bytes for length information  
            Stream stdin = Console.OpenStandardInput();
            int length = 0;
            byte[] bytes = new byte[4];
            stdin.Read(bytes, 0, 4);
            length = System.BitConverter.ToInt32(bytes, 0);

            string input = "";
            for (int i = 0; i < length; i++)
            {
                input += (char)stdin.ReadByte();
            }

            return input;
        }  
    }
}
```


  [1]: https://github.com/jukejuke/chrome-plugin