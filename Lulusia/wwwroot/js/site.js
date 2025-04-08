document.addEventListener("DOMContentLoaded", function () {
    // Attach the function to the scroll event
    //document.addEventListener('scroll', checkScrollDirection);
    //Excute setcookei
    SetLanguageByCookie();
});
//Get cookei
function getValueCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}
//Set cookei
function setValueCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires;
}
//Delete cookei
function deleteCookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}
//Set language by cookie
function SetLanguageByCookie() {
    let cookeiValue = getValueCookie('.AspNetCore.Culture');
    let element = document.getElementById('setLanguageByCookei');
    //let elementMobile = document.getElementById('setLanguageByCookeiMobile');
    if (cookeiValue && cookeiValue.includes('vi-VN')) {
        element.innerText = 'ENG';
        //elementMobile.innerText = 'ENG';
    }
    else if (cookeiValue && cookeiValue.includes('en-US')) {
        element.innerText = 'VN';
        //elementMobile.innerText = 'VN';
    }
    else {
        element.innerText = 'ENG';
        //elementMobile.innerText = 'ENG';

    }
}