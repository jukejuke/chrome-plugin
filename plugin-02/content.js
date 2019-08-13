var launch_message;
var port = null; 



document.addEventListener('myCustomEvent', function(evt) {
	console.log("myCustomEvent*****");
  chrome.runtime.sendMessage({type: "launch", message: evt.detail}, function(response) {
    console.log(response)
    var evt = document.createEvent("CustomEvent");
	  evt.initCustomEvent('pageEvent', true, false, response);
	  // fire the event
	  document.dispatchEvent(evt);
  });
}, false);
document.addEventListener('sendMsg', function(evt) {
	console.log("sendMsg*****");
	chrome.runtime.sendMessage({type: "sendMsg", message: evt.detail}, function(response) {
  	console.log(response)
	});
}, false);


//chrome.runtime.onMessege.addListener(function(request, sender, sendResponse) {
//	console.log("content receive msg ...");
//	console.log(request.message);
//});

// Injected script script
addEventListener('message', function(event) {
    if (event.data && event.data.extensionMessage) {
        alert(event.data.extensionMessage);
    }
});
// Content script which injects the script:
chrome.extension.onMessage.addListener(function(message) {
    postMessage({extensionMessage: message}, '*');
    return true;
});


//chrome.runtime.onMessage.addListener(
//  function(request, sender, sendResponse) {
//     if (request.type == "a1"){
//     	  console.log("a1...");
//       	consooe.log(request.message)
//    }else if(request.type == "a2"){
//    	  console.log("a2...");
//    	  consooe.log(request.message)
//    }
//	return true;
//});
