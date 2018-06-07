$( document ).ready(function( $ ) {
		// init slider pro
		$( '#slider' ).sliderPro({
			width: 1600,
			height: 400,
			fade: true,
			arrows: true,
			buttons: false,
			fullScreen: true,
			shuffle: false,
			smallSize: 500,
			mediumSize: 1000,
			largeSize: 3000,
			thumbnailArrows: true,
			autoplay: true,
			breakpoints: {
				800: {
					thumbnailsPosition: 'bottom',
					thumbnailWidth: 270,
					thumbnailHeight: 100
				},
				500: {
					thumbnailsPosition: 'bottom',
					thumbnailWidth: 120,
					thumbnailHeight: 50
				}
			}
		});

		// inti datepicker
		$(".dob").datetimepicker({format: 'DD/MM/YYYY'});
		// $(".to").datetimepicker({format: 'DD/MM/YYYY'});

		// init preetyphoto
		    // $("a[rel^='prettyPhoto']").prettyPhoto({
		    // 	social_tools: false
		    // });
	    // init slick
		    // $(".regular").slick({
      //           autoplay: true,
      //           autoplaySpeed: 2000,
      //           dots: false,
      //           infinite: true,
      //           slidesToShow: 7,
      //           slidesToScroll: 7
      //       });
	});