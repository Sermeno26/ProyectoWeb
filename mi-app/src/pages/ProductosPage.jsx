import React from "react";
import { useProductos } from "../hooks/useProductos";
import ProductosGrid from "../components/Productos/Producto_grid";
export default function ProductosPage() {
  const { productos, loading, error } = useProductos();

  if (loading) return <p>Cargando productos...</p>;
  if (error) return <p>Error al cargar: {error}</p>;

  return <ProductosGrid productos={productos} />;
}