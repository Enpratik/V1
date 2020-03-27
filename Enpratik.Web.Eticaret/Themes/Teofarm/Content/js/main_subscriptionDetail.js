

    var cancelSubsBtn = document.getElementById('cancel-subscription-btn');
    var cancelSubsModal = document.getElementById('cancel-subscriptrion');
    var subscriptionCancelBtn = document.getElementById('subscription-cancel-btn');

    cancelSubsBtn.addEventListener('click', function (e) {
        e.preventDefault();
        body.classList.add('openCancelModal');
    })

    overlay.addEventListener('click', function () {
        body.classList.remove('openCancelModal');
    })
    subscriptionCancelBtn.addEventListener('click', function () {
        body.classList.remove('openCancelModal');
    })
