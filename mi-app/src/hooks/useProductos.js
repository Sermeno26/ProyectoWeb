import { useEffect, useState } from "react";
import { toDataUrlFromBase64 } from "../utils/img.js";

export function useProductos() {
  const [productos, setProductos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const ctrl = new AbortController();  

    (async () => {
      try {
        const resp = await fetch("https://localhost:7044/api/MiPrimeraApi/novedades", {
          signal: ctrl.signal,
        });
        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);

        const data = await resp.json(); 

        const adaptados = (Array.isArray(data) ? data : []).map(p => ({
          id: String(p.idProducto),                               
          to: `/producto/${encodeURIComponent(p.idProducto)}`,
          nombre: p.tituloProducto,
          precio: Number(p.precio ?? 0),
          imgSrc: toDataUrlFromBase64(p.fotoBase64),              
        }));

        setProductos(adaptados);

        
        if (adaptados[0]?.imgSrc) {
          console.log("imgSrc preview:", adaptados[0].imgSrc.slice(0, 40));
        }
      } catch (e) {
        if (e.name !== "AbortError") setError(e.message || String(e));
      } finally {
        setLoading(false);
      }
    })();

    return () => ctrl.abort();
  }, []);

  return { productos, loading, error };
}