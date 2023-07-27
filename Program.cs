using EspPersonaje;
internal class Program
{
    private static void Main(string[] args)
    {
        var Datos = new PersonajesJson();
        var Personajes = new List<Personaje>();
        string[] Fases = {"Octavos de final", "Cuartos de final", "Semifinal", "Final"};
        string str;
        int op, fase = 0;
        bool cont = true;

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

        while (fase<4 && cont)
        {
            Fase(Fases[fase], Personajes[0], Personajes[1], true);
            Enter();
        
            if(Combate(Personajes[0], Personajes[1]))
            {
                Personajes.RemoveAt(1);
                RecSalud(Personajes[0]);
                cont = true;

                if (fase == 3)
                {
                    Console.WriteLine("\n////////////////////");
                    Console.WriteLine("//// " + Personajes[0].Nombre + " es el Campeon del Torneo de las Artes Marciales ////");
                    Console.WriteLine("/////////////////////");
                } else {
                    Console.WriteLine("es la fase "+ fase);
                    Console.WriteLine("\n\n--------------------------------------------------");
                    Console.WriteLine("1. Presenciar los encuentros restantes de la fase");
                    Console.WriteLine("2. Ver los ganadores directamente");
                    Console.WriteLine("Cualquier otra tecla, Salir\n\n");

                    str = Console.ReadLine();

                    if(int.TryParse(str, out op))
                    {
                        switch(op)
                        {
                            case 1:
                                Personajes = ComFases(Personajes, true, Fases[fase]);
                            break;

                            case 2:
                                Personajes = ComFases(Personajes, false, Fases[fase]);
                            break;
                            
                            default:
                                cont = false;
                            break;
                        }
                    } else cont = false;
                }
            } else cont = false;

            fase++;

            if (cont && fase<4)
            {
                Console.WriteLine("\n\nProxima ronda");
                Console.WriteLine("-------------");

                for(int j=0; j<Personajes.Count-1; j+=2)
                {
                    Console.WriteLine(Personajes[j].Nombre + " vs " + Personajes[j+1].Nombre);
                }

                Enter();
            }
        }

        if(!cont) Console.WriteLine("\n\n¡Juego terminado!");

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

    static void Fase(string fase, Personaje Pers_A, Personaje Pers_B, bool ver)
    {
        if (ver)
        {
            Console.Clear();
            Console.WriteLine("Torneo de las artes marciales");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\nFase: " + fase);
        }
        Console.WriteLine("\nSe enfrentan === " + Pers_A.Nombre + " vs " + Pers_B.Nombre + " === ");
    }

    static void Enter ()
    {

        Console.WriteLine("\nPulse [Enter] para continuar\n");
        Console.ReadKey();
        Console.Clear();
    }

    static bool Combate(Personaje Pers_A, Personaje Pers_B)
    {
        Personaje Ganador;
        int DProvocado = 0;
        string str;
        int op;
        bool win = false, turno = false;

        while (Pers_A.Salud > 0 && Pers_B.Salud > 0)
        {
            Valores(Pers_A, Pers_B);

            Console.WriteLine ("\n\n\n1. Atacar   2. Usar Semilla    3. Info Oponente   4. Rendirse\n");
            str = Console.ReadLine();
            

            if (int.TryParse(str, out op))
            {
                switch (op)
                {
                    case 1:
                        DProvocado = Atacar(Pers_A, Pers_B, true);
                        Pers_B.Salud -= DProvocado;
                        Pers_B.Energia -= DProvocado/3;
                        turno = true;                        
                    break;

                    case 2:
                        if (Pers_A.Semillas > 0)
                        {
                            UsarSemilla(Pers_A, true);
                            turno = true;
                        } else Console.WriteLine("\n No hay semillas disponibles\n");
                        Thread.Sleep(1500);
                        break;

                    case 3:
                        Pers_B.Mostrar();
                        Enter();
                    break;

                    case 4:
                        Console.WriteLine("\n" + Pers_A.Nombre + " se rinde. ");
                        Pers_A.Salud = 0;
                        Thread.Sleep(1500);
                    break;
                }
            }

            if (Pers_B.Salud > 0 && turno)
            {
                DProvocado = CPMove(Pers_B, Pers_A, true);

                Pers_A.Salud -= DProvocado;
                Pers_A.Energia -= DProvocado/3;

                turno = false;
            }



            
        }

        if (Pers_A.Salud <= 0)
        {
            Ganador = Pers_B;
        } else
            {
                Ganador = Pers_A;
                win = true;
            }

        Valores(Pers_A, Pers_B);


        Console.WriteLine("\n\n El ganador es: " + (Ganador.Nombre));

        return win;

    }

    static void Valores(Personaje Pers_A, Personaje Pers_B)
    {
        Console.Clear();
        Console.WriteLine ("-- " +Pers_A.Nombre + " --   Salud: " + Pers_A.Salud + "   Energia: " + Pers_A.Energia);
        Console.WriteLine ("\n-- " + Pers_B.Nombre + " -- Salud: " + Pers_B.Salud + "   Energia: " + Pers_B.Energia);
    }

    static int Atacar (Personaje Pers_A, Personaje Pers_B, bool ver)
    {
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);

        int ataque = Pers_A.Destreza * Pers_A.Fuerza * Pers_A.Nivel;
        int defensa = Pers_B.Armadura * Pers_B.Velocidad;
        int efectividad = rnd.Next(1,101);
        const int ajuste = 1000;

        int DProvocado = (((ataque*efectividad) - defensa)/ajuste) + Pers_A.Energia/10;
        if (ver)
        {
            Console.WriteLine("\n" + Pers_A.Nombre + " realiza un ataque quitando "+ DProvocado + " puntos de salud a su oponente\n");
            Thread.Sleep(2000);
        }
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
            DProvocado = Atacar(Pers_A, Pers_B, ver);
        }

        return DProvocado;

    }

    static void UsarSemilla(Personaje Pers, bool ver) 
    {   
        if(ver)
        {
            Console.WriteLine(Pers.Nombre + " utiliza una semilla del ermitaño y recupera su salud por completo\n");
        }
        RecSalud(Pers);

        Pers.Semillas -= 1;
    }

    static void RecSalud(Personaje Pers)
    {
        Pers.Salud = 100;
        Pers.Energia = 100;
    }


    static bool ComvsCom(Personaje Pers_A, Personaje Pers_B, bool ver, string fase)
    {
        Personaje Ganador;
        int DProvocado = 0;
        bool win = false;

        Fase(fase, Pers_A, Pers_B, ver);
        Thread.Sleep(2300);

        while(Pers_A.Salud > 0 && Pers_B.Salud > 0)
        {
            if(ver)
            {
                Valores(Pers_A, Pers_B);
            }

            DProvocado = CPMove(Pers_A, Pers_A, ver);
            Pers_B.Salud -= DProvocado;
            Pers_B.Energia -= DProvocado/3;

            if (Pers_B.Salud > 0)
            {
                DProvocado = CPMove(Pers_B, Pers_A, ver);

                Pers_A.Salud -= DProvocado;
                Pers_A.Energia -= DProvocado/3;
            }

        }

        if (Pers_A.Salud <= 0)
        {
            Ganador = Pers_B;
        } else
            {
                Ganador = Pers_A;
                win = true;
            }

        if (ver)
        {
            Valores(Pers_A, Pers_B);
        }
        Console.WriteLine("\nEl ganador es: " + (Ganador.Nombre));
        Thread.Sleep(2000);
        return win;
    }

    static List<Personaje> ComFases (List<Personaje> Personajes, bool ver, string fase)
    {
        for(int i=1; i<Personajes.Count; i++)
        {
            if(ComvsCom(Personajes[i], Personajes[i+1], ver, fase))
            {
                Personajes.RemoveAt(i+1);
                RecSalud(Personajes[i]);
            } else
                {
                    Personajes.RemoveAt(i);
                    RecSalud(Personajes[i]);
                }
        }

        return Personajes;
    }

}