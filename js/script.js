(function(){
  // Raíz del componente (aisla selecciones al contenedor)
  const root   = document.getElementById('destacados');
  if(!root) return; // por si el id no está en esta página

  const slider = root.querySelector('.slider');
  const track  = root.querySelector('.track');
  const slides = root.querySelectorAll('.slide');
  const prev   = root.querySelector('.prev');
  const next   = root.querySelector('.next');
  const dots   = root.querySelector('.dots');

  if(!slider || !track || slides.length === 0) return;

  let index = 0;
  let timer = null;
  const AUTOPLAY_MS = 5000; // cambia la velocidad si quieres

  // Crear puntos (paginación)
  slides.forEach((_,i)=>{
    const b = document.createElement('button');
    if(i===0) b.classList.add('active');
    b.addEventListener('click', ()=>goTo(i,true));
    dots.appendChild(b);
  });

  function update(){
    track.style.transform = `translateX(${index * -100}%)`;
    dots.querySelectorAll('button').forEach((d,i)=>{
      d.classList.toggle('active', i===index);
    });
  }

  function goTo(i, fromUser=false){
    index = (i + slides.length) % slides.length;
    update();
    if(fromUser) restart();
  }

  function nextSlide(){ goTo(index + 1); }
  function prevSlide(){ goTo(index - 1); }

  // Controles
  if(prev) prev.addEventListener('click', ()=>goTo(index-1, true));
  if(next) next.addEventListener('click', ()=>goTo(index+1, true));

  // Autoplay
  function start(){ stop(); timer = setInterval(nextSlide, AUTOPLAY_MS); }
  function stop(){ if(timer){ clearInterval(timer); timer = null; } }
  function restart(){ stop(); start(); }

  // Pausa al pasar el mouse solo dentro del slider aislado
  slider.addEventListener('mouseenter', stop);
  slider.addEventListener('mouseleave', start);

  // Inicializar
  update();
  start();
})();

document.addEventListener('DOMContentLoaded', () => {
  // Soporta .numeric-input (btn-minus/btn-plus) y .numi (numi__btn)
  const selectors = ['.numeric-input', '.numi'];

  const clamp = (val, min, max) => Math.max(min, Math.min(max, val));

  document.querySelectorAll(selectors.join(',')).forEach(container => {
    // Busca el input number dentro
    const input = container.querySelector('input[type="number"]');
    if (!input) return;

    // Delegación: maneja clicks en los botones del contenedor
    container.addEventListener('click', (e) => {
      const btn = e.target.closest('.btn-minus, .btn-plus, .numi__btn');
      if (!btn) return;
      e.preventDefault();

      const step = Number(input.step) || 1;
      const min  = input.min !== '' ? Number(input.min) : -Infinity;
      const max  = input.max !== '' ? Number(input.max) :  Infinity;
      const val  = Number(input.value) || 0;

      // Detecta si es + o - (por clase o por texto del botón)
      let delta = 0;
      if (btn.classList.contains('btn-plus')) delta = +step;
      else if (btn.classList.contains('btn-minus')) delta = -step;
      else {
        // Para .numi__btn usa el contenido (+ / −). Quita espacios.
        const t = (btn.textContent || '').trim();
        delta = t === '+' ? +step : -step;
      }

      const next = clamp(val + delta, min, max);
      if (next !== val) {
        input.value = next;
        input.dispatchEvent(new Event('input', { bubbles: true }));
        input.dispatchEvent(new Event('change', { bubbles: true }));
      }
    });

    // Evita cambiar con la rueda del mouse cuando está enfocado (opcional)
    input.addEventListener('wheel', (ev) => {
      if (document.activeElement === input) ev.preventDefault();
    }, { passive: false });

    // Sanitiza al perder foco
    input.addEventListener('blur', () => {
      const min  = input.min !== '' ? Number(input.min) : -Infinity;
      const max  = input.max !== '' ? Number(input.max) :  Infinity;
      const val  = Number(input.value);
      input.value = Number.isFinite(val) ? clamp(val, min, max) : (min !== -Infinity ? min : 0);
    });
  });
});