redirectToCheckout = function (sessionId){
    var stripe = Stripe("pk_test_51L3VY7SERN3GXox2xqE1F6ALiHZpqD9j2NqZkpqKtX9UL1fAM2E1O118GnbkgRno5ZNCjyaTWSNVsGWStCI9SyMg00lojiPVYH");
    stripe.redirectToCheckout({sessionId:sessionId});
}