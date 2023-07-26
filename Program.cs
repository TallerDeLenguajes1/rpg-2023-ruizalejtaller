using EspPersonaje;
internal class Program
{
    private static void Main(string[] args)
    {
        var Datos = new PersonajesJson();
        var Personajes = new List<Personaje>();
        string str;
        int op;

        if(Datos.Existe("Personajes.json"))
        {
            Console.Clear();
            Console.WriteLine("Existe un archivo de Personajes guardado");
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("[Enter] Utilizar");
            Console.WriteLine("1. Crear uno nuevo\n");
            str = Console.ReadLine();

            if (int.TryParse(str, out op))
            {
                if (op==1)
                {
                    Personajes = Generar();
                    Datos.GuardarPersonajes("Personajes.json", Personajes);
                } else Personajes = Datos.LeerPersonajes("Personajes.json");
            } else Personajes = Datos.LeerPersonajes("Personajes.json");
        }

        Console.WriteLine("Personaje seleccionado");
        Console.WriteLine("----------------------");
        Personajes[0].Mostrar();

        Enter();
        
        Console.WriteLine("Torneo de las artes marciales");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("\nSon los Cuartos de final");
        Console.WriteLine("\nSe enfrentan " + Personajes[0].Nombre + " vs " + Personajes[1].Nombre);

        Enter();

        Combate(Personajes[0], Personajes[1]);

    }

    // Multiples funciones
    static List<Personaje> Generar()
    {
        var Personajes = new List<Personaje>();
        Personajes.Add(Seleccionar());
        Personaje Personaje;
        bool flag = true, Esta;

        for (int i=0; i<15; i++)
        {
            while (flag)
            {
                Personaje = FabricaDePersonajes.Crear();
                Esta = false;

                for (int j=0; j<Personajes.Count; j++)
                {
                    if (Personajes[j].Nombre == Personaje.Nombre)
                    {
                        Esta = true;
                    }
                }

                if (!Esta)
                    {
                        flag = false;
                        Personajes.Add(Personaje);
                    }
            }

           flag = true;
        }

        return Personajes;

    }
    static Personaje Seleccionar()
    {
        string str;
        int esp;
        int name;
        bool flag = true;

        Personaje Personaje = new Personaje();

        while (flag)
        {
            Console.Clear();
            Console.WriteLine("Elija un personaje: ");
            Console.WriteLine("Especies: ");
            Console.WriteLine("1. Sayayin --- 2. Namekiano --- 3. Ice --- 4. Androide --- 5. Humano ");

            str = Console.ReadLine();

            if (int.TryParse(str, out esp))
            {
                if (esp>0 && esp <6)
                {
                    switch (esp)
                    {
                        case 1:
                            var Sayayin = new Sayayin();
                            for (int i=0; i<5; i++)
                            {
                                Console.WriteLine($"{i+1}. {Sayayin.Nombres[i]}");
                            }
                            
                        break;

                        case 2:
                            var Namekiano = new Namekiano();
                            for (int i=0; i<5; i++)
                            {
                                Console.WriteLine($"{i+1}. {Namekiano.Nombres[i]}");
                            }
                            
                        break;

                        case 3:
                            var Ice = new Ice();
                            for (int i=0; i<5; i++)
                            {
                                Console.WriteLine($"{i+1}. {Ice.Nombres[i]}");
                            }
                            
                        break;

                        case 4:
                            var Androide = new Androide();
                            for (int i=0; i<5; i++)
                            {
                                Console.WriteLine($"{i+1}. {Androide.Nombres[i]}");
                            }
                        break;

                        case 5:
                            var Humano = new Humano();
                            for (int i=0; i<5; i++)
                            {
                                Console.WriteLine($"{i+1}. {Humano.Nombres[i]}");
                            }
               
                        break;
                    }

                    str = Console.ReadLine();
                    if (int.TryParse(str, out name))
                    {
                        if (name>0 && name <6)
                        {
                            flag = false;

                            Personaje = FabricaDePersonajes.Armar(esp-1, name-1);
                        } else Console.WriteLine("No es una opcion valida\n");
                    }
                } else Console.WriteLine("No es una opcion valida\n");


            }

        }
        Console.WriteLine($"Personaje elegido: {Personaje.Nombre}\n");
        return Personaje;
    }


    static void Enter ()
    {

        Console.WriteLine("\nPulse Enter para continuar");
        Console.ReadKey();
        Console.Clear();
    }

    static bool Combate(Personaje Personaje_A, Personaje Personaje_B)
    {
        Personaje Ganador;
        int DProvocado = 0;
        string str;
        int op;
        bool win = false, turno = false;

        while (Personaje_A.Salud > 0 && Personaje_B.Salud > 0)
        {
            Console.Clear();
            Console.WriteLine ("-- " +Personaje_A.Nombre + " --   Salud: " + Personaje_A.Salud + "   Energia: " + Personaje_A.Energia);
            Console.WriteLine ("\n-- " + Personaje_B.Nombre + " -- Salud: " + Personaje_B.Salud + "   Energia: " + Personaje_B.Energia);

            Console.WriteLine ("\n\n\n1. Atacar   2. Usar Semilla    3. Info Oponente   4. Rendirse");
            str = Console.ReadLine();
            

            if (int.TryParse(str, out op))
            {
                switch (op)
                {
                    case 1:
                        DProvocado = Atacar(Personaje_A, Personaje_B);
                        Personaje_B.Salud -= DProvocado;
                        Personaje_B.Energia -= DProvocado/3;
                        turno = true;                        
                    break;

                    case 2:
                        if (Personaje_A.Semillas > 0)
                        {
                            UsarSemilla(Personaje_A, true);
                            turno = true;
                        } else Console.WriteLine("\n No hay semillas disponibles\n");
                        Thread.Sleep(1500);
                        break;

                    case 3:
                        Personaje_B.Mostrar();
                        Enter();
                    break;

                    case 4:
                        Console.WriteLine("\n" + Personaje_A.Nombre + " se rinde. ");
                        Personaje_A.Salud = 0;
                        Thread.Sleep(1500);
                    break;
                }
            }

            if (Personaje_B.Salud > 0 && turno)
            {
                DProvocado = CPMove(Personaje_B, Personaje_A, true);

                Personaje_A.Salud -= DProvocado;
                Personaje_A.Energia -= DProvocado/3;

                turno = false;
            }



            
        }

        if (Personaje_A.Salud <= 0)
        {
            Ganador = Personaje_B;
        } else Ganador = Personaje_A;

        Console.Clear();
        Console.WriteLine ("-- " +Personaje_A.Nombre + " --   Salud: " + Personaje_A.Salud + "   Energia: " + Personaje_A.Energia);
        Console.WriteLine ("\n-- " + Personaje_B.Nombre + " -- Salud: " + Personaje_B.Salud + "   Energia: " + Personaje_B.Energia);


        Console.WriteLine("\n\n El ganador es: " + (Ganador.Nombre));

        return win;

    }

    static int Atacar (Personaje Pers_A, Personaje Pers_B)
    {
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);

        int ataque = Pers_A.Destreza * Pers_A.Fuerza * Pers_A.Nivel;
        int defensa = Pers_B.Armadura * Pers_B.Velocidad;
        int efectividad = rnd.Next(1,101);
        const int ajuste = 1000;

        int DProvocado = (((ataque*efectividad) - defensa)/ajuste) + Pers_A.Energia/10;

        Console.WriteLine(Pers_A.Nombre + " realiza un ataque quitando "+ DProvocado + " puntos de salud a su oponente\n");
        Thread.Sleep(2000);
        return DProvocado;
    }

    static int CPMove (Personaje Pers_A, Personaje Pers_B, bool ver)
    {
        int DProvocado = 0;
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);
        int prob = rnd.Next(1,(100-Pers_A.Salud)+1);
        bool turno = true;

        if (Pers_A.Semillas > 0 && prob > 50)
        {
            UsarSemilla(Pers_A, ver);
            Thread.Sleep(1500);
            turno = false;
        }

        if (turno)
        {
            DProvocado = Atacar(Pers_A, Pers_B);
        }

        Console.WriteLine(DProvocado);

        return DProvocado;

    }

    static void UsarSemilla(Personaje Pers, bool ver) 
    {   
        if(ver)
        {
            Console.WriteLine("\n" + Pers.Nombre + " utiliza una semilla del ermitaño y recupera su salud por completo\n");
        }
        Pers.Salud = 100;
        Pers.Energia = 100;
        Pers.Semillas -= 1;
    }

}