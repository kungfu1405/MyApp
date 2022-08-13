
const gap = 16;

//const carousel = document.getElementById("carousel"),
//    content = document.getElementById("content"),
//    next = document.getElementById("next"),
//    prev = document.getElementById("prev");


//next.addEventListener("click", e => {
//    carousel.scrollBy(width + gap, 0);
//    if (carousel.scrollWidth !== 0) {
//        prev.style.display = "flex";
//    }
//    if (content.scrollWidth - width - gap <= carousel.scrollLeft + width) {
//        next.style.display = "none";
//    }
//});
//prev.addEventListener("click", e => {
//    carousel.scrollBy(-(width + gap), 0);
//    if (carousel.scrollLeft - width - gap <= 0) {
//        prev.style.display = "none";
//    }
//    if (!content.scrollWidth - width - gap <= carousel.scrollLeft + width) {
//        next.style.display = "flex";
//    }
//});

//let width = carousel.offsetWidth;
//window.addEventListener("resize", e => (width = carousel.offsetWidth));

//function NextClick(Carousel, Content, Next) {
//debugger;
var Carousels = document.getElementsByClassName("carousel-content");
for (var i = 0; i < Carousels.length; i++) {
    var item = Carousels[i];
    //var listChild = item.childNodes; //[".section-four-card-item"];
    var listChild = item.getElementsByClassName("section-four-card-item"); //[".section-four-card-item"];
    if (listChild.length < 4) {
        //item.style.display = "none";
        item.style.display = "flex";
    }
    if (listChild.length == 0 || listChild == null) {
        item.style.display = 'none';
    }
}


function NextClick(carouselId, contentId, prevId, nextId) {

    const carousel = document.getElementById(carouselId),
        content = document.getElementById(contentId),
        next = document.getElementById(nextId),
        prev = document.getElementById(prevId);

    let width = carousel.offsetWidth;
    window.addEventListener("resize", e => (width = carousel.offsetWidth));

    carousel.scrollBy(width + gap, 0);
    if (carousel.scrollWidth !== 0) {
        prev.style.display = "flex";
    }
    if (content.scrollWidth - width - gap <= carousel.scrollLeft + width) {
        next.style.display = "none";
    }

}
function PrevClick(carouselId, contentId, prevId, nextId) {
    const carousel = document.getElementById(carouselId),
        content = document.getElementById(contentId),
        next = document.getElementById(nextId),
        prev = document.getElementById(prevId);
    let width = carousel.offsetWidth;
    window.addEventListener("resize", e => (width = carousel.offsetWidth));

    carousel.scrollBy(-(width + gap), 0);
    if (carousel.scrollLeft - width - gap <= 0) {
        prev.style.display = "none";
    }
    if (!content.scrollWidth - width - gap <= carousel.scrollLeft + width) {
        next.style.display = "flex";
    }


}


