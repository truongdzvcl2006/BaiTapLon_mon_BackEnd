$(document).ready(function () {
    // Hiệu ứng scroll navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('.navbar').addClass('scrolled');

            // Hiệu ứng logo nhỏ lại
            $('.navbar-brand').css({
                'transform': 'scale(0.9)',
                'opacity': '0.9'
            });
        } else {
            $('.navbar').removeClass('scrolled');

            // Trả lại kích thước ban đầu
            $('.navbar-brand').css({
                'transform': 'scale(1)',
                'opacity': '1'
            });
        }
    });

    // Hiệu ứng hover nav-link
    $('.nav-link').hover(
        function () {
            $(this).css('color', '#4e73df');
        },
        function () {
            if (!$(this).parent().hasClass('active')) {
                $(this).css('color', 'rgba(255, 255, 255, 0.75)');
            }
        }
    );

    // Hiệu ứng click nav-item
    $('.nav-item').click(function () {
        $('.nav-item').removeClass('active');
        $(this).addClass('active');
    });

    // Hiệu ứng toggle mobile menu
    $('.navbar-toggler').click(function () {
        $(this).toggleClass('collapsed');
        $('.navbar-toggler-icon').toggleClass('active');

        // Thêm hiệu ứng xoay cho icon
        if ($(this).hasClass('collapsed')) {
            $('.navbar-toggler-icon').css('transform', 'rotate(0deg)');
        } else {
            $('.navbar-toggler-icon').css('transform', 'rotate(90deg)');
        }
    });

    // Smooth scroll cho các link
    $('a[href*="#"]').on('click', function (e) {
        e.preventDefault();

        $('html, body').animate(
            {
                scrollTop: $($(this).attr('href')).offset().top - 70,
            },
            800,
            'easeInOutExpo'
        );
    });
});

$(document).ready(function () {
    const sliderTrack = $('#sliderTrack');
    const slides = $('.shareholder-slide');
    const slideWidth = slides.outerWidth(true);
    const slideCount = slides.length;
    let currentPosition = 0;
    let autoSlideInterval;
    const slideDuration = 2000; // 2 giây

    // Tạo pagination dots
    const paginationDots = $('#paginationDots');
    for (let i = 0; i < slideCount; i++) {
        paginationDots.append('<div class="dot" data-index="' + i + '"></div>');
    }
    $('.dot').first().addClass('active');

    // Cập nhật vị trí slider
    function updateSliderPosition() {
        sliderTrack.css('transform', 'translateX(' + (-currentPosition * slideWidth) + 'px)');

        // Cập nhật active dot
        $('.dot').removeClass('active');
        const activeIndex = Math.abs(currentPosition / slideWidth) % slideCount;
        $('.dot').eq(activeIndex).addClass('active');
    }

    // Tự động chuyển slide
    function startAutoSlide() {
        autoSlideInterval = setInterval(function () {
            currentPosition += slideWidth;
            if (currentPosition >= slideWidth * (slideCount - 3)) {
                currentPosition = 0;
            }
            updateSliderPosition();
        }, slideDuration);
    }

    // Dừng tự động chuyển slide
    function stopAutoSlide() {
        clearInterval(autoSlideInterval);
    }

    // Bắt đầu tự động chuyển slide
    startAutoSlide();

    // Sự kiện khi hover vào slider
    $('.slider-container').hover(
        function () {
            stopAutoSlide();
        },
        function () {
            startAutoSlide();
        }
    );

    // Nút điều khiển
    $('#nextBtn').click(function () {
        stopAutoSlide();
        currentPosition += slideWidth;
        if (currentPosition >= slideWidth * (slideCount - 3)) {
            currentPosition = 0;
        }
        updateSliderPosition();
        startAutoSlide();
    });

    $('#prevBtn').click(function () {
        stopAutoSlide();
        currentPosition -= slideWidth;
        if (currentPosition < 0) {
            currentPosition = slideWidth * (slideCount - 3);
        }
        updateSliderPosition();
        startAutoSlide();
    });

    // Click vào dot
    $('.dot').click(function () {
        stopAutoSlide();
        const index = $(this).data('index');
        currentPosition = index * slideWidth;
        updateSliderPosition();
        startAutoSlide();
    });

    // Responsive - điều chỉnh số slide hiển thị
    function adjustSlides() {
        // Có thể thêm logic điều chỉnh ở đây nếu cần
    }

    $(window).resize(function () {
        adjustSlides();
    });
});

$(document).ready(function () {
    // Hiệu ứng khi scroll
    $(window).scroll(function () {
        $('.animate-on-scroll').each(function () {
            var elementTop = $(this).offset().top;
            var elementBottom = elementTop + $(this).outerHeight();
            var viewportTop = $(window).scrollTop();
            var viewportBottom = viewportTop + $(window).height();

            if (elementBottom > viewportTop && elementTop < viewportBottom) {
                $(this).addClass('fade-in animate-in');
            }
        });
    });

    // Kích hoạt hiệu ứng ban đầu
    $(window).scroll();
});

$(document).ready(function () {
    $('#customerForm').submit(function (e) {
        e.preventDefault();

        // Lấy dữ liệu form
        const formData = {
            name: $('#name').val(),
            phone: $('#phone').val(),
            email: $('#email').val(),
            service: $('#service').val(),
            message: $('#message').val()
        };

        // Validate
        if (!formData.name || !formData.phone || !formData.service) {
            alert('Vui lòng điền đầy đủ các trường bắt buộc');
            return;
        }

        // Gửi dữ liệu (có thể thay bằng AJAX)
        console.log('Form submitted:', formData);
        alert('Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất.');
        $('#customerForm')[0].reset();

        // Có thể thêm AJAX request ở đây để gửi dữ liệu về server
        /*
        $.ajax({
            url: '/submit-contact',
            method: 'POST',
            data: formData,
            success: function(response) {
                alert('Gửi thông tin thành công!');
                $('#customerForm')[0].reset();
            },
            error: function() {
                alert('Có lỗi xảy ra, vui lòng thử lại sau!');
            }
        });
        */
    });
});


// Animation khi scroll đến footer
$(window).scroll(function () {
    $('.animate-on-scroll').each(function () {
        var elementTop = $(this).offset().top;
        var elementBottom = elementTop + $(this).outerHeight();
        var viewportTop = $(window).scrollTop();
        var viewportBottom = viewportTop + $(window).height();

        if (elementBottom > viewportTop && elementTop < viewportBottom) {
            $(this).addClass('fade-in');
        }
    });
});

// Kích hoạt hiệu ứng khi load trang
$(window).scroll();