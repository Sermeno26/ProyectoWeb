// src/components/Productos/Producto_card.jsx
import React from "react";
import { useNavigate } from "react-router-dom";
import "./CSS/Producto_card.css";

export default function ProductoCard({
  to = "/producto",
  producto,          // objeto original
  imgSrc = "",
  alt = "Producto",
  nombre = "Producto",
  precio = "$0.00",
}) {
  const navigate = useNavigate();

  const handleClick = (e) => {
    e.preventDefault();

    // Mandamos el producto + la img resuelta
    navigate(to, {
      state: {
        producto: {
          ...(producto || {}),
          imgSrc, // 🔥 la imagen que ya mostramos en la card
        },
      },
    });
  };

  return (
    <div className="producto">
      <a
        href={to}
        onClick={handleClick}
        className="producto__link"
        aria-label={`Ver detalle: ${nombre}`}
      >
        <img
          className="producto__imagen"
          src={imgSrc}
          alt={alt}
          loading="lazy"
          decoding="async"
        />
        <div className="producto__informacion">
          <p className="producto__nombre">{nombre}</p>
          <p className="producto__precio">{precio}</p>
        </div>
      </a>
    </div>
  );
}
