using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiWebMarket.Models;

public partial class MercaUcaContext : DbContext
{
    public MercaUcaContext()
    {
    }

    public MercaUcaContext(DbContextOptions<MercaUcaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArticulosCarrito> ArticulosCarritos { get; set; }

    public virtual DbSet<ArticulosOrden> ArticulosOrdens { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<CondicionProducto> CondicionProductos { get; set; }

    public virtual DbSet<Direccione> Direcciones { get; set; }

    public virtual DbSet<EstadoArticulosOrden> EstadoArticulosOrdens { get; set; }

    public virtual DbSet<EstadoPayment> EstadoPayments { get; set; }

    public virtual DbSet<EstadoUsuario> EstadoUsuarios { get; set; }

    public virtual DbSet<EstadosOrden> EstadosOrdens { get; set; }

    public virtual DbSet<LineaEnvioDireccion> LineaEnvioDireccions { get; set; }

    public virtual DbSet<MetodoAbono> MetodoAbonos { get; set; }

    public virtual DbSet<Orden> Ordens { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Payout> Payouts { get; set; }

    public virtual DbSet<PayoutEstado> PayoutEstados { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoMedium> ProductoMedia { get; set; }

    public virtual DbSet<ProveedorPayment> ProveedorPayments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VariantesProducto> VariantesProductos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LEGION5PRO\\SQLEXPRESS; Database=MercaUca; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticulosCarrito>(entity =>
        {
            entity.HasKey(e => e.IdArticulosCarrito);

            entity.ToTable("Articulos_carrito");

            entity.Property(e => e.IdArticulosCarrito)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_articulos_carrito");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCarrito)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_carrito");
            entity.Property(e => e.IdProductoVariante)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto_variante");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.ArticulosCarritos)
                .HasForeignKey(d => d.IdCarrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carrito_Articulos_carrito");

            entity.HasOne(d => d.IdProductoVarianteNavigation).WithMany(p => p.ArticulosCarritos)
                .HasForeignKey(d => d.IdProductoVariante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variantes_productos_Articulos_carrito");
        });

        modelBuilder.Entity<ArticulosOrden>(entity =>
        {
            entity.HasKey(e => e.IdArticuloOrden);

            entity.ToTable("Articulos_orden");

            entity.Property(e => e.IdArticuloOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_articulo_orden");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaEntrega)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.IdEstadoArticuloOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_estado_articulo_orden");
            entity.Property(e => e.IdOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_orden");
            entity.Property(e => e.IdProductoVariante)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto_variante");
            entity.Property(e => e.IdVendedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_vendedor");
            entity.Property(e => e.PorcentajeComision).HasColumnName("porcentaje_comision");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.SubtotalComision).HasColumnName("subtotal_comision");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("tracking_number");

            entity.HasOne(d => d.IdEstadoArticuloOrdenNavigation).WithMany(p => p.ArticulosOrdens)
                .HasForeignKey(d => d.IdEstadoArticuloOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estado_articulos_orden_Articulos_orden");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.ArticulosOrdens)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Articulos_orden");

            entity.HasOne(d => d.IdProductoVarianteNavigation).WithMany(p => p.ArticulosOrdens)
                .HasForeignKey(d => d.IdProductoVariante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variantes_productos_Articulos_orden");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito);

            entity.ToTable("Carrito");

            entity.Property(e => e.IdCarrito)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_carrito");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.IdComprador)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_comprador");

            entity.HasOne(d => d.IdCompradorNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdComprador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Carrito");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.IdCategoria)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_categoria");
            entity.Property(e => e.CategoriaActiva).HasColumnName("categoria_activa");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<CondicionProducto>(entity =>
        {
            entity.HasKey(e => e.IdCondicion);

            entity.ToTable("Condicion_producto");

            entity.Property(e => e.IdCondicion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_condicion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Direccione>(entity =>
        {
            entity.HasKey(e => e.IdDireccion);

            entity.Property(e => e.IdDireccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_direccion");
            entity.Property(e => e.DireccionDefault).HasColumnName("direccion_default");
            entity.Property(e => e.IdPais)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_pais");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_usuario");
            entity.Property(e => e.NombreDireccion)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre_direccion");
            entity.Property(e => e.NombreReceptor)
                .HasMaxLength(170)
                .IsUnicode(false)
                .HasColumnName("nombre_receptor");
            entity.Property(e => e.TelefonoReceptor)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono_receptor");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pais_Direcciones");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Direcciones");
        });

        modelBuilder.Entity<EstadoArticulosOrden>(entity =>
        {
            entity.HasKey(e => e.IdEstadoArticuloOrden);

            entity.ToTable("Estado_articulos_orden");

            entity.Property(e => e.IdEstadoArticuloOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_estado_articulo_orden");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<EstadoPayment>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPayment);

            entity.ToTable("Estado_payment");

            entity.Property(e => e.IdEstadoPayment)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_estado_payment");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(85)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<EstadoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("Estado_usuario");

            entity.Property(e => e.IdEstado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_estado");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<EstadosOrden>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOrden);

            entity.ToTable("Estados_orden");

            entity.Property(e => e.IdEstadoOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_estado_orden");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<LineaEnvioDireccion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Linea_envio_direccion");

            entity.Property(e => e.IdDireccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_direccion");
            entity.Property(e => e.LineaDireccion)
                .IsUnicode(false)
                .HasColumnName("linea_direccion");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany()
                .HasForeignKey(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direcciones_Linea_envio_direccion");
        });

        modelBuilder.Entity<MetodoAbono>(entity =>
        {
            entity.HasKey(e => e.IdMetodoAbono);

            entity.ToTable("Metodo_abono");

            entity.Property(e => e.IdMetodoAbono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_metodo_abono");
            entity.Property(e => e.NombreMetodo)
                .HasMaxLength(85)
                .IsUnicode(false)
                .HasColumnName("nombre_metodo");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.IdOrden);

            entity.ToTable("Orden");

            entity.Property(e => e.IdOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_orden");
            entity.Property(e => e.FechaCierre).HasColumnName("fecha_cierre");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdComprador)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_comprador");
            entity.Property(e => e.IdDireccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_direccion");
            entity.Property(e => e.IdEstadoOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_estado_orden");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.MontoTotal).HasColumnName("monto_total");

            entity.HasOne(d => d.IdCompradorNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.IdComprador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Orden");

            entity.HasOne(d => d.IdEstadoOrdenNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.IdEstadoOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estados_orden_Orden");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais);

            entity.Property(e => e.IdPais)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_pais");
            entity.Property(e => e.NombrePais)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombre_pais");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("PAYMENTS");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("payment_id");
            entity.Property(e => e.FechaAutorizacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fecha_autorizacion");
            entity.Property(e => e.FechaRealizacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fecha_realizacion");
            entity.Property(e => e.IdEstadoPayment)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_estado_payment");
            entity.Property(e => e.IdOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_orden");
            entity.Property(e => e.IdProveedorPago)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_proveedor_pago");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("moneda");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.RefrenciaTransaccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("refrencia_transaccion");

            entity.HasOne(d => d.IdEstadoPaymentNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdEstadoPayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estado_payment_PAYMENTS");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_PAYMENTS");

            entity.HasOne(d => d.IdProveedorPagoNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdProveedorPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor_payments_PAYMENTS");
        });

        modelBuilder.Entity<Payout>(entity =>
        {
            entity.Property(e => e.PayoutId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("payout_id");
            entity.Property(e => e.FechaRealizacion).HasColumnName("fecha_realizacion");
            entity.Property(e => e.IdMetodoAbono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_metodo_abono");
            entity.Property(e => e.IdOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_orden");
            entity.Property(e => e.IdPayoutEstado)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_payout_estado");
            entity.Property(e => e.IdVendedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_vendedor");
            entity.Property(e => e.MontoNeto).HasColumnName("monto_neto");
            entity.Property(e => e.PorcentajeComision).HasColumnName("porcentaje_comision");

            entity.HasOne(d => d.IdMetodoAbonoNavigation).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.IdMetodoAbono)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metodo_abono_estado_Payouts");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Payouts");

            entity.HasOne(d => d.IdPayoutEstadoNavigation).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.IdPayoutEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payout_estado_Payouts");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Payouts");
        });

        modelBuilder.Entity<PayoutEstado>(entity =>
        {
            entity.HasKey(e => e.IdPayoutEstado);

            entity.ToTable("Payout_estado");

            entity.Property(e => e.IdPayoutEstado)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_payout_estado");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(85)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_categoria");
            entity.Property(e => e.IdCondicion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_condicion");
            entity.Property(e => e.IdVendedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_vendedor");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.TituloProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo_producto");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categoria_producto_Productos");

            entity.HasOne(d => d.IdCondicionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCondicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Condicion_producto_Productos");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Productos");
        });

        modelBuilder.Entity<ProductoMedium>(entity =>
        {
            entity.HasKey(e => e.IdMedia);

            entity.ToTable("Producto_media");

            entity.Property(e => e.IdMedia)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_media");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.PosicionGaleria).HasColumnName("posicion_galeria");
            entity.Property(e => e.ProductoPrimario).HasColumnName("producto_primario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoMedia)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Producto_media");
        });

        modelBuilder.Entity<ProveedorPayment>(entity =>
        {
            entity.HasKey(e => e.IdProveedorPago);

            entity.ToTable("Proveedor_payments");

            entity.Property(e => e.IdProveedorPago)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_proveedor_pago");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombre_proveedor");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.Property(e => e.ReviewId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("review_id");
            entity.Property(e => e.Comentario)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdArticuloOrden)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_articulo_orden");
            entity.Property(e => e.IdComprador)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_comprador");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.IdVendedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_vendedor");
            entity.Property(e => e.Publico).HasColumnName("publico");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.Property(e => e.IdRol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_usuario");
            entity.Property(e => e.EmailUsuario)
                .IsUnicode(false)
                .HasColumnName("email_usuario");
            entity.Property(e => e.IdEstado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_estado");
            entity.Property(e => e.IdRol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id_rol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(175)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.TelefonoUsuario)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono_usuario");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estado_usuario_Usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roleso_Usuario");
        });

        modelBuilder.Entity<VariantesProducto>(entity =>
        {
            entity.HasKey(e => e.IdProductoVariante);

            entity.ToTable("Variantes_productos");

            entity.Property(e => e.IdProductoVariante)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto_variante");
            entity.Property(e => e.Color)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Talla)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("talla");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.VariantesProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Variantes_productos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
