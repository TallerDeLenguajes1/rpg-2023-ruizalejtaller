namespace EspPersonaje;

public class Personaje
{
    private Razas tipo;
    private string nombre;
    private int nac;
    private int edad;
    private int velocidad;
    private int destreza;
    private int fuerza;
    private int nivel;
    private int armadura;
    private int salud;
    private int energia;
    private int semillas;

    public Razas Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public int Nac { get => nac; set => nac = value; }
    public int Edad { get => edad; set => edad = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Energia { get => energia; set => energia = value; }
    public int Semillas { get => semillas; set => semillas = value; }

    public void Mostrar ()
    {
        Console.Clear();
        Console.WriteLine("\n--- Datos ---");
        Console.WriteLine("Raza: " + Tipo);
        Console.WriteLine("Nombre: " + Nombre);
        Console.WriteLine("Año de Nacimiento: " + Nac + " (Edad: " + Edad + ")");

        Console.WriteLine("\n--- Caracteristicas ---");
        Console.WriteLine("Destreza: " + Destreza + " /// Fuerza: " + Fuerza + " /// Nivel: " + Nivel);
        Console.WriteLine("Velocidad: " + Velocidad + " /// Armadura: " + Armadura);
        Console.WriteLine("Salud: " + Salud + " /// Energia: " + Energia);

        Console.WriteLine("Semillas: " + Semillas);
    }

}
