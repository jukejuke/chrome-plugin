var port = null; 
var tempTabId = null;
chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
     if (request.type == "launch"){
     		getCurrentTabId(connectToNativeHost);
       	//connectToNativeHost(tempTabId);
       	
       	sendResponse({tyep: 'MsgFromChrome', msg: 'Hello, I am chrome extension~'});
    }else if(request.type == "sendMsg"){
    	console.log("sendMsg...");
    	console.log(request.message);
    	getCurrentTabId(tabId);
    	port.postMessage({message: request.message,id:tempTabId});	              
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
	//chrome.runtime.sendMessage({type: "sendMsg", message: msg}, function(response) {
  //	console.log(response)
	//});
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
	port.postMessage({message: msg});	
 } 
 
 /**
 * 获取当前选项卡id
 * @param callback - 获取到id后要执行的回调函数
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