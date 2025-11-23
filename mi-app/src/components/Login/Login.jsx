import React, { useState } from "react";
import "./Login.css";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const navigate = useNavigate();

  // Estado del formulario y UI
  const [idUsuario, setIdUsuario] = useState("");
  const [password, setPassword] = useState("");
  const [mensaje, setMensaje] = useState("");
  const [loading, setLoading] = useState(false);

  // Ir a registro (ruta absoluta)
// Ir a registro 
  const irAVender = () => { navigate("Registro"); };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMensaje("");
    setLoading(true);

    try {
      // 👉 Usa tu endpoint de login que devuelve token
      const res = await fetch("https://localhost:7044/api/usuarios/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ idUsuario: idUsuario.trim(), password }),
      });

      if (!res.ok) {
        const error = await res.json().catch(() => ({}));
        setMensaje(error.message || "Credenciales inválidas");
        return;
      }

      const data = await res.json(); // { token, expiresAtUtc, user }

      // ✅ Guardar sesión para uso posterior
      localStorage.setItem("token", data.token);
      localStorage.setItem("expiresAtUtc", data.expiresAtUtc);
      localStorage.setItem("user", JSON.stringify(data.user));

      setMensaje("✅ Inicio de sesión correcto");

      // Redirigir a donde quieras (inicio / dashboard)
      setTimeout(() => navigate("/"), 800);
    } catch (err) {
      console.error(err);
      setMensaje("⚠️ Error al conectar con el servidor");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      <div className="login-box">
        <h2 className="login-title">Iniciar Sesión</h2>

        <form className="login-form" onSubmit={handleSubmit}>
          <div className="input-group">
            <input
              type="text"
              name="idUsuario"
              placeholder="Usuario"
              value={idUsuario}
              onChange={(e) => setIdUsuario(e.target.value)}
              required
              autoComplete="username"
            />
          </div>

          <div className="input-group">
            <input
              type="password"
              name="password"
              placeholder="Contraseña"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              autoComplete="current-password"
              minLength={6}
            />
          </div>

          <button type="submit" className="btn-53" disabled={loading}>
            <div className="original">{loading ? "Entrando..." : "Entrar"}</div>
            <div className="letters" aria-hidden="true">
              <span>E</span><span>N</span><span>T</span>
              <span>R</span><span>A</span><span>R</span>
            </div>
          </button>
        </form>

        {/* Mensaje de resultado */}
        {mensaje && (
          <p
            style={{
              marginTop: "1rem",
              textAlign: "center",
              fontSize: "17px",
              color: mensaje.startsWith("✅") ? "green" : "red",
            }}
          >
            {mensaje}
          </p>
        )}

        <div className="register-section">
          <p>¿No tienes cuenta?</p>
          <button className="btn-register" type="button" onClick={irAVender}>
            Registrarse
          </button>
        </div>
      </div>
    </div>
  );
}