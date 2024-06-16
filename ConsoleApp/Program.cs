using Dominio;

List<Producto> listaProductos = new List<Producto>();

while (true)
{
    string opcion;
    Console.WriteLine("1- Registrar producto");
    Console.WriteLine("2- Comprar producto");
    opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            RegistrarProducto();
            break;

        case "2":
            ComprarProducto()

            break;

        default:
            break;
    }
}

void RegistrarProducto()
{
    var nuevoProducto = new Producto();

    Console.Write("Nombre: ");
    nuevoProducto.Nombre = Console.ReadLine();
    Console.Write("Descripcion: ");
    nuevoProducto.Descripcion = Console.ReadLine();
    Console.Write("Precio: ");
    Double.TryParse(Console.ReadLine(), out double precio);
    Console.Write("Stock: ");
    Int32.TryParse(Console.ReadLine(), out int stock);

    nuevoProducto.Precio = precio;
    nuevoProducto.Stock = stock;

    if (listaProductos.Any(producto => producto.Nombre == nuevoProducto.Nombre))
    {
        Console.WriteLine("Error: el producto ya esta registrado.");
    }
    else
    {
        listaProductos.Add(nuevoProducto);
        Console.WriteLine("Producto registrado correctamente.");
    }
}

void ComprarProducto()
{
   /* if (!usuarioLogueado) falta autentificacion
    {
        Console.WriteLine("Debe iniciar sesión para comprar productos.");
        return;
    }
    */
    Console.Write("Nombre del producto: ");
    string nombreProducto = Console.ReadLine();
    Console.Write("Cantidad a comprar: ");
    Int32.TryParse(Console.ReadLine(), out int cantidad);

    var producto = listaProductos.FirstOrDefault(p => p.Nombre == nombreProducto);

    if (producto == null)
    {
        Console.WriteLine("Error: el producto no existe.");
    }
    else if (producto.Stock < cantidad)
    {
        Console.WriteLine("Error: no hay suficiente stock.");
    }
    else
    {
        producto.Stock -= cantidad;
        Console.WriteLine("Compra realizada con éxito.");
    }
}