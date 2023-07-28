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
    private int saludInicial;
    private int salud;
    private int energia;
    private int semillas;

    private bool estado;

    public Razas Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public int Nac { get => nac; set => nac = value; }
    public int Edad { get => edad; set => edad = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int SaludInicial { get => saludInicial; set => saludInicial = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Energia { get => energia; set => energia = value; }
    public int Semillas { get => semillas; set => semillas = value; }
    public bool Estado { get => estado; set => estado = value; }

    public void Mostrar ()
    {
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

    public void Ganador(int fase)
    {
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);

        Salud = SaludInicial + (fase+1)*20;
        Energia = 100 + (fase+1)*10;

        Velocidad += rnd.Next(0,2);
        Destreza += rnd.Next(0,2);
        Fuerza += rnd.Next(0,2);
        Nivel += rnd.Next(0,2);
        Armadura += rnd.Next(0,2);
    }

    public void Transfor()
    {
        double IncS = Salud*(0.30);
        double IncE = Energia*(0.50);
        
        if (!estado)
        {
            Salud += (int)IncS;
            Energia += (int)IncE;

            Velocidad += 2;
            Destreza += 2;
            Fuerza += 2;
            Nivel += 2;
            Armadura += 2;

            Estado = true;
        }

        else
        {
            Salud -= (int)IncS-10;
            Energia -= (int)IncE;

            Velocidad -= 2;
            Destreza -= 2;
            Fuerza -= 2;
            Nivel -= 2;
            Armadura -= 2;

            Estado = false;
        }
    }

}
