// ============================================================
// ELDEN RING MVC — site.js
// ============================================================

// ---- 1. AUDIO PLAYER (MP3 local) ----
var audio = new Audio('/audio/ambient.mp3');
audio.loop = true;
audio.volume = 0.25;

document.addEventListener('DOMContentLoaded', function () {

    // ---- 2. AUDIO PLAY/PAUSE BUTTON ----
    var playBtn = document.getElementById('audioPlayBtn');
    var icon    = document.getElementById('audioPlayIcon');
    var volSlider = document.getElementById('volSlider');

    if (playBtn) {
        playBtn.addEventListener('click', function () {
            if (audio.paused) {
                audio.play().then(function () {
                    icon.textContent = '⏸';
                    playBtn.setAttribute('aria-label', 'Pausar música de fons');
                }).catch(function (e) {
                    console.warn('Audio error:', e);
                });
            } else {
                audio.pause();
                icon.textContent = '▶';
                playBtn.setAttribute('aria-label', 'Reproduir música de fons');
            }
        });
    }

    // ---- 3. VOLUME SLIDER ----
    if (volSlider) {
        volSlider.addEventListener('input', function () {
            audio.volume = this.value / 100;
        });
    }

    // ---- 4. SCROLL FADE-IN ANIMATIONS ----
    var fadeEls = document.querySelectorAll('.fade-in');
    var observer = new IntersectionObserver(function (entries) {
        entries.forEach(function (e) {
            if (e.isIntersecting) {
                e.target.classList.add('visible');
                observer.unobserve(e.target);
            }
        });
    }, { threshold: 0.1 });
    fadeEls.forEach(function (el) { observer.observe(el); });

    // ---- 5. DIFFICULTY / RANKING BARS ----
    document.querySelectorAll('.difficulty-fill[data-width]').forEach(function (bar) {
        setTimeout(function () { bar.style.width = bar.dataset.width + '%'; }, 300);
    });
    document.querySelectorAll('.rank-bar[data-width]').forEach(function (bar) {
        setTimeout(function () { bar.style.width = bar.dataset.width + '%'; }, 400);
    });

    // ---- 6. ACTIVE NAV LINK ----
    var path = window.location.pathname.toLowerCase();
    document.querySelectorAll('.nav-link').forEach(function (link) {
        var href = (link.getAttribute('href') || '').toLowerCase();
        if (href !== '/' && path.startsWith(href)) {
            link.classList.add('active');
        } else if (href === '/' && path === '/') {
            link.classList.add('active');
        }
    });

});
