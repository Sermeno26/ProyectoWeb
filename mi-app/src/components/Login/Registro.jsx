import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";

export default function Registro() {
  const navigate = useNavigate();

  // Estado del formulario y mensaje
  const [form, setForm] = useState({
    nombre: "",
    usuario: "",
    correo: "",
    phone: "",
    password: "",
  });

  const [mensaje, setMensaje] = useState("");

  // Manejo del cambio en inputs
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  // Enviar datos al endpoint de ASP.NET
  const handleSubmit = async (e) => {
    e.preventDefault();

    // Construir el objeto con las propiedades que espera la API
    const data = {
      idUsuario: form.usuario,            // username único
      nombreUsuario: form.nombre,         // nombre completo
      emailUsuario: form.correo,
      telefonoUsuario: form.phone,
      password: form.password,
    };

    try {
      const res = await fetch("https://localhost:7044/api/Usuarios/registro", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });

      if (!res.ok) {
        const error = await res.json().catch(() => ({}));
        setMensaje(error.message || "⚠️ Error al registrar usuario");
        return;
      }

      // Si el registro fue exitoso
      const result = await res.json();
      setMensaje(`✅ Usuario ${result.idUsuario} registrado correctamente.`);

      // Limpiar campos
      setForm({
        nombre: "",
        usuario: "",
        correo: "",
        phone: "",
        password: "",
      });

      // Redirigir al login después de unos segundos
      setTimeout(() => navigate("/login"), 1500);
    } catch (err) {
      console.error(err);
      setMensaje("❌ Error al conectar con el servidor.");
    }
  };

  return (
    <div className="login-container">
      <div className="login-box">
        <h2 className="login-title">Crear cuenta</h2>

        <form className="login-form" onSubmit={handleSubmit}>
          <div className="input-group">
            <input
              type="text"
              name="nombre"
              placeholder="Nombre completo"
              autoComplete="name"
              value={form.nombre}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <input
              type="text"
              name="usuario"
              placeholder="Usuario (único)"
              autoComplete="username"
              value={form.usuario}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <input
              type="email"
              name="correo"
              placeholder="Correo electrónico"
              autoComplete="email"
              value={form.correo}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <input
              type="text"
              name="phone"
              placeholder="Número Telefónico"
              value={form.phone}
              onChange={handleChange}
              required
            />
          </div>

          <div className="input-group">
            <input
              type="password"
              name="password"
              placeholder="Contraseña"
              autoComplete="new-password"
              minLength={6}
              value={form.password}
              onChange={handleChange}
              required
            />
          </div>

          {/* Botón principal reutilizando tu estilo .btn-53 */}
          <button type="submit" className="btn-53">
            <div className="original">Registrarme</div>
            <div className="letters">
              <span>R</span>
              <span>E</span>
              <span>G</span>
              <span>I</span>
              <span>S</span>
              <span>T</span>
              <span>R</span>
              <span>A</span>
              <span>R</span>
              <span>M</span>
              <span>E</span>
            </div>
          </button>
        </form>

        {/* Mensaje de resultado */}
        {mensaje && (
          <p
            style={{
              marginTop: "1rem",
              textAlign: "center",
              color: mensaje.startsWith("✅") ? "green" : "red",
            }}
          >
            {mensaje}
          </p>
        )}

        <div className="register-section" style={{ marginTop: "1.2rem" }}>
          <p>¿Ya tienes cuenta?</p>
          <button
            type="button"
            className="btn-register"
            onClick={() => navigate("/login")}
          >
            Iniciar sesión
          </button>
        </div>
      </div>
    </div>
  );
}