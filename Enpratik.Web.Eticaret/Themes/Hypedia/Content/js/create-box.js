var basketItems = [];

function addItem(key, value) {
    for (var i = 0; i < basketItems.length; i++) {
        if (basketItems[i].key == key) {
            document.getElementById(basketItems[i].value).classList.remove('select-box');
            basketItems.splice(i, 1);
            break;
        }
    }
    var obj = {
        key: key,
        value: value
    };
    basketItems.push(obj);
}

function getItem(key) {
    var result;
    basketItems.forEach(item => {
        if (item.key == key) {
            result = item.value;
        }
    });
    return result;
}


const chooseProduct = document.querySelector('.choose-product');
const products = chooseProduct.querySelectorAll('.ProductType');

function setEvent(nextSection) {
    
    if (nextSection == "choose-tampon") {
        chooseTampon = document.querySelector('.choose-tampon');
        tampons = chooseTampon.querySelectorAll('.TamponType');
        tampons.forEach(tampon => tampon.addEventListener('click', function () {
            changeSection(tampon.id, tampon.dataset.currentSection, tampon.dataset.toSection);
        }));
    }
    if (nextSection == "choose-days") {
        chooseDays = document.querySelector('.choose-days');
        days = chooseDays.querySelectorAll('.ReglDay');
        days.forEach(day => day.addEventListener('click', function () {
            changeSection(day.id, day.dataset.currentSection, day.dataset.toSection);
        }));
    }
    if (nextSection == "choose-densitys") {
        chooseDensitys = document.querySelector('.choose-densitys');
        densitys = chooseDensitys.querySelectorAll('.DensityType');
        densitys.forEach(density => density.addEventListener('click', function () {
            changeSection(density.id, density.dataset.currentSection, density.dataset.toSection);
        }));
    }
    if (nextSection == "choose-brands") {
        chooseBrands = document.querySelector('.choose-brands');
        brands = chooseBrands.querySelectorAll('.BrandName');
        brands.forEach(brand => brand.addEventListener('click', function () {
            changeSection(brand.id, brand.dataset.currentSection, brand.dataset.toSection);
        }));
    }
    if (nextSection == "choose-thinped") {
        chooseThinPed = document.querySelector('.choose-thinped');
        thinPeds = chooseThinPed.querySelectorAll('.IsThinPad');
        thinPeds.forEach(thinPed => thinPed.addEventListener('click', function () {
            changeSection(thinPed.id, thinPed.dataset.currentSection, thinPed.dataset.toSection);
        }));
    }
    if (nextSection == "choose-subscription") {
        chooseSubscription = document.querySelector('.choose-subscription');
        subscriptions = chooseSubscription.querySelectorAll('.Subscription');
        subscriptions.forEach(subscription => subscription.addEventListener('click', function () {
            changeSection(subscription.id, subscription.dataset.currentSection, subscription.dataset.toSection);
        }));
    }
    if (nextSection == "choose-additional-product") {
        chooseAddtionalProduct = document.querySelector('.choose-additional-product');
        additionalProducts = chooseAddtionalProduct.querySelectorAll('.additional-product-box');
        additionalProducts.forEach(additionalProduct => additionalProduct.addEventListener('click', function () {
            changeSection(additionalProduct.id, additionalProduct.dataset.currentSection, additionalProduct.dataset.toSection);
        }));

        additionalProducts.forEach(additionalProducts => additionalProducts.addEventListener('click', addProductSelectF));
    }
}

const overlay = document.getElementsByClassName('hy-overlay')[0];


function getProductCount() {

    $.ajax({
        type: "post",
        url: "/ProductBoxs/HypadiaUrunListesi",
       // data: "",
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (json) {
            return json;
        },
        error: function () {
            console.log("Hata oluştu. getProductCount.");
            return "0";
        }
    });

}

// section değişimlerinin yapıldığı yer
function changeSection(id, currentSection) {

    console.log("id: " + id);
    console.log("currentSection: " + currentSection);

    addItem(currentSection, id);
    console.log(JSON.stringify(basketItems));

    var nextSection = "";

    $.ajax({
        type: "post",
        url: "/ProductBoxs/BoxSteps",
        data: "{ basketItems: '" + JSON.stringify(basketItems) + "', currentSection:'" + currentSection + "' }",
        contentType: "application/json; charset=utf-8;",
        dataType: "json",
        success: function (msg) {

            console.log(msg)

            nextSection = msg.NextSection;

            console.log("nextSection: " + msg.NextSection);

            var nextElement = getNextElement(nextSection);

            console.log("nextElement : " + nextElement);

            $(nextElement).html(getHtmlData(nextSection, msg));

            $("." + nextSection + "-back-next").html(getBackNextHtml(currentSection, nextSection));

            setEvent(nextSection);
            
            document.getElementById(id).classList.toggle('select-box');

            // Ek urun listeleme alani
            if (currentSection == "choose-subscription") {
                TweenMax.to('.additional-btn-wrapper', 1, {
                    autoAlpha: 1,
                    y: 0,
                    ease: Power2.easeInOut,
                });

                var urunSayisi = getProductCount();
                if (urunSayisi == 0) {
                    nextSection = "box-is-ready";
                }
            }
           
            TweenMax.to('.' + currentSection, 1, {
                x: '-100%',
                autoAlpha: 0,
                ease: Power2.easeInOut,
                display: 'none',
                delay: .3
            });
            TweenMax.from('.' + nextSection, 1, {
                x: '100%',
                autoAlpha: 1,
                ease: Power2.easeInOut,
                display: 'none',
                delay: .3
            })
            TweenMax.to('.' + nextSection, 1, {
                x: '0%',
                autoAlpha: 1,
                ease: Power2.easeInOut,
                display: 'block',
                delay: .3
            });
            return;
        },
        error: function () {
            alert("Hata oluştu. Lütfen tekrar seçim yapmayı deneyin.");
            return;
        }
    });
}

function getBackNextHtml(currentSection, nextSection) {

    if (nextSection == "choose-product")
    {
        return '<span class="icon icon-hy-icon-left-arrow" id="lnkBack"></span><h3 class="text-center process-headline">Ürünü Seç</h3><span class="icon icon-hy-icon-right-arrow" id="lnkNext" onclick="getNext(\'' + currentSection + '\', \'' + nextSection + '\')"></span>';
    }

    if (nextSection == "choose-tampon") {

        return '<span class="icon icon-hy-icon-left-arrow" onclick="getBack(\'' + nextSection + '\',\'' + currentSection + '\')"></span><h3 class="text-center process-headline">Tampon Seç</h3><span class="icon icon-hy-icon-right-arrow" onclick="getNext(\'' + currentSection + '\',\'' + nextSection + '\')"></span>';
    }
    if (nextSection == "choose-days") {

        return '<span class="icon icon-hy-icon-left-arrow" onclick="getBack(\'' + nextSection + '\',\'' + currentSection + '\')"></span><h3 class="text-center process-headline">Regl Süreni Seç</h3><span class="icon icon-hy-icon-right-arrow" onclick="getNext(\'' + currentSection + '\',\'' + nextSection + '\')"></span>';
    }
    if (nextSection == "choose-densitys") {

        return '<span class="icon icon-hy-icon-left-arrow" onclick="getBack(\'' + nextSection + '\',\'' + currentSection + '\')"></span><h3 class="text-center process-headline">Yoğunluğunu Seç</h3><span class="icon icon-hy-icon-right-arrow" onclick="getNext(\'' + currentSection + '\',\'' + nextSection + '\')"></span>';
    }
    if (nextSection == "choose-brands") {

        return '<span class="icon icon-hy-icon-left-arrow" onclick="getBack(\'' + nextSection + '\',\'' + currentSection + '\')"></span><h3 class="text-center process-headline">Markanı Seç</h3><span class="icon icon-hy-icon-right-arrow" onclick="getNext(\'' + currentSection + '\',\'' + nextSection + '\')"></span>';
    }
    if (nextSection == "choose-thinped") {
        return '<span class="icon icon-hy-icon-left-arrow" onclick="getBack(\'' + nextSection + '\',\'' + currentSection + '\')"></span><h3 class="text-center process-headline">Günlük İnce Ped Kullanıyor musun?</h3><span class="icon icon-hy-icon-right-arrow" onclick="getNext(\'' + currentSection + '\',\'' + nextSection + '\')"></span>';
    }
    if (nextSection == "choose-subscription") {
        return '<span class="icon icon-hy-icon-left-arrow" onclick="getBack(\'' + nextSection + '\',\'' + currentSection + '\')"></span><h3 class="text-center process-headline">Abonelik Seç</h3><span class="icon icon-hy-icon-right-arrow" onclick="getNext(\'' + currentSection + '\',\'' + nextSection + '\')"></span>';
    }
    if (nextSection == "choose-additional-product") {
        return "";
    }
    if (nextSection == "box-is-ready") {
        return "";
    }
}

function getHtmlData(nextSection, msg) {

    var resultHtml = "";

    console.log("getHtmlData : " + nextSection);

    if (nextSection == "choose-tampon") {
        resultHtml = '<div class="col-md-offset-1 col-md-10">';

        $.each(msg.BoxResponseItem, function () {
            $.each(this, function (k, v) {

                var iconCss = "icon-hy-icon-tampon";
                if (v == "Aplikatörlü Tampon")
                    iconCss = "icon-hy-icon-apli-tampon";

                resultHtml += '   <div class="process-box TamponType" id="' + v + '" data-current-section="choose-tampon" data-to-section="choose-days">' +
                    '      <div class="process-box_check"><span></span></div>' +
                    '     <div class="process-box_icon">' +
                    '        <span class="icon ' + iconCss + ' icon-rotate"></span>' +
                    '   </div>' +
                    '  <div class="process-box_wrapper">' +
                    '     <div class="process-box_content">' +
                    '        <h3>'+v+'</h3>' +
                    '       <span class="caption-small">' +
                    '          Kutunun içinde sadece ped ürünleri olur' +
                    '     </span>' +
                    '</div>' +
                    ' </div>' +
                    '</div>';
            });
        });
        resultHtml += '</div>';
        return resultHtml;
    }
    if (nextSection == "choose-days") {

        resultHtml = '<div class="col-md-offset-1 col-md-10">';

        $.each(msg.BoxResponseItem, function () {
            $.each(this, function (k, v) {

                var iconCss = "icon-hy-icon-4-day";

                if (v.indexOf("5-6")>-1)
                    iconCss = "icon-hy-icon-6-day";

                if (v.indexOf("7-8")>-1)
                    iconCss = "icon-hy-icon-8-day";


                resultHtml += '<div class="process-box ReglDay" id="' + v + '" data-current-section="choose-days" data-to-section="choose-densitys">';
                resultHtml += '<div class="process-box_check"><span></span></div>';
                resultHtml += '<div class="process-box_icon">';
                resultHtml += '<span class="icon ' + iconCss + ' icon-center"></span>';
                resultHtml += '</div>';
                resultHtml += '<div class="process-box_wrapper">';
                resultHtml += '<div class="process-box_content">';
                resultHtml += '<h3>' + v + '</h3>';
                resultHtml += '<span class="caption-small">Kutunun içinde sadece ped ürünleri olur</span>';
                resultHtml += ' </div>';
                resultHtml += ' </div>';
                resultHtml += ' </div>';
            });
        });

        resultHtml += '</div>';
        return resultHtml;
    }
    if (nextSection == "choose-densitys") {

        resultHtml = '<div class="col-md-offset-1 col-md-10">';

        $.each(msg.BoxResponseItem, function () {
            $.each(this, function (k, v) {

                var iconCss = "icon-hy-icon-rare";
                var densitySubTitle = "Normal ped";
                console.log(v);

                if (v == "Normal") {
                    iconCss = "icon-hy-icon-normal";
                    densitySubTitle = "";
                }
                if (v == "Yoğun") {
                    iconCss = "icon-hy-icon-intensive-2";
                    densitySubTitle = "";
                }

                resultHtml += ' <div class="process-box DensityType" id="'+v+'" data-current-section="choose-densitys" data-to-section="choose-brands">' +
                    ' <div class="process-box_check"><span></span></div>' +
                    ' <div class="process-box_icon">' +
                    '<span class="icon ' + iconCss + ' icon-center"></span>' +
                    '</div>' +
                    ' <div class="process-box_wrapper">' +
                    '    <div class="process-box_content">' +
                    '         <h3>'+v+'</h3>' +
                    '          <span class="caption-small">Normal Ped</span>' +
                    '       </div>' +
                    '    </div>' +
                    '</div>';
            });
        });

        resultHtml += '</div>';
        return resultHtml;   
    }
    if (nextSection == "choose-brands") {

        resultHtml = '<div class="col-md-offset-1 col-md-10">';

        $.each(msg.BoxResponseItem, function () {
            $.each(this, function (k, v) {
                v = v.replace("+", "");
                v = v.replace(/ /g, "");
                resultHtml += ' <div class="process-box BrandName" id="' + v + '" data-current-section="choose-brands" data-to-section="choose-thinped">' +
                '<div class="process-box_check"><span></span></div>' +
                '<div class="process-box_wrapper">' +
                    ' <div class="process-box_content">' +
                    '    <img src="/Themes/Hypedia/Content/img/brands/' + v + '.png" alt="' + v + '">' +
                ' </div>' +
                ' </div>'+
                '</div>';
            });
        });
        

        resultHtml += '</div>';
        return resultHtml;  
    }
    if (nextSection == "choose-thinped") {

        resultHtml = '<div class="col-md-offset-1 col-md-10">';
        $.each(msg.BoxResponseItem, function () {
            $.each(this, function (k, v) {
                resultHtml += '<div class="process-box IsThinPad" id="' + v +'" data-current-section="choose-thinped" data-to-section="choose-subscription">'+
                    '<div class="process-box_check"><span></span></div>' +
                    '<div class="process-box_wrapper">' +
                    '   <div class="process-box_content">' +
                    '        <h3>'+v+'</h3>' +
                    '    </div>' +
                    '</div>' +
                    '</div>'; 
            });
        });
        resultHtml += '</div>';
        return resultHtml;  
    }
    if (nextSection == "choose-subscription") {

        var price1 = 0, price2 = 0, price3 = 0, taksit3 = 0, taksit6 = 0;

        price1 = Math.ceil(parseFloat(msg.BoxResponseItem[0].key));
        price2 = Math.ceil(parseFloat(msg.BoxResponseItem[1].key));
        price3 = Math.ceil(parseFloat(msg.BoxResponseItem[2].key));

        taksit3 = price2 / 3;
        taksit3 = Math.ceil(taksit3);
        taksit6 = price3 / 6;
        taksit6 = Math.ceil(taksit6);


        resultHtml = '<div class="col-md-offset-1 col-md-10">';
        resultHtml += '<div class="process-box Subscription" id="' + price1 + '-1" data-current-section="choose-subscription" data-to-section="choose-additional-product">' +
            '    <div class="process-box_check"><span></span></div>' +
            '    <div class="process-box_wrapper">' +
            '        <div class="process-box_content">' +
            '            <p>HER AY</p>' +
            '            <h6>' + price1 + ' TL / AY</h6>' +
            '            <span class="d-block">&nbsp;</span>' +
            '            <span class="caption-small"><span class="bold">her ay</span> kapında.</span>' +
            '        </div>' +
            '    </div>' +
            '</div>' +
            '<div class="process-box Subscription" id="' + price2 + '-3" data-current-section="choose-subscription" data-to-section="choose-additional-product">' +
            '    <div class="process-box_check"><span></span></div>' +
            '    <div class="process-box_wrapper">' +
            '        <div class="process-box_content">' +
            '            <p>3 AYLIK</p>' +
            '            <h6>' + price2 + ' TL</h6>' +
            '            <span class="d-block">(' + taksit3 + ' TL / AY)</span>' +
            '            <span class="caption-small">3 ay peşin ödemeli, <span class="bold">her ay</span> kapında.</span>' +
            '        </div>' +
            '    </div>' +
            '</div>' +
            '<div class="process-box Subscription" id="' + price3 + '-6" data-current-section="choose-subscription" data-to-section="choose-additional-product">' +
            '    <div class="process-box_check"><span></span></div>' +
            '    <div class="process-box_wrapper">' +
            '        <div class="process-box_content">' +
            '            <p>6 AYLIK</p>' +
            '            <h6>' + price3 + ' TL</h6>' +
            '            <span class="d-block">(' + taksit6 + ' TL / AY)</span>' +
            '            <span class="caption-small">6 ay peşin ödemeli, <span class="bold">her ay</span> kapında.</span>' +
            '        </div>' +
            '    </div>' +
            '</div>';
        resultHtml += '</div>';
        return resultHtml;  

    }
    if (nextSection == "choose-additional-product") {
    }
    if (nextSection == "box-is-ready") {
        return msg.BoxResponseItem[0].key;
    }

}

function getNextElement(nextSection) { return "."+nextSection.replace("choose-", "") + "Row"; }

document.getElementById('addtional-btn').addEventListener('click', function() {
    TweenMax.to('.additional-btn-wrapper', 1, {
        autoAlpha: 0,
        y: 200,
        ease: Power2.easeInOut,
    });
    TweenMax.to('.choose-additional-product', 1, {
        x: '-100%',
        autoAlpha: 0,
        ease: Power2.easeInOut,
        display: 'none',
        delay: .3
    });
    TweenMax.from('.box-is-ready', 1, {
        x: '100%',
        autoAlpha: 1,
        ease: Power2.easeInOut,
        display: 'none',
        delay: .3
    })
    TweenMax.to('.box-is-ready', 1, {
        x: '0%',
        autoAlpha: 1,
        ease: Power2.easeInOut,
        display: 'block',
        delay: .3
    });
});

//  Section eventlerin tanımlandığı alan
products.forEach(product => product.addEventListener('click', function() {
    changeSection(product.id, product.dataset.currentSection, product.dataset.toSection);
}));



// Geri Butonu Fonksiyonu
function getBack(currentSection, pervSection) {
    TweenMax.from('.' + pervSection, 1, {
        x: '-100%',
        ease: Power2.easeInOut,
        autoAlpha: 0,
        display: 'none',
        delay: .3
    });
    TweenMax.to('.' + pervSection, 1, {
        autoAlpha: 1,
        x: '0%',
        ease: Power2.easeInOut,
        delay: .3,
        display: 'block',
        delay: .3
    });
    TweenMax.to('.' + currentSection, 1, {
        x: '100%',
        autoAlpha: 0,
        ease: Power2.easeInOut,
        display: 'none',
        delay: .3
    });
    return;
}


// ileri Butonu Fonksiyonu
function getNext(currentSection, nextSection) {
    TweenMax.to('.' + currentSection, 1, {
        x: '-100%',
        autoAlpha: 0,
        ease: Power2.easeInOut,
        display: 'none',
        delay: .3
    });
    TweenMax.from('.' + nextSection, 1, {
        x: '100%',
        autoAlpha: 1,
        ease: Power2.easeInOut,
        display: 'none',
        delay: .3
    })
    TweenMax.to('.' + nextSection, 1, {
        x: '0%',
        autoAlpha: 1,
        ease: Power2.easeInOut,
        display: 'block',
        delay: .3
    });
}

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

function addProduct(productId) {

    setTimeout(function() {
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

}


const additionalProductModalContainer = document.querySelector('.additional-product-detail-container');
const addProductsModal = additionalProductModalContainer.querySelectorAll('.additional-product-detail-wrapper');

function addProductModalF(modalId) {
    addProductsModal.forEach(item => {
        if (item.id == modalId) {
            item.classList.add('open-modal');
            setTimeout(function() {
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

overlay.addEventListener('click', function() {
    setTimeout(function() {
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


/* ======= Open Mobile Menu ======= */

var menuTrigger = document.getElementById('menu-trigger'),
    body = document.getElementsByTagName('body')[0];

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


/* ======= Add Product Quantity (increment & decrement) ======= */


$(".hy-product-quantity").each(function (index) {


    console.log(index);

    var productQuantity = $(this).children('.hy-quantity');
    var productQuantityMax = $(this).children('.max');
    var productQuantityMin = $(this).children('.min');
    var quantityIncrement = $(this).children('.hy-incrament');
    var quantityDecrement = $(this).children('.hy-decrement');

    quantityDecrement.on("click", function (e) {
        var minval = productQuantity.val();
        minval *= 1;

        if (minval <= productQuantityMin) {
            productQuantity.val(productQuantityMin);
        } else {
            minval--;
            productQuantity.val(minval);
        }
    });

    quantityIncrement.on("click", function (e) {
        var minval = productQuantity.val();
        minval *= 1;
        if (minval >= productQuantityMax) {
            productQuantity.val(productQuantityMax);
        }
        else {
            minval++;
            productQuantity.val(minval);
        }
    });

    productQuantity.on("blur", function (e) {
        minval = e.target.value;
    })

}); 


//var productQuantity = document.getElementById('hy-quantity'),
//    productQuantityMax = productQuantity.getAttribute('max'),
//    productQuantityMin = productQuantity.getAttribute('min'),
//    quantityIncrement = document.getElementById('hy-incrament'),
//    quantityDecrement = document.getElementById('hy-decrement');

//var val = 1;
//quantityDecrement.addEventListener("click", function(e) {
//    if (productQuantity.value <= productQuantityMin) {
//        productQuantity.value = productQuantityMin;
//    } else {
//        val--;
//        productQuantity.value = val;
//    }
//});

//quantityIncrement.addEventListener("click", function(e) {
//    val++;
//    productQuantity.value = val;
//});
//productQuantity.addEventListener("blur", function(e) {
//    val = e.target.value;
//})