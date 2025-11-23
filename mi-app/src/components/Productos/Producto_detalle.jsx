// src/components/Productos/Producto_detalle.jsx
import { useLocation, useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import "./CSS/productDetail.css";

export default function ProductoDetalle() {
  const { id } = useParams();
  const navigate = useNavigate();
  const location = useLocation();

  // Producto que puede venir desde:
  // - popup de búsqueda (fotoBase64 / foto)
  // - grid (imgSrc)
  const productoInicial = location.state?.producto || null;

  const [producto, setProducto] = useState(productoInicial);
  const [descripcion, setDescripcion] = useState("");
  const [cantidad, setCantidad] = useState(1);
  const [loading, setLoading] = useState(true);
  const [adding, setAdding] = useState(false);

  // Datos 
  useEffect(() => {
    const fetchDetalle = async () => {
      try {
        const res = await fetch(`https://localhost:7044/api/Productos/${id}`);

        if (!res.ok) {
          console.warn("Error al obtener detalle:", res.status);
          setLoading(false);
          return;
        }

        const data = await res.json();

        if (data.descripcion) {
          setDescripcion(data.descripcion);
        }

        // llamda del objeto si se cargo del serach o del grid
        if (!productoInicial) {
          setProducto({
            idProducto: data.idProducto,
            idProductoVariante: data.idProductoVariante, // si existe
            tituloProducto: data.tituloProducto,
            precio: data.precio,
            fotoBase64: data.fotoBase64,
            foto: data.foto,
          });
        }
      } catch (err) {
        console.error("Error obteniendo detalle:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchDetalle();
  }, [id, productoInicial]);

  const handleBack = () => navigate(-1);

  const incrementar = () => setCantidad((c) => c + 1);
  const decrementar = () => setCantidad((c) => (c > 1 ? c - 1 : 1));

  // ===== PRECIOS =====
  const unitPrice = Number(producto?.precio ?? 0);
  const totalPrice = unitPrice * cantidad;

  // imagen con las varianbtes soportadas
  const imageSrc =
    producto?.fotoBase64 ||
    producto?.foto ||
    producto?.imgSrc || // viene del grid
    "";

  // agregacion al carrito
  const handleAddToCart = async () => {
    if (!producto) return;

    try {
      setAdding(true);

      // token
      const rawUser = localStorage.getItem("user");
      const rawToken = localStorage.getItem("token");

      if (!rawUser || !rawToken) {
        // Si no hay token se dirige al login
        navigate("/Login");
        return;
      }

      let parsedUser = null;
      try {
        parsedUser = JSON.parse(rawUser);
      } catch {
        parsedUser = null;
      }

      const finalUser = parsedUser?.user || parsedUser || null;

      // DTO espera IdUsuario, no IdComprador
      const idUsuario = finalUser?.idUsuario || finalUser?.idComprador;

      if (!idUsuario) {
        console.error("No se encontró idUsuario en el usuario logueado");
        navigate("/Login");
        return;
      }

      //
      const idProductoVariante =
        producto.idProductoVariante ||
        producto.idProducto ||
        producto.id;

      // 3) Precio total (unidad * cantidad)
      const precioTotal = totalPrice;

      // 4) Payload según tu DTO:
      // public string IdUsuario { get; set; }
      // public string IdProductoVariante { get; set; }
      // public int Cantidad { get; set; }
      // public double Precio { get; set; }
      // public string Moneda { get; set; }
      const payload = {
        idUsuario,          
        idProductoVariante, 
        cantidad,           
        precio: precioTotal, 
        moneda: "USD",      
      };

      console.log("Payload agregar-articulo:", payload);

      const res = await fetch(
        "https://localhost:7044/api/Carrito/agregar-articulo",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${rawToken}`,
          },
          body: JSON.stringify(payload),
        }
      );

      if (!res.ok) {
        console.error("Error al agregar al carrito:", res.status);
        const errorText = await res.text().catch(() => "");
        console.error("Detalle error:", errorText);
        alert("No se pudo agregar al carrito.");
        return;
      }



      alert("Producto agregado al carrito ✅");
    } catch (err) {
      console.error("Error en handleAddToCart:", err);
      alert("Error inesperado al agregar al carrito.");
    } finally {
      setAdding(false);
    }
  };

  // ===== ESTADOS DE CARGA / ERROR =====
  if (!producto && loading) {
    return (
      <main className="product-page">
        <div className="pd-back-wrap">
          <button
            type="button"
            className="pd-back-btn"
            onClick={handleBack}
          >
            REGRESAR A TIENDA
          </button>
        </div>
        <p className="pd-status">Cargando producto…</p>
      </main>
    );
  }

  if (!producto && !loading) {
    return (
      <main className="product-page">
        <div className="pd-back-wrap">
          <button
            type="button"
            className="pd-back-btn"
            onClick={handleBack}
          >
            REGRESAR A TIENDA
          </button>
        </div>
        <p className="pd-status">No se encontró el producto.</p>
      </main>
    );
  }

  //vista principal
  return (
    <main className="product-page">
      {/* Barra superior */}
      <div className="pd-back-wrap">
        <button
          type="button"
          className="pd-back-btn"
          onClick={handleBack}
        >
          REGRESAR A TIENDA
        </button>
      </div>

      {/* Layout principal */}
      <section className="pd-layout">
        {/* Imagen */}
        <div className="pd-image-card">
          {imageSrc && (
            <img
              src={imageSrc}
              alt={producto.tituloProducto || producto.nombre}
              className="pd-image"
            />
          )}
        </div>

        {/* Info */}
        <div className="pd-info">
          <h1 className="pd-title">
            {producto.tituloProducto || producto.nombre}
          </h1>

          {/* Precio unitario */}
          <div className="pd-price-row">
            <span className="pd-price-label">Precio unidad</span>
            <span className="pd-price-value">
              ${unitPrice.toFixed(2)}
            </span>
          </div>

          {/* Cantidad + Total */}
          <div className="pd-summary-card">
            <div className="pd-qty-block">
              <span className="pd-block-title">Cantidad</span>
              <div className="pd-qty-control">
                <button
                  type="button"
                  className="pd-qty-btn"
                  onClick={decrementar}
                  disabled={adding}
                >
                  −
                </button>
                <span className="pd-qty-value">{cantidad}</span>
                <button
                  type="button"
                  className="pd-qty-btn"
                  onClick={incrementar}
                  disabled={adding}
                >
                  +
                </button>
              </div>
            </div>

            <div className="pd-total-block">
              <span className="pd-block-title">Precio total</span>
              <div className="pd-total-row">
                <span className="pd-total-label">
                  {cantidad} x ${unitPrice.toFixed(2)}
                </span>
                <span className="pd-total-value">
                  ${totalPrice.toFixed(2)}
                </span>
              </div>
            </div>
          </div>

          {/* Descripción */}
          <div className="pd-description">
            <h2 className="pd-subtitle">DESCRIPCIÓN</h2>
            <p className="pd-description-text">
              {descripcion ||
                "Descripción no disponible por el momento."}
            </p>
          </div>

          {/* Botón agregar al carrito */}
          <button
            type="button"
            className="pd-add-btn"
            onClick={handleAddToCart}
            disabled={adding}
          >
            {adding ? "Agregando..." : "Agregar al carrito"}
          </button>
        </div>
      </section>
    </main>
  );
}
