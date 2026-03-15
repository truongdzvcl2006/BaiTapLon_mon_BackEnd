// wwwroot/js/property-detail.js

// Gallery variables
let currentImageIndex = 0;
let images = [];

// Initialize gallery images from Razor
document.addEventListener('DOMContentLoaded', function () {
    // Get all gallery images
    const mainImage = document.querySelector('.main-image img');
    const subImages = document.querySelectorAll('.sub-image img');

    images = [mainImage.src];
    subImages.forEach(img => images.push(img.src));
});

// Open modal
function openModal(index) {
    currentImageIndex = index;
    document.getElementById('imageModal').style.display = 'block';
    updateModalImage();
}

// Close modal
function closeModal() {
    document.getElementById('imageModal').style.display = 'none';
}

// Change image
function changeImage(direction) {
    currentImageIndex += direction;

    if (currentImageIndex < 0) {
        currentImageIndex = images.length - 1;
    } else if (currentImageIndex >= images.length) {
        currentImageIndex = 0;
    }

    updateModalImage();
}

// Update modal image
function updateModalImage() {
    const modalImage = document.getElementById('modalImage');
    const counter = document.getElementById('imageCounter');

    modalImage.src = images[currentImageIndex];
    counter.textContent = `${currentImageIndex + 1} / ${images.length}`;
}

// Keyboard navigation
document.addEventListener('keydown', function (e) {
    const modal = document.getElementById('imageModal');

    if (modal.style.display === 'block') {
        if (e.key === 'Escape') {
            closeModal();
        } else if (e.key === 'ArrowLeft') {
            changeImage(-1);
        } else if (e.key === 'ArrowRight') {
            changeImage(1);
        }
    }
});

// Close modal when clicking outside
window.onclick = function (event) {
    const modal = document.getElementById('imageModal');
    if (event.target === modal) {
        closeModal();
    }
}

// Open request form
function openRequestForm() {
    document.getElementById('requestModal').style.display = 'block';
    document.body.style.overflow = 'hidden';
}

// Close request form
function closeRequestForm() {
    document.getElementById('requestModal').style.display = 'none';
    document.body.style.overflow = 'auto';
}

// Submit request form
function submitRequest(event) {
    event.preventDefault();

    // Show loading
    const submitBtn = event.target.querySelector('.submit-btn');
    const originalText = submitBtn.textContent;
    submitBtn.textContent = 'Đang gửi...';
    submitBtn.disabled = true;

    // Simulate API call
    setTimeout(function () {
        alert('Yêu cầu của bạn đã được gửi thành công! Chúng tôi sẽ liên hệ trong thời gian sớm nhất.');
        closeRequestForm();
        submitBtn.textContent = originalText;
        submitBtn.disabled = false;
        event.target.reset();
    }, 1500);
}

// Close request modal when clicking outside
window.addEventListener('click', function (event) {
    const requestModal = document.getElementById('requestModal');
    if (event.target === requestModal) {
        closeRequestForm();
    }
});

// Smooth scroll for anchor links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// Add to favorites