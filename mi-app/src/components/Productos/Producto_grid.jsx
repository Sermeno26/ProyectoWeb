// src/components/Productos/Producto_grid.jsx
import PropTypes from "prop-types";
import ProductoCard from "./Producto_card";

/**
 * Normaliza la imagen: si viene como imgSrc la usa,
 * si viene como base64 en Foto, la convierte a data URL.
 */
function resolverImgSrc(p) {
  if (p.imgSrc) return p.imgSrc;
  if (p.Foto) return `data:image/jpeg;base64,${p.Foto}`;
  if (p.fotoBase64) return p.fotoBase64;
  if (p.foto) return p.foto;
  return "";
}

export default function ProductosGrid({ productos = [] }) {
  const lista = Array.isArray(productos) ? productos : [];

  if (lista.length === 0) {
    return (
      <div className="p-8 text-center text-gray-500">
        No hay productos para mostrar.
      </div>
    );
  }

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
      {lista.map((p) => {
        const id =
          p.id ?? p.IdProducto ?? p.idProducto; // tolerante a varios nombres

        return (
          <ProductoCard
            key={id}
            to={`/producto/${id}`}     // misma ruta que usa ProductDetail
            producto={p}              // 🔥 se pasa el objeto completo
            imgSrc={resolverImgSrc(p)}
            alt={p.alt ?? p.nombre ?? p.TituloProducto ?? "Producto"}
            nombre={p.nombre ?? p.TituloProducto}
            precio={Number(p.precio ?? p.Precio ?? 0).toLocaleString(
              "en-US",
              {
                style: "currency",
                currency: "USD",
              }
            )}
          />
        );
      })}
    </div>
  );
}

ProductosGrid.propTypes = {
  productos: PropTypes.arrayOf(PropTypes.object),
};
