//<script src="https://js.stripe.com/v3/"></script>

//var stripe = Stripe('pk_test_51KUSwbAzFT9dgqIm48jQQT7P7GTYB9kaWFtLyyVbm0s7CXkAoXFds5OxFsamBhNZUNx9qpn2qruLdCufCqvt2LXy00OyXhEWVR');
//var elements = stripe.elements();
//var paymentElement = elements.create('payment', { /* configuration options */ });
//paymentElement.mount('#integ-payment-element');


//var form = document.getElementById('payment-form');

//form.addEventListener('submit', function (event) {
//    event.preventDefault();

//    stripe.createPaymentMethod({
//        type: 'card',
//        card: paymentElement,
//    }).then(function (result) {
//        if (result.error) {
//            // Handle error
//        } else {
//            // Process the payment method
//            var paymentMethodId = result.paymentMethod.id;
//            // Send the paymentMethodId to your server for further processing
//        }
//    });
//});


function callSubscription(event) {
    alert("Hello");
    SilkFlo.ViewModels.Subscribe.Stripe.Form_OnSubmit(event);
}

function gotoSignIn() {
    window.location.href = '/Account/SignInMicrosoft';
}

function activateUser(obj) {
    $.ajax({
        url: 'CreateMSUserAccount',
        type: 'POST',
        data: JSON.stringify(obj),  // Convert the model to JSON string
        contentType: 'application/json',
        success: function (result) {
            window.location.href = "/Account/AMActivatedLoading";
            // Handle success response if needed
        },
        error: function (error) {
            // Handle error if needed
        }
    });
}