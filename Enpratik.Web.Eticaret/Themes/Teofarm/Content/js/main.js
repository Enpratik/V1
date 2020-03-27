/* ======= Open Mobile Menu ======= */

var menuTrigger = document.getElementById('menu-trigger'),
    body = document.getElementsByTagName('body')[0],
    overlay = document.getElementsByClassName('hy-overlay')[0];

menuTrigger.addEventListener('click', function(e) {
    e.preventDefault();
    body.classList.add('open-menu');
});

overlay.addEventListener('click', function() {
    body.classList.remove('open-menu');
})

window.addEventListener('scroll', function() {
    var h = window.pageYOffset;
    if (h <= 1) {
        document.getElementsByTagName('header')[0].classList.remove('sticky');
    } else {
        document.getElementsByTagName('header')[0].classList.add('sticky');
    }

})