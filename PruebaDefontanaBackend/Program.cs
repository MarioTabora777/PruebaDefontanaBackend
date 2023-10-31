using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaDefontanaBackend.DatabaseFiles;
using PruebaDefontanaBackend.Services;





IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();




var connectionString = configuration.GetConnectionString("Prueba");

var options = new DbContextOptionsBuilder<PruebaContext>()
    .UseSqlServer(connectionString)
    .Options;


using var context = new PruebaContext(options);     

VentasService ventasService  = new VentasService(context);

var ventas = ventasService.ObtenerVentas(30);






Console.WriteLine("-----------------------------Venta de los últimos 30 días----------------------------------");
foreach (var venta in ventas)
    Console.WriteLine($"Total  { venta.Total.ToString() }  Fecha  { venta.Fecha.ToString()}");


Console.WriteLine("\n----El total de ventas de los últimos 30 días (monto total y cantidad total de ventas)-----");
var ventasDetalle = ventas.SelectMany(i => i.VentaDetalles);
var CantidadTransacciones = ventas.Count();
var totalVenta = ventasDetalle.Sum(i => i.TotalLinea);
var CantidadTotal = ventasDetalle.Sum(i => i.Cantidad);


Console.WriteLine("Monto Total:" + totalVenta.ToString());
Console.WriteLine("Cantidad Total:" + CantidadTotal.ToString());
Console.WriteLine("Cantidad de Transacciones:" + CantidadTransacciones.ToString());
Console.WriteLine("-------------------------------------------------------------------------------------------\n");

Console.WriteLine("-----El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto---");
var VentaMontoAlto = ventas.OrderByDescending(i=> i.Total).FirstOrDefault();

if (VentaMontoAlto == null)
    Console.WriteLine("Sin Datos");
else Console.WriteLine($"La venta con el monto más alto se realizó el {VentaMontoAlto.Fecha.Date.ToString("dd/MM/yyyy")} a las {VentaMontoAlto.Fecha.TimeOfDay} y el monto es {VentaMontoAlto.Total}");

Console.WriteLine("-------------------------------------------------------------------------------------------\n");

Console.WriteLine("--Indicar cuál es el producto con mayor monto total de ventas.-------------------------------");

var productoConMayorMonto = ventas
    .SelectMany(i => i.VentaDetalles)
    .GroupBy(i => i.Producto)
    .Select(i => new
    {
        Producto = i.Key,
        MontoTotalVentas = i.Sum(detalle => detalle.TotalLinea)
    })
    .OrderByDescending(i => i.MontoTotalVentas)
    .FirstOrDefault();
if(productoConMayorMonto != null)
        Console.WriteLine($"El producto con el mayor monto de venta es {productoConMayorMonto.Producto.Nombre}\ncon un monto de {productoConMayorMonto.MontoTotalVentas}");
else
    Console.WriteLine("Sin Datos");

Console.WriteLine("-------------------------------------------------------------------------------------------\n");
Console.WriteLine("-------------------------------Indicar el local con mayor monto de ventas--------------------");

var localConMayorMonto = ventas
    .GroupBy(i => i.Local)
    .Select(i => new
    {
        Local = i.Key,
        MontoTotalVentas = i.Sum(venta => venta.Total)
    })
    .OrderByDescending(i => i.MontoTotalVentas)
    .FirstOrDefault();


if (localConMayorMonto != null)
    Console.WriteLine($"El local con mayor monto de venta es {localConMayorMonto.Local.Nombre} con un monto de  { localConMayorMonto.MontoTotalVentas }"); 
else
    Console.WriteLine("Sin Datos");

Console.WriteLine("-------------------------------------------------------------------------------------------\n");
Console.WriteLine("--------------¿Cuál es la marca con mayor margen de ganancias?-------------------------------");

var marcaConMayorMargen = ventas
    .SelectMany(venta => venta.VentaDetalles)
    .GroupBy(detalle => detalle.Producto.Marca)
    .Select(grupo => new
    {
        Marca = grupo.Key,
        MargenGanancias = grupo.Sum(detalle => (detalle.Cantidad * detalle.Precio_Unitario) - (detalle.Cantidad * detalle.Producto.Costo_Unitario))
    })
    .OrderByDescending(x => x.MargenGanancias)
    .FirstOrDefault();
if (marcaConMayorMargen != null)
    Console.WriteLine($"la marca con mayor margen de ganancia es {marcaConMayorMargen.Marca.Nombre} y el margen es {marcaConMayorMargen.MargenGanancias}");
else
    Console.WriteLine("Sin Datos");

Console.WriteLine("-------------------------------------------------------------------------------------------\n");
Console.WriteLine("--------------¿Cómo obtendrías cuál es el producto que más se vende en cada local??-------------------------------");

var productosMasVendidosPorLocal = ventas
    .SelectMany(i => i.VentaDetalles)
    .GroupBy(i => new { i.Venta.Local, i.Producto })
    .Select(i => new
    {
        Local = i.Key.Local,
        Producto = i.Key.Producto,
        TotalVendido = i.Sum(y => y.Cantidad)
    })
    .GroupBy(i => i.Local)
    .Select(i => new
    {
        Local = i.Key,
        ProductoMasVendido = i.OrderByDescending(y => y.TotalVendido).FirstOrDefault()
    })
    .ToList();


if (productosMasVendidosPorLocal != null)
    foreach (var producto in productosMasVendidosPorLocal)
        Console.WriteLine($"{producto.ProductoMasVendido.Producto.ID_Producto}En el local {producto.Local.Nombre} el producto más vendido es {producto.ProductoMasVendido.Producto.Nombre}\nCon un total de {producto.ProductoMasVendido.TotalVendido}\n"); 
else 
    Console.WriteLine("Sin Datos");