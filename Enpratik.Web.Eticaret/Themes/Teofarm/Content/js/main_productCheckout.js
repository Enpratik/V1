

    var h = window.outerHeight;
    var invoiceOpen = document.getElementById("invoiceOpen");
    invoiceOpen.addEventListener('click', function () {
        if (invoiceOpen.checked) {
            document.getElementsByClassName('invoice-section')[0].style.display = 'block';
            window.scrollTo(0, h + 700);
        }
        if (!invoiceOpen.checked) {
            document.getElementsByClassName('invoice-section')[0].style.display = 'none';
        }
    })

    var loginSection = document.getElementById('login-s'),
        registerSection = document.getElementById('register-s'),
        profileSection = document.getElementById('profile-s'),
        loginBtn = document.getElementById('sign-in-btn'),
        registerBtn = document.getElementById('sign-up-btn');


    loginBtn.addEventListener('click', function (e) {
        e.preventDefault();
        registerSection.classList.remove('d-block');
        registerSection.classList.add('d-none');
        loginSection.classList.add('d-block');
    });

    registerBtn.addEventListener('click', function (e) {
        e.preventDefault();
        loginSection.classList.remove('d-block');
        loginSection.classList.add('d-none');
        registerSection.classList.add('d-block');
    });
