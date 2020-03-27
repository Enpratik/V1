
    var productQuantity = document.getElementById('hy-quantity'),
        productQuantityMax = productQuantity.getAttribute('max'),
        productQuantityMin = productQuantity.getAttribute('min'),
        quantityIncrement = document.getElementById('hy-incrament'),
        quantityDecrement = document.getElementById('hy-decrement');

    var val = 0;

    function decrementProduct(id) {
        if (productQuantity.value <= productQuantityMin) {
            productQuantity.value = productQuantityMin;
        } else {
            val--;
            productQuantity.value = val;
            console.log(val)
        }
    }

    function incrementProduct(id) {
        val++;
        productQuantity.value = val;
        console.log(val)
    }
    productQuantity.addEventListener("blur", function (e) {
        val = e.target.value;
        console.log(val)
    })

    const chooseAddtionalProduct = document.querySelector('.addtional-product-box_wrapper');
    const additionalProducts = chooseAddtionalProduct.querySelectorAll('.additional-product-box');
    const overlay = document.getElementsByClassName('hy-overlay')[0];






    // Additional Product Modal

    function addProductSelectF() {
        additionalProducts.forEach(item => {
            if (this == item) {
                addProductModalF(item.id);
                TweenMax.to(overlay, 0.2, {
                    autoAlpha: 1,
                    opacity: 1,
                    visibility: 'visible'
                });
                return;
            }
            item.classList.remove('open-modal');
        });
    }

    additionalProducts.forEach(additionalProducts => additionalProducts.addEventListener('click', addProductSelectF));



    const additionalProductModalContainer = document.querySelector('.additional-product-detail-container');
    const addProductsModal = additionalProductModalContainer.querySelectorAll('.additional-product-detail-wrapper');

    function addProductModalF(modalId) {
        addProductsModal.forEach(item => {
            if (item.id == modalId) {
                item.classList.add('open-modal');
                setTimeout(function () {
                    TweenMax.to(item, 0.5, {
                        ease: Power3.easeOut,
                        autoAlpha: 1,
                        y: '-400px',
                        opacity: 1,
                        visibility: 'visible'
                    });
                }, 300);
                return;
            };
            item.classList.remove('open-modal');
        })
    }

    overlay.addEventListener('click', function () {
        setTimeout(function () {
            TweenMax.to(overlay, 0.1, {
                autoAlpha: 0,
                opacity: 0,
                visibility: 'hidden'
            });
        }, 400);
        addProductsModal.forEach(item => {
            TweenMax.to(item, 0.5, {
                ease: Power3.easeOut,
                autoAlpha: 1,
                y: '0px',
                opacity: 0,
                visibility: 'hidden'
            });
        })
    });

