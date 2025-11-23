// destacados.js
// - Índice circular (prev/next hacen wrap)
// - Autoplay con pausa al hover/focus/touch
// - Dots, teclado, aria y transform
// - No depende de React; se inicializa con el nodo raíz de #destacados.

export function initDestacados(root) {
  if (!root) return;

  const slider   = root.querySelector(".slider");
  const track    = root.querySelector(".track");
  const slides   = Array.from(root.querySelectorAll(".slide"));
  const btnPrev  = root.querySelector(".prev");
  const btnNext  = root.querySelector(".next");
  const dotsWrap = root.querySelector(".dots");
  const dots     = Array.from(dotsWrap?.querySelectorAll("button") || []);

  let index = 0;
  const total = slides.length;

  // ====== AUTOPLAY CONFIG ======
  const prefersReduce = window.matchMedia?.("(prefers-reduced-motion: reduce)")?.matches;
  const DEFAULT_DELAY = 5000; // 5s
  // Puedes setear en HTML: <div class="slider" data-autoplay-interval="4000">
  const AUTOPLAY_DELAY = Number(slider?.dataset?.autoplayInterval) || DEFAULT_DELAY;
  let autoplayTimer = null;
  let isPaused = false;

  function updateARIA() {
    slides.forEach((s, i) => {
      s.setAttribute("aria-label", `${i + 1} de ${total}`);
    });
  }

  function updateUI() {
    track.style.transform = `translateX(-${index * 100}%)`;

    // En carrusel circular no se deshabilitan los botones:
    if (btnPrev) btnPrev.disabled = false;
    if (btnNext) btnNext.disabled = false;

    dots.forEach((d, i) => {
      const active = i === index;
      d.classList.toggle("active", active);
      d.setAttribute("aria-selected", active ? "true" : "false");
    });
  }

  function goTo(i) {
    index = ((i % total) + total) % total; // wrap circular
    updateUI();
  }

  function prev() { goTo(index - 1); }
  function next() { goTo(index + 1); }

  function onKeyDown(e) {
    switch (e.key) {
      case "ArrowLeft":
        e.preventDefault(); prev(); restartAutoplay(); break;
      case "ArrowRight":
        e.preventDefault(); next(); restartAutoplay(); break;
      case "Home":
        e.preventDefault(); goTo(0); restartAutoplay(); break;
      case "End":
        e.preventDefault(); goTo(total - 1); restartAutoplay(); break;
      default:
        break;
    }
  }

  // ====== AUTOPLAY helpers ======
  function startAutoplay() {
    if (prefersReduce || isPaused || autoplayTimer) return;
    autoplayTimer = setInterval(() => {
      next();
    }, AUTOPLAY_DELAY);
  }

  function stopAutoplay() {
    if (autoplayTimer) {
      clearInterval(autoplayTimer);
      autoplayTimer = null;
    }
  }

  function pause() {
    isPaused = true;
    stopAutoplay();
  }

  function resume() {
    isPaused = false;
    startAutoplay();
  }

  function restartAutoplay() {
    // útil después de un clic en dot/prev/next/teclado
    stopAutoplay();
    startAutoplay();
  }

  // ====== Listeners ======
  const dotHandlers = dots.map((d, i) => {
    const handler = () => { goTo(i); restartAutoplay(); };
    d.addEventListener("click", handler);
    return { d, handler };
  });

  btnPrev?.addEventListener("click", () => { prev(); restartAutoplay(); });
  btnNext?.addEventListener("click", () => { next(); restartAutoplay(); });
  slider?.addEventListener("keydown", onKeyDown);

  // Pausa con cursor encima
  slider?.addEventListener("mouseenter", pause);
  slider?.addEventListener("mouseleave", resume);

  // Pausa cuando hay foco dentro (accesibilidad/teclado)
  slider?.addEventListener("focusin", pause);
  slider?.addEventListener("focusout", resume);

  // Pausa en interacción táctil (evita pelear con el autoplay)
  slider?.addEventListener("touchstart", pause, { passive: true });
  slider?.addEventListener("touchend", () => {
    // Pequeño delay para no reanudar en medio del gesto
    setTimeout(resume, 300);
  }, { passive: true });

  // ====== Inicial ======
  updateARIA();
  updateUI();
  startAutoplay();

  // ====== Limpieza ======
  return function cleanup() {
    stopAutoplay();
    dotHandlers.forEach(({ d, handler }) => d.removeEventListener("click", handler));
    btnPrev?.removeEventListener("click", prev);
    btnNext?.removeEventListener("click", next);
    slider?.removeEventListener("keydown", onKeyDown);

    slider?.removeEventListener("mouseenter", pause);
    slider?.removeEventListener("mouseleave", resume);
    slider?.removeEventListener("focusin", pause);
    slider?.removeEventListener("focusout", resume);
    slider?.removeEventListener("touchstart", pause);
    slider?.removeEventListener("touchend", resume);
  };
}