
using Humanizer;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public static class DTO_transformation
    {
        public static Categoria toOriginal(this CategoriaDTO dto)
        {
            return new()
            {
                CategoriaId = dto.CategoriaId,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Pesokg = dto.Pesokg ?? 0m

            };
        }

        public static Cliente toOriginal(this ClienteDTO dto)
        {

            return new()
            {
                ClienteId = dto.ClienteId,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                CorreoElectronico = dto.CorreoElectronico,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Sexo = dto.Sexo,
                Edad = dto.Edad,
                DireccionEnvio = dto.DireccionEnvio,
                MetodoPago = dto.MetodoPago,
                Orden = dto.Orden,
                ReseñaProducto = dto.ReseñaProducto
            };

        }
        public static Descuento toOriginal(this DescuentoDTO dto)
        {
            return new Descuento
            {
                DescuentoId = dto.DescuentoId,
                PorcentajeDescuento = dto.PorcentajeDescuento ?? 0m, // Asigna 0 si es null
                FechaInicio = dto.FechaInicio ?? DateOnly.FromDateTime(DateTime.Today), // Asigna la fecha actual si es null
                FechaFin = dto.FechaFin ?? DateOnly.FromDateTime(DateTime.Today), // Asigna la fecha actual si es null
                TipoDescuento = dto.TipoDescuento,
            };
        }


        public static DetalleOrden toOriginal(this DetalleOrdenDTO dto)
        {
            return new()
            {
                DetalleId = dto.DetalleId,
                OrdenId = dto.OrdenId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioTotal = dto.PrecioTotal,
                Orden = dto.Orden,
                Producto = dto.Producto
            };
        }
        public static DireccionEnvio toOriginal(this DireccionEnviosDTO dto)
        {
            return new()
            {
                DireccionId = dto.DireccionId,
                ClienteId = dto.ClienteId,
                Dirección = dto.Dirección,
                Ciudad = dto.Ciudad,
                Provincia = dto.Provincia,
                Pais = dto.Pais,
                CodigoPostal = dto.CodigoPostal,
                Cliente = dto.Cliente

            };
        }
        public static MetodoPago toOriginal(this MetodoPagoDTO dto)
        {
            return new()
            {
                MetodoPagoId = dto.MetodoPagoId,
                ClienteId = dto.ClienteId,
                TipoTarjeta = dto.TipoTarjeta,
                NumeroTarjeta = dto.NumeroTarjeta ?? 0,
                FechaExpiracion = dto.FechaExpiracion ?? DateOnly.FromDateTime(DateTime.Today),
                Cvv = dto.Cvv ?? 0


            };
        }
        public static Orden toOriginal(this OrdenDTO dto)
        {
            return new Orden
            {
                OrdenId = dto.OrdenId,
                FechaOrden = dto.FechaOrden ?? DateOnly.FromDateTime(DateTime.Today), // Asigna la fecha actual si es null
                Total = dto.Total ?? 0, // Asigna 0 si es null
                Estado = dto.Estado,
                ClienteId = dto.ClienteId,
                Cliente = dto.Cliente,
                DetalleOrden = dto.DetalleOrden
            };
        }
        public static Producto toOriginal(this ProductoDTO dto)
        {
            return new()
            {
                ProductoId = dto.ProductoId,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio ?? 0,
                Cantidad = dto.Cantidad ?? 0,
                Procedencia = dto.Procedencia,
                Estado = dto.Estado,
                Marca = dto.Marca,
                DescuentoId = dto.DescuentoId,
                CategoriaId = dto.CategoriaId,
                Descuento = dto.Descuento,
                DetalleOrden = dto.DetalleOrden,
                ReseñaProducto = dto.ReseñaProducto
            };
        }
        public static ReseñaProducto toOriginal(this ReseñaProductoDTO dto)
        {
            return new()
            {
                ReseñaId = dto.ReseñaId,
                ProductoId = dto.ProductoId,
                ClienteId = dto.ClienteId,
                Calificación = dto.Calificación ?? 0,
                Comentario = dto.Comentario,
                FechaReseña = dto.FechaReseña ?? DateTime.Now,//Error
                Cliente = dto.Cliente,
                Producto = dto.Producto
            };
        }
        //-------------------------------------------------TO DTO---------------------------------------------------

        public static CategoriaDTO ToDto(this Categoria original)
        {
            return new()
            {
                CategoriaId = original.CategoriaId,
                Nombre = original.Nombre,
                Descripcion = original.Descripcion,
                Pesokg = original.Pesokg

            };
        }
        public static ReseñaProductoDTO ToDto(this ReseñaProducto original)
        {
            return new()
            {
                ReseñaId = original.ReseñaId,
                ProductoId = original.ProductoId,
                ClienteId = original.ClienteId,
                Calificación = original.Calificación,
                Comentario = original.Comentario,
                FechaReseña = original.FechaReseña,
                Cliente = original.Cliente,
                Producto = original.Producto
            };
        }





        public static ClienteDTO ToDto(this Cliente Original)
        {

            return new()
            {
                ClienteId = Original.ClienteId,
                Nombre = Original.Nombre,
                Apellido = Original.Apellido,
                CorreoElectronico = Original.CorreoElectronico,
                Direccion = Original.Direccion,
                Telefono = Original.Telefono,
                Sexo = Original.Sexo,
                Edad = Original.Edad,
                DireccionEnvio = Original.DireccionEnvio,
                MetodoPago = Original.MetodoPago,
                Orden = Original.Orden,
                ReseñaProducto = Original.ReseñaProducto,
            };


        }
        public static ProductoDTO toDto(this Producto Original)
        {
            return new()
            {
                ProductoId = Original.ProductoId,
                Nombre = Original.Nombre,
                Descripcion = Original.Descripcion,
                Precio = Original.Precio,
                Cantidad = Original.Cantidad,
                Procedencia = Original.Procedencia,
                Estado = Original.Estado,
                Marca = Original.Marca,
                DescuentoId = Original.DescuentoId,
                CategoriaId = Original.CategoriaId,
                Descuento = Original.Descuento,
                DetalleOrden = Original.DetalleOrden,
                ReseñaProducto = Original.ReseñaProducto
            };
        }
        public static DescuentoDTO toDto(this Descuento Original)
        {
            return new()
            {
                DescuentoId = Original.DescuentoId,
                PorcentajeDescuento = Original.PorcentajeDescuento,
                FechaInicio = Original.FechaInicio,
                FechaFin = Original.FechaFin,
                TipoDescuento = Original.TipoDescuento,
            };
        }
        public static DetalleOrdenDTO toDto(this DetalleOrden Original)
        {
            return new()
            {
                DetalleId = Original.DetalleId,
                OrdenId = Original.OrdenId,
                ProductoId = Original.ProductoId,
                Cantidad = Original.Cantidad,
                PrecioTotal = Original.PrecioTotal,
                Orden = Original.Orden,
                Producto = Original.Producto,
            };
        }
        public static DireccionEnviosDTO toDto(this DireccionEnvio Original)
        {
            return new()
            {
                DireccionId = Original.DireccionId,
                ClienteId = Original.ClienteId,
                Dirección = Original.Dirección,
                Ciudad = Original.Ciudad,
                Provincia = Original.Provincia,
                Pais = Original.Pais,
                CodigoPostal = Original.CodigoPostal,
                Cliente = Original.Cliente

            };
        }
        public static MetodoPagoDTO toDto(this MetodoPago Original)
        {
            return new()
            {
                MetodoPagoId = Original.MetodoPagoId,
                ClienteId = Original.ClienteId,
                TipoTarjeta = Original.TipoTarjeta,
                NumeroTarjeta = Original.NumeroTarjeta,
                FechaExpiracion = Original.FechaExpiracion,
                Cvv = Original.Cvv


            };
        }
        public static OrdenDTO toDto(this Orden Original)
        {
            return new()
            {
                OrdenId = Original.OrdenId,
                FechaOrden = Original.FechaOrden,
                Total = Original.Total,
                Estado = Original.Estado,
                ClienteId = Original.ClienteId,
                Cliente = Original.Cliente,
                DetalleOrden = Original.DetalleOrden
            };
        }
    }
}