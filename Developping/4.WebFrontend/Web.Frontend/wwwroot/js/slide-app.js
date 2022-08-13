$(document).ready(function () {
    $("#next-1").click(function () {       
        NextClick('carousel-wrapper-horizion', 'horizion-moving-style');
    });

    var gap = 0;
    function NextClick(containner_wrap, horizion_moving_style) {
        gap += 500;
        var container = $('.' + containner_wrap);
        // container.addClass(horizion_moving_style);
        container.css("transform", "translate3d(-" + gap + "px, 0px, 0px)");
        container.css(" transition-duration", "300ms");
   
    }
});