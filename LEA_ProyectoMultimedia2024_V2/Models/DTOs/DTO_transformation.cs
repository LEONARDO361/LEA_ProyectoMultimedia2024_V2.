
using Humanizer;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                Pesokg = dto.Pesokg

            };
        }
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
            return new()
            {
                DescuentoId = dto.DescuentoId,
                PorcentajeDescuento = dto.PorcentajeDescuento,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
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
                NumeroTarjeta = dto.NumeroTarjeta,
                FechaExpiracion = dto.FechaExpiracion,
                Cvv = dto.Cvv


            };
        }
        public static Orden toOriginal(this OrdenDTO dto)
        {
            return new()
            {
                OrdenId = dto.OrdenId,
                FechaOrden = dto.FechaOrden,
                Total = dto.Total,
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
                Precio = dto.Precio,
                Cantidad = dto.Cantidad,
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
                Calificación = dto.Calificación,
                Comentario = dto.Comentario,
                FechaReseña = dto.FechaReseña,
                Cliente = dto.Cliente,
                Producto = dto.Producto
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
    }
}