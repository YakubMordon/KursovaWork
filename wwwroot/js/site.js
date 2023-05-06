// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var elem = document.querySelector('.main-carousel');
var flkty = new Flickity(elem, {
    cellAlign: 'left',
    contain: true,
    wrapAround: true,
});

flkty.playPlayer();

elem.addEventListener('mouseenter', function () {
    flkty.pausePlayer();
});

elem.addEventListener('mouseleave', function () {
    flkty.playPlayer();
});



const parallax = document.querySelector('.parallax');
const parallaxBackground = document.querySelector('.parallax-background');

function setParallaxBackgroundPosition() {
    const scrollTop = window.pageYOffset;
    const parallaxOffsetTop = parallax.offsetTop;
    const parallaxHeight = parallax.offsetHeight;
    const parallaxBackgroundHeight = parallaxBackground.offsetHeight;

    if (scrollTop > parallaxOffsetTop + parallaxHeight) {
        return;
    }

    const distance = (scrollTop - parallaxOffsetTop) * 0.7;
    const parallaxBackgroundPosition = (parallaxHeight - parallaxBackgroundHeight) / 2 + distance;

    parallaxBackground.style.transform = `translate3d(0, ${parallaxBackgroundPosition}px, 0)`;
}

setParallaxBackgroundPosition();
window.addEventListener('scroll', setParallaxBackgroundPosition);


