// Destacados.jsx
// Destacados.jsx
import React, { useEffect, useId,useState, useRef } from "react";
import "./CSS/Novedades.css";
import { initDestacados } from "./JS/Novedades.js";

export default function Destacados() {
  const [fotos, setFotos] = useState([]); // 👈 aquí se define correctamente
  const sliderId = useId();
  const rootRef = useRef(null);

  useEffect(() => {
    if (!rootRef.current) return;
    const cleanup = initDestacados(rootRef.current);
    return () => cleanup?.();
  }, []);

  useEffect(() => {
    // Llama a tu endpoint (ajusta la URL según tu backend)
    fetch("https://localhost:7044/api/MiPrimeraApi/productosSlider/ProductoMediaSlider")
      .then(res => res.json())
      .then(data => setFotos(data))
      .catch(err => console.error("Error al cargar imágenes:", err));
  }, []);

  return (
    <section className="contenedor" aria-labelledby={`${sliderId}-heading`}>
      <h1 id={`${sliderId}-heading`}>Nuestros Productos</h1>

      {/* SLIDER DESTACADOS */}
      <div id="destacados" ref={rootRef}>
        <div className="slider" id="slider">
          <div className="track" aria-live="polite" aria-atomic="true">
            {/* SLIDE 1 */}
            <div className="slide" aria-roledescription="slide" aria-label="1 de 3">
              <div className="slide__left bg-verde">
                <h2 className="slide__title">Compra y vende artículos</h2>
                <p className="slide__text">Desde la comodidad de tu casa.</p>
                <button className="cta" type="button" aria-label="Explora ahora">
                  <span>Explora ahora</span>
                  <svg width="15" height="10" viewBox="0 0 13 10" aria-hidden="true">
                    <path d="M1,5 L11,5"></path>
                    <polyline points="8 1 12 5 8 9"></polyline>
                  </svg>
                </button>
              </div>
              <div className="slide__right">
                <img
                  src={fotos[0]?.fotoBase64 ? `data:image/jpeg;base64,${fotos[0].fotoBase64}` : "/imagenes/PleaseWait.png"}
                  alt="Reloj Bulova en primer plano sobre superficie clara"
                  className="slide__img"
                  loading="lazy"
                  decoding="async"
                />
              </div>
            </div>

            {/* SLIDE 2 */}
            <div className="slide" aria-roledescription="slide" aria-label="2 de 3">
              <div className="slide__left bg-vino">
                <h2 className="slide__title">Lujo que habla por ti</h2>
                <p className="slide__text">Joyas únicas en oro y plata.</p>
                <button className="cta" type="button" aria-label="Ver colección">
                  <span>Ver colección</span>
                  <svg width="15" height="10" viewBox="0 0 13 10" aria-hidden="true">
                    <path d="M1,5 L11,5"></path>
                    <polyline points="8 1 12 5 8 9"></polyline>
                  </svg>
                </button>
              </div>
              <div className="slide__right">
                 {/* Muestra la imagenm del array del JSON o muestra una imagen temporal si aun no se carga la imagen regresada por wel API*/}
                <img
                  src={fotos[1]?.fotoBase64 ? `data:image/jpeg;base64,${fotos[1].fotoBase64}` : "/placeholder1.jpg"}
                  alt="Anillos de oro brillando sobre fondo oscuro"
                  className="slide__img"
                  loading="lazy"
                  decoding="async"
                />
              </div>
            </div>

            {/* SLIDE 3 */}
            <div className="slide" aria-roledescription="slide" aria-label="3 de 3">
              <div className="slide__left bg-azul">
                <h2 className="slide__title">Ofertas de temporada</h2>
                <p className="slide__text">Descuentos limitados.</p>
                <button className="cta" type="button" aria-label="Aprovecha">
                  <span>Aprovecha</span>
                  <svg width="15" height="10" viewBox="0 0 13 10" aria-hidden="true">
                    <path d="M1,5 L11,5"></path>
                    <polyline points="8 1 12 5 8 9"></polyline>
                  </svg>
                </button>
              </div>
              <div className="slide__right">
                <img
                  src={fotos[2]?.fotoBase64 ? `data:image/jpeg;base64,${fotos[2].fotoBase64}` : "/placeholder1.jpg"}
                  alt="Reloj con accesorios y caja de regalo"
                  className="slide__img"
                  loading="lazy"
                  decoding="async"
                />
              </div>
            </div>
          </div>

          {/* Controles */}
          <div className="nav" aria-label="Controles del carrusel">
            <button className="prev" type="button" aria-label="Anterior">❮</button>
            <button className="next" type="button" aria-label="Siguiente">❯</button>
          </div>

          {/* Dots */}
          <div className="dots" role="tablist" aria-label="Paginación del slider">
            <button className="dot active" role="tab" aria-selected="true" aria-label="Ir al slide 1" />
            <button className="dot" role="tab" aria-selected="false" aria-label="Ir al slide 2" />
            <button className="dot" role="tab" aria-selected="false" aria-label="Ir al slide 3" />
          </div>
        </div>
      </div>
    </section>
  );
}