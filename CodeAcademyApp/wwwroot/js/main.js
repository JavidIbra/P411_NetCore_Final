$(document).ready(function () {

    function handlePreloader() {
        if ($('#preloader').length) {
            $('#preloader').delay(300).fadeOut(500);
        }
    }
    handlePreloader();

  $(".owl-carousel").owlCarousel({
    loop: true,
    items: 1,
    autoplay: true,
    autoplayTimeout: 5000,
    autoplayHoverPause: true,
  });

    AOS.init();
});
