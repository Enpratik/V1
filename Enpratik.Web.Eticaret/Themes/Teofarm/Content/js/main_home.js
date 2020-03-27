

    var heroImgRule = CSSRulePlugin.getRule('.hy-hero-img:after');

    window.onload = function () {
        TweenMax.to('.preload', 0.5, {
            autoAlpha: 0
        })
        TweenMax.to(heroImgRule, 1, {
            cssRule: { width: '0px' },
            ease: Power2.easeInOut,
        });
        TweenMax.staggerFrom('.hidden', 1, {
            y: 100,
            ease: Power2.easeInOut,
            delay: 1.1
        }, .4);
        TweenMax.staggerTo('.hidden', 1, {
            ease: Power2.easeInOut,
            autoAlpha: 1,
            delay: 1.1
        }, .4);
    }


    var mySwiper = new Swiper('.swiper-container', {
        // Optional parameters
        direction: 'horizontal',
        loop: true,
        slidesPerView: 3,
        spaceBetween: 30,

        // Navigation arrows
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        breakpoints: {
            1500: {
                slidesPerView: 2,
                spaceBetween: 40,
            },
            640: {
                slidesPerView: 1,
                spaceBetween: 20,
            }
        }

    });

    var mySwiper = new Swiper('.swiper-container-review', {
        // Optional parameters
        direction: 'horizontal',
        loop: true,
        slidesPerView: 1,
        spaceBetween: 20,

        // Navigation arrows
        navigation: {
            nextEl: '.swiper-review-button-next',
            prevEl: '.swiper-review-button-prev',
        }

    });


    //  HOME PAGE ACCORDION


    const accordion = document.querySelector('.accordion');
    const items = accordion.querySelectorAll('li');
    const questions = accordion.querySelectorAll('.question');

    //Lets figure out what item to click
    function toggleAccordion() {
        const thisItem = this.parentNode;

        items.forEach(item => {
            if (thisItem == item) {
                thisItem.classList.toggle('open');
                return;
            }

            item.classList.remove('open');
        });
    }
    questions.forEach(question => question.addEventListener('click', toggleAccordion));
