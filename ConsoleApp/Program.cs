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