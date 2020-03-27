
    var deliveryDateContainer = document.querySelector('.delivery-dates');
    var deliveryDates = deliveryDateContainer.querySelectorAll('.delivery-date-box');


    function deliveryDatesF() {
        deliveryDates.forEach(item => {
            if (this == item) {
                item.classList.toggle('select-box');
                return;
            }
            item.classList.remove('select-box');
        })
    }


    deliveryDates.forEach(deliveryDates => deliveryDates.addEventListener('click', deliveryDatesF));
