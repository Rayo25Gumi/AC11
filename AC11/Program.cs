using System.Globalization;
using System.Numerics;

public class Personas
{
    public string Nombre { get; set; }
    public string DNI { get; set; }

    public Personas(string nombre, string dni)
    {
        Nombre = nombre;
        DNI = dni;
    }
}

public class Empleados : Personas
{
    public string Cargo { get; set; }
    public int Salario { get; set; }

    public Empleados(string cargo, int salario, string nombre, string dni)
        : base(nombre, dni)
    {
        Cargo = cargo;
        Salario = salario;
    }
}

public class Tareas
{
    public string NombreTarea { get; set; }
    public string Estado { get; set; }
    public string Descripcion { get; set; }

    public Tareas(string nombretarea, string estado, string descripcion)
    {
        NombreTarea = nombretarea;
        Estado = estado;
        Descripcion = descripcion;
    }
}

public class Clientes : Personas
{
    public double Pagado { get; set; }
    public double Adelanto { get; set; }

    public Clientes(string nombre, string dni, double pagado, double adelanto)
        : base(nombre, dni)
    {
        Pagado = pagado;
        Adelanto = adelanto;
    }
}

public class Proveedores : Personas
{
    public Proveedores(string nombre, string dni)
        : base(nombre, dni) { }
}

public class Proyectos
{
    public string NombreProyecto { get; set; }
    public string Descripcion { get; set; }
    public int Dias { get; set; }
    public List<Empleados> Empleado { get; set; } = new List<Empleados>();
    public List<Tareas> Tarea { get; set; } = new List<Tareas>();
    public List<Clientes> Cliente { get; set; } = new List<Clientes>();
    public List<Proveedores> Proveedor { get; set; } = new List<Proveedores>();
    public double Costo { get; set; }
    public string Estado { get; set; }

    public Proyectos(string nombre, string descripcion, int dias)
    {
        NombreProyecto = nombre;
        Descripcion = descripcion;
        Dias = dias;
        Costo = CostoEstimado(dias);
        Estado = EstadoProyecto();
    }

    public string EstadoProyecto()
    {
        foreach (Tareas tarea in Tarea)
        {
            if (tarea.Estado != "Completado")
            {
                return "En proceso";
            }
        }
        return "Completado";
    }

    static double CostoEstimado(double a)
    {
        double costedia = 53;
        double total = a * costedia;
        return (total);
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        List<Proyectos> proyectos = new List<Proyectos>();
        string respuesta;

        do
        {
            Console.WriteLine("Escribe los datos del proyecto:");
            Console.Write("Nombre del Proyecto: ");
            string nombreProyecto = Console.ReadLine();

            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();

            Console.Write("Duración en días: ");
            int dias = Convert.ToInt32(Console.ReadLine());

            Proyectos nuevoProyecto = new Proyectos(nombreProyecto, descripcion, dias);

            do
            {
                Console.WriteLine("Escribe los datos del empleado:");
                Console.Write("Nombre del empleado: ");
                string nombreEmpleado = Console.ReadLine();

                Console.Write("DNI del empleado: ");
                string dniEmpleado = Console.ReadLine();

                Console.Write("Cargo del empleado: ");
                string cargoEmpleado = Console.ReadLine();

                Console.Write("Salario: ");
                int salario = Convert.ToInt32(Console.ReadLine());

                nuevoProyecto.Empleado.Add(
                    new Empleados(cargoEmpleado, salario, nombreEmpleado, dniEmpleado)
                );

                Console.WriteLine("Quieres registrar otro empleado? Escribe si/no");
                respuesta = Console.ReadLine();
            } while (respuesta == "si");

            do
            {
                Console.WriteLine("Escribe los datos de la tarea:");
                Console.WriteLine("Nombre de la tarea: ");
                string nombreTarea = Console.ReadLine();

                Console.WriteLine("Estado (Pendiente/Completado): ");
                string estadoTarea = Console.ReadLine();

                Console.WriteLine("Descripción: ");
                string descripcionTarea = Console.ReadLine();

                nuevoProyecto.Tarea.Add(new Tareas(nombreTarea, estadoTarea, descripcionTarea));

                Console.WriteLine("Quieres registrar otra tarea? Escribe si/no");
                respuesta = Console.ReadLine();
            } while (respuesta == "si");

            do
            {
                Console.WriteLine("Escribe los datos del cliente:");
                Console.WriteLine("Nombre del cliente: ");
                string nombreCliente = Console.ReadLine();

                Console.WriteLine("DNI del cliente: ");
                string dniCliente = Console.ReadLine();

                Console.WriteLine("Cantidad pagada: ");
                double pagado = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Cantidad adelantada: ");
                double adelanto = Convert.ToDouble(Console.ReadLine());

                nuevoProyecto.Cliente.Add(
                    new Clientes(nombreCliente, dniCliente, pagado, adelanto)
                );

                Console.WriteLine("Quieres registrar otro cliente? Escribe si/no");
                respuesta = Console.ReadLine();
            } while (respuesta == "si");

            do
            {
                Console.WriteLine("Escribe los datos del proveedor:");
                Console.Write("Nombre del proveedor: ");
                string nombreProveedor = Console.ReadLine();

                Console.Write("DNI del proveedor: ");
                string dniProveedor = Console.ReadLine();

                nuevoProyecto.Proveedor.Add(new Proveedores(nombreProveedor, dniProveedor));

                Console.WriteLine("Quieres registrar otro proveedor? Escribe si/no");
                respuesta = Console.ReadLine();
            } while (respuesta == "si");

            proyectos.Add(nuevoProyecto);

            Console.WriteLine("Quieres registrar otro proyecto? Escribe si/no");
            respuesta = Console.ReadLine();
        } while (respuesta == "si");
        Console.WriteLine("Resumen de proyectos:");
        foreach (var proyecto in proyectos)
        {
            Console.WriteLine($"Proyecto: {proyecto.NombreProyecto}");
            Console.WriteLine($"Descripción: {proyecto.Descripcion}");
            Console.WriteLine($"Duración: {proyecto.Dias} días");
            Console.WriteLine($"Costo estimado: {proyecto.Costo} €");
            Console.WriteLine($"Estado: {proyecto.Estado}");

            Console.WriteLine("Trabajadores asignados:");
            foreach (var empleado in proyecto.Empleado)
            {
                Console.WriteLine(
                    $" Nombre: {empleado.Nombre}, Cargo: {empleado.Cargo}, Salario: {empleado.Salario} €"
                );
            }

            Console.WriteLine("Tareas:");
            foreach (var tarea in proyecto.Tarea)
            {
                Console.WriteLine(
                    $" Nombre: {tarea.NombreTarea}, Estado: {tarea.Estado}, Descripción: {tarea.Descripcion}"
                );
            }

            Console.WriteLine("Clientes:");
            foreach (var cliente in proyecto.Cliente)
            {
                Console.WriteLine(
                    $" Nombre: {cliente.Nombre}, DNI: {cliente.DNI}, Pagado: {cliente.Pagado} €, Adelanto: {cliente.Adelanto} €"
                );
            }

            Console.WriteLine("Proveedores:");
            foreach (var proveedor in proyecto.Proveedor)
            {
                Console.WriteLine($"Nombre: {proveedor.Nombre}, DNI: {proveedor.DNI}");
            }

            Console.WriteLine();
        }
    }
}
