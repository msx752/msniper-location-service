$(document).ready(function(){

	/************************
	/*	MAIN NAVIGATION
	/************************/

	$mainMenu = $('.main-menu');

	// init collapse first for browser without transition support (IE9) 
	$mainMenu.find('li').has('ul').children('ul').collapse({toggle: false});

	$mainMenu.find('li.active').has('ul').children('ul').addClass('in');
	$mainMenu.find('li').not('.active').has('ul').children('ul').removeClass('in');


	$('.main-menu .submenu-toggle').click( function(e){
		e.preventDefault();

		$currentItemToggle = $(this);
		$currentItem = $(this).parent();
		$mainMenu.find('li').not($currentItem).not($currentItem.parents('li')).removeClass('active').children('ul.in').collapse('hide');
		$currentItem.toggleClass('active').children('ul').collapse('toggle');
	});

	$('.btn-off-canvas').click( function() {
		if($('.wrapper').hasClass('off-canvas-active')) {
			$('.wrapper').removeClass('off-canvas-active');
		} else {
			$('.wrapper').addClass('off-canvas-active');
		}
	});

	$('.btn-nav-sidebar-minified').click( function(e) {
		e.preventDefault();

		

		if( $('.wrapper').hasClass('main-nav-minified') ) {
			$('.wrapper').removeClass('main-nav-minified');
			$('#main-nav').hide();
			/*$('#fixed-left-nav').removeAttr('disabled');*/
			$('#donateLink').show();
			$('#main-nav').find('span.text').show();
			setTimeout(
				function () {
					$('#main-nav').fadeIn(500);
				}, 100);
		} else {
			$('#main-nav').find('span.text').hide();
			$('.wrapper').addClass('main-nav-minified');
		    $('#donateLink').hide();
		    /*disableFixedLeft(); // fixed left sidebar is not applicable for this mode
            $('#fixed-left-nav').attr('checked', false).attr('disabled', true);*/
		}
	});

	$(window).resize(removeMinifiedOnSmallScreen);

	function removeMinifiedOnSmallScreen() {
		if( ($(document).innerWidth()) < 1200) {
			$('.wrapper').removeClass('main-nav-minified');
		}
	}


	/************************
	/*	DEMO PANEL
	/************************/

	// skin switcher
	// check if skin has already applied before
	var skin = localStorage.getItem('queenSkin');
	var skinLogo = localStorage.getItem('queenSkinLogo');
	var skinLogoDefault = 'assets/img/queenadmin-logo.png';

	if(skin != null) {
		$('head').append('<link rel="stylesheet" href="' + skin + '" type="text/css" />');
	}

	if(skinLogo != null) {
		$('.logo img').attr('src', skinLogo);
	}

	// skin button action
	$('.btn-skin').click( function(e) {

		e.preventDefault();

		resetStyle();
		$('head').append('<link rel="stylesheet" href="' + $(this).attr('data-skin') + '" type="text/css" />');

		if(!$(this).hasClass('full-white')) {
			skinLogo = 'assets/img/queenadmin-logo-white.png';
		}else {
			skinLogo = skinLogoDefault;
		}

		$('.logo img').attr('src', skinLogo);

		localStorage.setItem('queenSkin', $(this).attr('data-skin'));
		localStorage.setItem('queenSkinLogo', skinLogo);
	});

	$('#style-switcher').change( function() {
		// fixed top nav checkbox
		if( $('#fixed-top-nav').is(':checked') ) {
			$('.top-bar').addClass('navbar-fixed-top');
			$('body').addClass('fixed-top-active');
		} else {
			$('.top-bar').removeClass('navbar-fixed-top');
			$('body').removeClass('fixed-top-active');
		}

		// fixed left nav checkbox
		if( $('#fixed-left-nav').is(':checked') ) {
			$('body').addClass('fixed-left-active');

			$('.main-nav-wrapper').slimScroll({
				height: '100%'
			});

		} else {
			disableFixedLeft();
		}
	});

	$('#style-switcher li[data-toggle="popover"]').popover({
		html: true
	});

	function disableFixedLeft() {
		$('body').removeClass('fixed-left-active');

		if($('#col-left .slimScrollDiv').length > 0) {
			$(".main-nav-wrapper").parent().replaceWith($(".main-nav-wrapper"));
		}
	}

	// reset stlye
	$('.reset-style').click( function() {
		resetStyle();
	});

	function resetStyle() {
		$('head link[rel="stylesheet"]').each( function() {

			if( $(this).attr('href').toLowerCase().indexOf("skins") >= 0 )
				$(this).remove();
		});

		$('.logo img').attr('src', 'assets/img/queenadmin-logo.png');

		localStorage.removeItem('queenSkin');
		localStorage.setItem('queenSkinLogo', skinLogoDefault);

		// reset top nav
		if(!$('.top-bar').hasClass('navbar-fixed-top')) {
			$('.top-bar').addClass('navbar-fixed-top');
			$('body').addClass('fixed-top-active');
			$('#fixed-top-nav').prop('checked', 'checked');
			$('#fixed-left-nav').prop('checked', 'checked');
		}
	}


	/************************
	/*	SIDEBAR
	/************************/

	$('.toggle-right-sidebar').click( function(e) {
		$(this).toggleClass('active');
		$('.right-sidebar').toggleClass('active');
	});


	/************************
	/*	WIDGET
	/************************/

	// widget remove
	$('.widget .btn-remove').click( function(e) {

		e.preventDefault();
		$(this).parents('.widget').fadeOut(300, function() {
			$(this).remove();
		});
	});

	// widget toggle expand
	$('.widget .btn-toggle-expand').clickToggle(
		function(e) {
			e.preventDefault();
			$(this).parents('.widget').find('.slimScrollDiv').css('height', 'auto');
			$(this).parents('.widget').find('.widget-content').slideUp(300);
			$(this).find('i').removeClass('ion-ios-arrow-up').addClass('ion-ios-arrow-down');
		},
		function(e) {
			e.preventDefault();
			$(this).parents('.widget').find('.widget-content').slideDown(300);
			$(this).find('i').removeClass('ion-ios-arrow-down').addClass('ion-ios-arrow-up');
		}
	);


	/************************
	/*	BOOTSTRAP TOOLTIP
	/************************/

	$('body').tooltip({
		selector: "[data-toggle=tooltip]",
		container: "body"
	});


	/************************
	/*	BOOTSTRAP POPOVER
	/************************/

	$('.demo-popover1 #popover-title').popover({
		html: true,
		title: '<i class="icon ion-ios-chatbubble"></i> Popover Title',
		content: 'This popover has title and support HTML content. Quickly implement process-centric networks rather than compelling potentialities. Objectively reinvent competitive technologies after high standards in process improvements. Phosfluorescently cultivate 24/365.'
	});

	$('.demo-popover1 #popover-hover').popover({
		html: true,
		title: '<i class="icon ion-ios-chatbubble"></i> Popover Title',
		trigger: 'hover',
		content: 'Activate the popover on hover. Objectively enable optimal opportunities without market positioning expertise. Assertively optimize multidisciplinary benefits rather than holistic experiences. Credibly underwhelm real-time paradigms with.'
	});

	$('.demo-popover2 .btn').popover();


	/************************
	/*	TODO LIST
	/************************/

	if( $('.todo-list').length > 0 ) {
		$('.todo-list').sortable({
			revert: true,
			placeholder: "ui-state-highlight",
			handle: '.handle'
		});

		$('.todo-list input').change( function() {
			if( $(this).prop('checked') ) {
				$(this).parents('li').addClass('completed');
			}else {
				$(this).parents('li').removeClass('completed');
			}
		});
	}


	//*******************************************
	/*	DROPZONE FILE UPLOAD
	/********************************************/

	// if dropzone exist
	if( $('.dropzone').length > 0 ) {
		Dropzone.autoDiscover = false;
		
		$(".dropzone").dropzone({
			url: "php/dropzone-upload.php",
			addRemoveLinks : true,
			maxFilesize: 0.5,
			maxFiles: 5,
			acceptedFiles: 'image/*, application/pdf, .txt',
			dictResponseError: 'File Upload Error.'
		});
	} // end if dropzone exist


	//*******************************************
	/*	WIDGET SLIM SCROLL
	/********************************************/

	if( $('body.dashboard').length > 0) {
		$('.widget-todo .widget-content').slimScroll({
			height: '400px',
			wheelStep: 5,
		});

		$('.widget-live-feed .widget-content').slimScroll({
			height: '409px',
			wheelStep: 5,
		});
	}

	$('.widget-chat-contacts .widget-content').slimScroll({
		height: '800px',
		wheelStep: 5,
		railVisible: true,
		railColor: '#fff'
	});
	

	//*******************************************
	/*	CHAT STATUS
	/********************************************/

	$('.chat-status a').click( function(e) {
		e.preventDefault();

		$btnToggle = $(this).parents('ul').siblings('.dropdown-toggle');
		
		$btnToggle
		.html($(this).text() + ' <span class="caret"></span>')
		.removeClass($btnToggle.attr('data-btnclass'))
		.addClass($(this).attr('data-btnclass'))
		.attr('data-btnclass', $(this).attr('data-btnclass'));

	});


	//*******************************************
	/*	SELECT2
	/********************************************/

	if( $('.select2').length > 0) {
		$('.select2').select2();
	}

	if( $('.select2-multiple').length > 0) {
		$('.select2-multiple').select2();
	}


	//*******************************************
	/*	WIDGET SCRIPTS
	/********************************************/

	$('.widget-single-multiselect').multiselect({
		buttonClass: 'btn btn-success btn-xs',
		templates: {
			li: '<li><a href="javascript:void(0);"><label><i></i></label></a></li>'
		}
	});

	$('.btn-help').popover({
		container: 'body',
		placement: 'top',
		html: true,
		title: '<i class="icon ion-help-circled"></i> Help',
		content: "Help summary goes here. Options can be passed via data attributes <code>data-</code> or JavaScript. Please check <a href='http://getbootstrap.com/javascript/#popovers'>Bootstrap Doc</a>"
	});
	
});

	// toggle function
	$.fn.clickToggle = function( f1, f2 ) {
		return this.each( function() {
			var clicked = false;
			$(this).bind('click', function() {
				if(clicked) {
					clicked = false;
					return f2.apply(this, arguments);
				}

				clicked = true;
				return f1.apply(this, arguments);
			});
		});

	}