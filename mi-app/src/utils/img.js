// src/utils/img.js
export function toDataUrlFromBase64(raw) {
  if (!raw) return "";
  const s = String(raw).trim();

  // Si ya viene con data:image/..., úsalo tal cual
  if (s.startsWith("data:image")) return s;

  // Detecta MIME por los primeros caracteres
  // /9j/ -> jpeg | iVBOR -> png | R0lGOD -> gif | UklGR -> webp
  let mime = "image/jpeg";
  if (s.startsWith("iVBOR")) mime = "image/png";
  else if (s.startsWith("/9j/")) mime = "image/jpeg";
  else if (s.startsWith("R0lGOD")) mime = "image/gif";
  else if (s.startsWith("UklGR")) mime = "image/webp";

  return `data:${mime};base64,${s}`;
}