(function ($) {
	var pathname = window.location.pathname,
		pages = ['/Home/Index', '/Usuarios/Usuarios', '/Roles/Roles', '/Productos/Productos'];

	$('.sidebar-item').each(function (i) {
		if (pathname.includes(pages[i])) $(this).addClass('active');
		else if (this.className.includes('active')) $(this).removeClass('active');
	});

	"use strict";

	var fullHeight = function () {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function () {
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$(".toggle-password").click(function () {

		$(this).toggleClass("fa-eye fa-eye-slash");
		var input = $($(this).attr("toggle"));
		if (input.attr("type") == "password") {
			input.attr("type", "text");
		} else {
			input.attr("type", "password");
		}
	});

})(jQuery);
