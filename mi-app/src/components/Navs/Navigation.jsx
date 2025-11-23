import React from "react";
import { NavLink } from "react-router-dom";
import "./navigation.css"; // Importa los estilos

export default function Navigation() {
  return (
     <nav className="navegacion" aria-label="Principal">
      <NavLink to="/Novedades" end className={({ isActive }) =>
        `navegacion__enlace ${isActive ? "navegacion__enlace--activo" : ""}`
      }>
        Novedades
      </NavLink>

      <NavLink to="/vender" className={({ isActive }) =>
        `navegacion__enlace ${isActive ? "navegacion__enlace--activo" : ""}`
      }>
        Vender
      </NavLink>

      <NavLink to="/vendedores" className={({ isActive }) =>
        `navegacion__enlace ${isActive ? "navegacion__enlace--activo" : ""}`
      }>
        Vendedores
      </NavLink>
    </nav>
  );
}