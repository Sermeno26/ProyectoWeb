import "./App.css";
import React, { useState } from "react";
import { Routes, Route, useLocation } from "react-router-dom";

import Topbar from "./components/Header/Topbar";
import Novedades from "./components/pages/Novedades";
import Navigation from "./components/Navs/Navigation";
import PaginaNovedades from "./pages/ProductosPage";
import LoginPage from "./components/Login/Login";
import RegistroPage from "./components/Login/Registro.jsx";
import Cart from "./components/Carrito/carrito.jsx";
import ProductDetail from "./components/Productos/Producto_detalle.jsx";

function App() {
  const { pathname } = useLocation();

  // Normalizamos
  const ruta = pathname.toLowerCase();

  // Ocultar layout en login/registro/detalle producto
  const ocultarLayout =
    ruta.startsWith("/login") ||
    ruta.startsWith("/login/registro") ||
    ruta.startsWith("/producto/");      

  const [isCartOpen, setIsCartOpen] = useState(false);
  const openCart = () => setIsCartOpen(true);
  const closeCart = () => setIsCartOpen(false);

  return (
    <main className="contenedor">
      {/* Topbar + Nav + Carrito (NO aparece en login, registro o detalle de producto) */}
      {!ocultarLayout && (
        <>
          <Topbar onCartClick={openCart} />
          <Navigation />
          <Cart open={isCartOpen} onClose={closeCart} />
        </>
      )}

      <Routes>
        {/* Página de novedades */}
        <Route
          path="/Novedades"
          element={
            <>
              <Novedades />
              <PaginaNovedades />
            </>
          }
        />

        {/* Detalle de producto */}
        <Route path="/producto/:id" element={<ProductDetail />} />

        {/* Login / Registro */}
        <Route path="/Login" element={<LoginPage />} />
        <Route path="/Login/Registro" element={<RegistroPage />} />
      </Routes>
    </main>
  );
}

export default App;
