// wwwroot/js/project.js

document.addEventListener('DOMContentLoaded', function () {
    // Thêm hiệu ứng loading khi click vào card
    const cards = document.querySelectorAll('.project-card');

    cards.forEach(card => {
        card.addEventListener('click', function (e) {
            // Thêm hiệu ứng loading
            this.style.opacity = '0.7';
            this.style.pointerEvents = 'none';

            // Tạo hiệu ứng ripple
            const ripple = document.createElement('span');
            ripple.classList.add('ripple');
            this.appendChild(ripple);

            // Xóa ripple sau 500ms
            setTimeout(() => {
                ripple.remove();
            }, 500);
        });
    });
});

// Thêm CSS cho hiệu ứng ripple
const style = document.createElement('style');
style.textContent = `
    .project-card {
        position: relative;
        overflow: hidden;
    }
    
    .ripple {
        position: absolute;
        border-radius: 50%;
        background: rgba(255, 255, 255, 0.5);
        transform: scale(0);
        animation: ripple 0.6s linear;
        pointer-events: none;
    }
    
    @keyframes ripple {
        to {
            transform: scale(4);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);