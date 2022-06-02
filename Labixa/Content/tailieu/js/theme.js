// -----------------------------

//   JS INDEX
/* =================== */
/* 

    ## Animation Js
    ## Preloder
    ## Stiky menu
    ## Scrool Menu
    ## Scrool Up
    ## smart menu
    ## smoothscroll 
    ## Owl Carousel 
    ## Shuffle Js
    ## Googel Map
    ## Ajax
    ## Recaptcha

*/


(function($) {
  "use strict"; 


/*Animation js*/
AOS.init({

  offset:     120,

  delay:      0,

  easing:     'ease',

  duration:   5000,

  disable:    false, // Condition when AOS should be disabled. e.g. 'mobile'

  once:       false,

  mirror:     false, // whether elements should animate out while scrolling past them

  startEvent: 'DOMContentLoaded'

});





//**================== Preloder========================*//
$(window).on('load', function() {
  $('#preloader').fadeOut('slow', function() { $(this).remove(); });
});

//**================= End of Preloder =====================**//


//**================= Sticky =====================**//

 $(window).on('scroll', function() {
  if ($(window).scrollTop() > 50) {
      $('.header-area').addClass('nav-fixed');
      $('.scroll-to-target').addClass('open');
  } else {
      $('.header-area').removeClass('nav-fixed');
      $('.scroll-to-target').removeClass('open');
  }
});

//**===================Scroll UP ===================**//

if ($('.scroll-to-target').length) {
  $(".scroll-to-target").on('click', function () {
    var target = $(this).attr('data-target');
    // animate
    $('html, body').animate({
      scrollTop: $(target).offset().top
    }, 1000);
  });
} 

//**===================Scroll UP ===================**//

//**================= CounterUp =====================**//
if ($('.counterUp').length > 0) {   
  $('.counterUp').counterUp({
      delay: 10,
      time: 1000
  });
} 

//**================= End CounterUp =====================**//

  /*---------------------
    Sidepanel JS
    --------------------- */
    $('.sidebar-btn').on('click', function() {
      $('.side-panel').removeClass('hide');
  });
  $('.close-sp').on('click', function() {
      $('.side-panel').addClass('hide');
  });



//**================= Smart Menu =====================**//

// SmartMenus init
$(function() {
  $('#main-menu').smartmenus({
    subMenusSubOffsetX: 6,
    subMenusSubOffsetY: -8
  });
});

// SmartMenus mobile menu toggle button
$(function() {
  var $mainMenuState = $('#main-menu-state');
  if ($mainMenuState.length) {
    // animate mobile menu
    $mainMenuState.change(function(e) {
      var $menu = $('#main-menu');
      if (this.checked) {
        $menu.hide().slideDown(250, function() { $menu.css('display', ''); });
      } else {
        $menu.show().slideUp(250, function() { $menu.css('display', ''); });
      }
    });
    // hide mobile menu beforeunload
    $(window).bind('beforeunload unload', function() {
      if ($mainMenuState[0].checked) {
        $mainMenuState[0].click();
      }
    });
  }
});

//**================= End Smart Menu =====================**//


    /*---------------------
    smooth scroll
    --------------------- */
    $('.smoothscroll').on('click', function(e) {
      e.preventDefault();
      var target = this.hash;

      $('html, body').stop().animate({
          'scrollTop': $(target).offset().top - 150
      }, 3000);
  });


  /*---------------------


  /*------------search icon js Start-----------------*/

  $(document).ready(function() {
    $('#close-btn').click(function() {
      $('#search-overlay').fadeOut();
      $('#search-btn').show();
    });
    $('#search-btn').click(function() {
      $(this).hide();
      $('#search-overlay').fadeIn();
    });

    $("form").submit(function(event) {
      event.preventDefault();
      return false;
    });
  });
  
  /*------------search icon js end-----------------*/

  //*================Portfolio Shuffle ============*/
/*---------------------
    Shuffle Activation    
   --------------------- */
   $(window).on('load', function(e) {
    if ($('body').find('.my-shuffle-container').length !== 0) {
        var Shuffle = window.Shuffle;
        var jQuery = window.jQuery;
        var portFolioShuffle = new Shuffle(document.querySelector('.my-shuffle-container'), {
            itemSelector: '.single-project',
            sizer: '.Ssizer-element',
            buffer: 1,
        });
        $('input[name="shuffle-filter"]').on('change', function(evt) {
            var input = evt.currentTarget;
            if (input.checked) {
                portFolioShuffle.filter(input.value);
            }
        });
    }
});



//*================End Portfolio Shuffle ============*//




    /*================================

    Magnific Popup

    ==================================*/

   $('.expand-img').magnificPopup({

      type: 'image',

      gallery: {

          enabled: true

      }

  }); 



  $('.expand-video').magnificPopup({

      type: 'iframe',

      gallery: {

          enabled: true

      }

  });



//**================== Owl Carousl========================*//

if ($('.bestsell-slider').length > 0) {   

  $('.bestsell-slider').owlCarousel({
  
    loop:true,
    margin:30,
    center:false,
    dots:false,
    nav:true,
    autoplay:false,
    autoplayTimeout:3000,
    navText:["<i class='flaticon-left-arrow-1'></i>","<i class='flaticon-right-arrow'></i>"],
    smartSpeed :2000,
    responsive:{
        0:{
            items:1
        },
        450:{
            items:2
            },
        600:{
            items:2
            },
        1000:{
            items:4
        }
    }
  })
  }

  if ($('.testimonial-slider').length > 0) {   

    $('.testimonial-slider').owlCarousel({
    
      loop:true,
      margin:30,
      center:false,
      dots:false,
      nav:true,
      autoplay:false,
      autoplayTimeout:3000,
      navText:["<i class='flaticon-left-arrow-1'></i>","<i class='flaticon-right-arrow'></i>"],
      smartSpeed :2000,
      responsive:{
          0:{
              items:1
          },
          450:{
              items:1
              },
          600:{
              items:1
              },
          1000:{
              items:2
          }
      }
    })
    }
  
    if ($('.testimonial-slider2').length > 0) {   

      $('.testimonial-slider2').owlCarousel({
      
        loop:true,
        margin:30,
        center:false,
        dots:false,
        nav:true,
        autoplay:false,
        autoplayTimeout:3000,
        navText:["<i class='flaticon-left-arrow-1'></i>","<i class='flaticon-right-arrow'></i>"],
        smartSpeed :2000,
        responsive:{
            0:{
                items:1
            },
            450:{
                items:1
                },
            600:{
                items:1
                },
            1000:{
                items:1
            }
        }
      })
      }



      if ($('.product-d-info').length > 0) { 
        $(document).ready(function() {
          $('.product-d-info').owlCarousel({
            items: 1,
            loop: false,
            center: true,
            margin: 10,
            nav:false,
            dots:false,
            autoplay:false,
            callbacks: true,
            URLhashListener: true,
            autoplayHoverPause: true,
            startPosition: 'URLHash'
          });
        })
        }








//**================== Owl Carousl========================*//





}(jQuery));
  


//**=================== Google Map ==========================**//

if ($('#googleMap').length > 0) { 
  var user_lat, user_lng;
      var map;
  
      function initMap() {
          map = new google.maps.Map(document.getElementById('googleMap'), {
              center: {
                  lat: 23.782062,
                  lng: 90.416053
              },
              zoom: 15,
              scrollwheel: false
          });
                
        var marker = new google.maps.Marker({
          position:{lat: 23.782062, lng: 90.416053},
          map: map,
       /*    icon: "assets/img/locator.png" */
        });
  
            } 
          }
  
  //*================ End Google Map ============*//



  
/*---------------------
    // Ajax Contact Form
    --------------------- */

    $('.cf-msg').hide();
    $('form#cf button#submit').on('click', function() {
    
        var name = $('#name').val();
        var email = $('#email').val();
        var msg = $('#msg').val();2
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    
        if (!regex.test(email)) {
            alert('Please enter valid email');
            return false;
        }
    
        name = $.trim(name);
        email = $.trim(email);
        msg = $.trim(msg);
    
        if (name != '' && email != '' && msg != '') {
            var values = "name=" + name + "&email=" + email + "&msg=" + msg;
            $.ajax({
                type: "POST",
                url: "assets/mail-sender/contact.php",
                data: values,
                success: function() {
                    $('#name').val('');
                    $('#email').val('');
                    $('#msg').val('');
    
                   $('.cf-msg').fadeIn().html('<div class="alert alert-success"><strong>Success!</strong> Email has been sent successfully.</div>');
                    setTimeout(function() {
                        $('.cf-msg').fadeOut('slow');
                    }, 4000);
                }
            });
        } else {
            $('.cf-msg').fadeIn().html('<div class="alert alert-danger"><strong>Warning!</strong> Please fillup the informations correctly.</div>')   
            $('.cf-msg').fadeOut(2000)
        }
        return false;
    });


// Ajax Contact Form JS END



/*============Recaptcha==================*/

/* if ($('#recaptcha').length > 0) {   
  var onloadCallback = function() {
    grecaptcha.render('recaptcha', {
      'sitekey' : '6Le-d6sZAAAAAOOxiYmGhuAh48qXaHBiuGE-Fkwi'
    });
  };
  
  } */
  

/*============Recaptcha==================*/
 
    // Product Cart  Increment

    function increaseValue() {
      var value = parseInt(document.getElementById('number').value, 10);
      value = isNaN(value) ? 0 : value;
      value++;
      document.getElementById('number').value = value;
    }
    
    function decreaseValue() {
      var value = parseInt(document.getElementById('number').value, 10);
      value = isNaN(value) ? 0 : value;
      value < 1 ? value = 1 : '';
      value--;
      document.getElementById('number').value = value;
    }

    // Product Cart  Increment End