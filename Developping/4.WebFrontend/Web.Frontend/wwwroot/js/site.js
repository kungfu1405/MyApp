$(document).ready(function () { 
    //cookie lang
    var lang = getCookie("lang");
    if (lang == undefined || lang == "") {
        setCookie("lang", "vn", 365);
        lang = getCookie("lang");
    }

    $(".nav_lang label").on("click", function () {
        $(".nav_lang ul").css("display", "flex");
    });    

    $(".nav_lang ul span").on("click", function () {
        var lng = $(this).attr("data-lang");
        setCookie("lang", lng, 365);
        $(".nav_lang ul").css("display", "none");
        $(".nav_lang label").html(lng);
    });

    $(".hashtag").each(function () {
        var src = $(this).attr("href");
        $(this).attr("href", src.replace("#", ""));
    });

    $(document).on("click", 'a[href^="/Experience/"], a.expUri', function (e) {
        e.preventDefault();
        var url = window.location.href;
        console.log(url);
        $("a.exp-control").attr("data-url", url);

        console.log($(this).attr("href"));
        var href = $(this).attr("href");
        $(".experiencePreview iframe").attr("src", href);
        $(".experiencePreview").addClass("showUp");
        window.history.pushState({ href: href }, '', href);
    });
});

function imgError(image) {
    image.onerror = "";
    image.src = "/img/icon-no-image.svg";
    return true;
}

function markItem(obj) {
    var itm = $(obj).find("ion-icon");
    var marked = false;
    var name = itm.attr("name");
    if (name == "bookmark")
        marked = true;

    if (marked) {
        itm.attr("name", "bookmark-outline");
    }
    else {
        itm.attr("name", "bookmark");
    }    
}

function activeNavBar(nav_txt) {
    $(".heade-menu").find("a.active").removeClass("active");
    $(".heade-menu").find("a." + nav_txt).addClass("active");
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie(cname) {
    var username = getCookie(cname);
    if (username != "") {
        console.log("COOKIE " + username);
    } else {
        console.log("COOKIE not found");
    }
}
function OnLoad() {
    if (localStorage.getItem('pageYOffset')) {
        window.scrollBy(0, localStorage.getItem('pageYOffset'));
    }
}
function TypeClick() {

    var pageYOffset = window.pageYOffset;
    localStorage.setItem('pageYOffset', pageYOffset);
}
function checkNullString(value) {    
    if (value == undefined || value == null) {
        return "";
    }
    else {
        return value;
    }
}
function downPreviewExperience(obj) {
    if ($(".experiencePreview").hasClass("showUp")) {
        $(".experiencePreview").removeClass("showUp");
        var url = $(obj).attr("data-url");
        console.log(url);
        window.history.pushState({ href: url }, '', url);
    }
    else {
        location.href = "/";
    }
}
