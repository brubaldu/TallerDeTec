using Dominio;

const string usuariosPath = "../../../../usuarios.txt";
List<Producto> listaProductos = new List<Producto>();
string opcion = "";
bool usuarioAutenticado = false;

while (opcion != "0" && !usuarioAutenticado)
{
    Console.WriteLine(" -----------Login----------");
    Console.WriteLine("|  1- Registrar usuario   |");
    Console.WriteLine("|  2- Ingresar            |");
    Console.WriteLine("|                         |");
    Console.WriteLine("|  0- Salir               |");
    Console.WriteLine(" -------------------------");
    opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            RegistrarUsuario();
            break;

        case "2":
            Login();
            break;

        case "0":
            Console.WriteLine("¡Gracias por utilizar el sistema EZKiosco!");
            break;

        default:
            Console.WriteLine("Seleccione una opcion valida");
            Console.WriteLine();
            break;
    }
}

while (opcion != "0")
{
    Console.WriteLine(" -----------Menú----------");
    Console.WriteLine("|  1- Registrar producto  |");
    Console.WriteLine("|  2- Comprar producto    |");
    Console.WriteLine("|                         |");
    Console.WriteLine("|  0- Salir               |");
    Console.WriteLine(" -------------------------");
    opcion = Console.ReadLine();
    Console.WriteLine();

    switch (opcion)
    {
        case "1":
            RegistrarProducto();
            break;

        case "2":ComprarProducto();
            break;

        case "0":
            Console.WriteLine("¡Gracias por utilizar el sistema EZKiosco!");
            break;

        default:
            Console.WriteLine("Seleccione una opcion valida");
            Console.WriteLine();
            break;
    }
}

void RegistrarUsuario()
{
    Dictionary<string, string> credenciales = CargarUsuarios();

    Console.Write("Nombre de usuario: ");
    string usuario = Console.ReadLine();

    if (credenciales.ContainsKey(usuario))
    {
        Console.WriteLine("Error: Usuario ya registrado");
        Console.WriteLine();
    }
    else
    {
        Console.Write("Contraseña: ");
        string contrasenia = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(contrasenia))
        {
            Console.WriteLine("Error: debe ingresar una contraseña");
        }
        else
        {
            using (StreamWriter sw = File.AppendText(usuariosPath))
            {
                sw.WriteLine($"{usuario},{contrasenia}");
            }
            Console.WriteLine("¡Usuario registrado exitosamente!");
            Console.WriteLine();
        }
    }
}

void Login()
{
    Dictionary<string, string> credenciales = CargarUsuarios();

    Console.Write("Usuario: ");
    string usuario = Console.ReadLine();

    Console.Write("Contraseña: ");
    string contrasenia = Console.ReadLine();

    if (credenciales.TryGetValue(usuario, out var contraseniaGuardada) && contraseniaGuardada == contrasenia)
    {
        usuarioAutenticado = true;

        Console.Clear();
        Console.WriteLine($"¡Bienvenido {usuario}!");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Error: credenciales incorrectas");
        Console.WriteLine();
    }
}

static Dictionary<string, string> CargarUsuarios()
{
    var credenciales = new Dictionary<string, string>();

    foreach (var linea in File.ReadAllLines(usuariosPath))
    {
        var partes = linea.Split(',');
        if (partes.Length == 2)
        {
            var usuario = partes[0].Trim();
            var contrasenia = partes[1].Trim();
            credenciales[usuario] = contrasenia;
        }
    }

    return credenciales;
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