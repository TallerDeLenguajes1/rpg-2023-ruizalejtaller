using EspPersonaje;
using System.Net;
using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {
        var Datos = new PersonajesJson();
        var Personajes = new List<Personaje>();
        string[] Fases = {"Octavos de final", "Cuartos de final", "Semifinal", "Final"};
        string str;
        int op, fase = 0;
        int temp, fav = 6;
        bool cont = true, ver = true;

        temp = Get();

        Console.Clear();

        if (temp!=0)
        {
            if(temp<=15)
            {
                Favorece(temp,"fresco", "Ice");
                fav = 2;
            }

            if(temp>15 && temp <=25)
            {
                Favorece(temp, "templado", "Humana");
                fav = 4;
            }

            if(temp>25)
            {
                Favorece(temp, "caluroso", "Namek");
                fav = 1;
            }

        } else Console.WriteLine("No se pudo acceder al clima via api");

        Enter();

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
        } else
            {
                Personajes = Generar();
                Datos.GuardarPersonajes("Personajes.json", Personajes);
            }

        for(int i=0; i<Personajes.Count; i++)
        {
            if(Personajes[i].Tipo == (Razas)fav)
            {
                Personajes[i].SaludInicial += 20;
                Personajes[i].Salud +=20;
            }
        }

        Console.WriteLine("Personaje seleccionado");
        Console.WriteLine("----------------------");
        Personajes[0].Mostrar();

        Console.WriteLine("\nVer la informacion de los demas personajes ? (s/n)");
        str = Console.ReadLine();

        if (str.ToLower() == "s")
        {
            for(int i=1; i<Personajes.Count; i++)
            {
                Console.Clear();
                Console.WriteLine("Personaje N: " + (i+1));
                Console.WriteLine("----------------------");
                Personajes[i].Mostrar();
                Thread.Sleep(2000);
            }
        }

        while (fase<4 && cont)
        {
            Fase(Fases[fase], Personajes[0], Personajes[1], true);
            Enter();
        
            if(Combate(Personajes[0], Personajes[1], fase))
            {
                Personajes.RemoveAt(1);
                Personajes[0].Ganador(fase);
                cont = true;

                if (fase == 3)
                {
                    Console.WriteLine("\n////////////////////");
                    Console.WriteLine("//// " + Personajes[0].Nombre + " es el Campeon del Torneo de las Artes Marciales !!!");
                    Console.WriteLine("////////////////////");
                    Console.WriteLine("\n///////          Felicitaciones        ///////////");
                } else {
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
                                cont = true; ver = true;
                            break;

                            case 2:
                                cont = true; ver = false;
                            break;
                            
                            default:
                                cont = false;
                            break;
                        }
                    } else cont = false;
                }
            } else cont = false;

            if(cont && fase<4)
            {
                Console.WriteLine("\nFase: " + Fases[fase]);
                Console.WriteLine("----------------------");
                Personajes = ComFases(Personajes, ver, fase, Fases);
            }
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

    static void Favorece(int temp,string clima, string raza)
    {
        Console.WriteLine($"La temperatura es de {temp} grados...");
        Console.WriteLine($"Un clima {clima} que favorece a los luchadores de la raza {raza}\n");
        Console.WriteLine("El Torneo está a punto de comenzar...");
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

    static bool Combate(Personaje Pers_A, Personaje Pers_B, int fase)
    {
        Personaje Ganador;
        int DProvocado = 0;
        string str;
        int op;
        bool win = false, turno = false, transfor = false;

        if (Pers_A.Tipo!=(Razas)3 && Pers_A.Tipo!=(Razas)4)
        {
            transfor = true;
        }

        while (Pers_A.Salud > 0 && Pers_B.Salud > 0)
        {
            Valores(Pers_A, Pers_B);

            Console.WriteLine ($"\n\n\n1. Atacar   2. Semilla    3. Info   4. Info Rival   5. Rendirse  {(transfor? "6. Transformarse":"")}");
            str = Console.ReadLine();
            

            if (int.TryParse(str, out op))
            {
                switch (op)
                {
                    case 1:
                        DProvocado = Atacar(Pers_A, Pers_B, true);
                        Pers_B.Salud -= DProvocado;
                        Pers_B.Energia -= DProvocado/4;
                        if(Pers_A.Estado) Pers_A.Energia -= 50; 
                        if (Pers_A.Energia < 60 && Pers_A.Estado)
                        {
                            Pers_A.Transfor();
                            Console.WriteLine(Pers_A.Nombre + " pierde su transformacion por desgaste de energia");
                            Thread.Sleep(1000);
                        }
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
                        Console.Clear();
                        Pers_A.Mostrar();
                        Enter();
                    break;

                    case 4:
                        Console.Clear();
                        Pers_B.Mostrar();
                        Enter();
                    break;

                    case 5:
                        Console.WriteLine("\n" + Pers_A.Nombre + " se rinde. ");
                        Pers_A.Salud = 0;
                        Thread.Sleep(1500);
                    break;

                    case 6:
                        if (transfor)
                        {
                            Transformarse(Pers_A, true);
                            Thread.Sleep(1000);
                            turno = true;
                        }
                    break;
                }
            }

            if (Pers_B.Salud > 0 && turno)
            {
                DProvocado = CPMove(Pers_B, Pers_A, true, fase);

                Pers_A.Salud -= DProvocado;
                Pers_A.Energia -= DProvocado/4;

                turno = false;
            }



            
        }

        if (Pers_A.Salud <= 0)
        {
            Ganador = Pers_B;
        } else
            {
                Ganador = Pers_A;
                if(Pers_A.Estado) Pers_A.Transfor();
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

    static int CPMove (Personaje Pers_A, Personaje Pers_B, bool ver, int fase)
    {
        int DProvocado = 0;
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);
        int maxValue = ((100+(fase*20))-Pers_A.Salud);

        if (maxValue<1) maxValue = 2;

        int prob = rnd.Next(1, maxValue);
        int transf = rnd.Next(1, 100);
        bool turno = true;

        if (Pers_A.Tipo!=(Razas)3 && Pers_A.Tipo!=(Razas)4)
        {
            if (transf>20 && transf<80 && Pers_A.Energia > 70 && !Pers_A.Estado)
            {
                Transformarse(Pers_A, ver);
                turno = false;
            }
        }


        if (Pers_A.Semillas > 0 && prob > 50 && turno)
        {
            UsarSemilla(Pers_A, ver);
            turno = false;
        }

        if (turno)
        {
            DProvocado = Atacar(Pers_A, Pers_B, ver);
            if(Pers_A.Estado) Pers_A.Energia -= 50; 
            if (Pers_A.Energia < 60 && Pers_A.Estado)
            {
                Pers_A.Transfor();
                if (ver)
                {
                    Console.WriteLine(Pers_A.Nombre + " pierde su transformacion por desgaste de energia");
                    Thread.Sleep(1000);
                }
            }

        }

        return DProvocado;

    }

    static void UsarSemilla(Personaje Pers, bool ver) 
    {   
        if(ver)
        {
            Console.WriteLine("\n" + Pers.Nombre + " utiliza una semilla del ermitaño y recupera su salud por completo\n");
            Thread.Sleep(1500);
        }
        RecSalud(Pers);

        Pers.Semillas -= 1;
    }

    static void RecSalud(Personaje Pers)
    {
        Pers.Salud = 100;
        Pers.Energia = 100;
    }


    static bool ComvsCom(Personaje Pers_A, Personaje Pers_B, bool ver, int fase, string[] fases)
    {
        Personaje Ganador;
        int DProvocado = 0;
        bool win = false;

        Fase(fases[fase], Pers_A, Pers_B, ver);
        Thread.Sleep(2300);

        while(Pers_A.Salud > 0 && Pers_B.Salud > 0)
        {
            if(ver)
            {
                Valores(Pers_A, Pers_B);
            }

            DProvocado = CPMove(Pers_A, Pers_A, ver, fase);
            Pers_B.Salud -= DProvocado;
            Pers_B.Energia -= DProvocado/4;

            if (Pers_B.Salud > 0)
            {
                DProvocado = CPMove(Pers_B, Pers_A, ver, fase);

                Pers_A.Salud -= DProvocado;
                Pers_A.Energia -= DProvocado/4;
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

    static List<Personaje> ComFases (List<Personaje> Personajes, bool ver, int fase, string[] fases)
    {
        for(int i=1; i<Personajes.Count; i++)
        {
            if(ComvsCom(Personajes[i], Personajes[i+1], ver, fase, fases))
            {
                Personajes.RemoveAt(i+1);
                Personajes[i].Ganador(fase);
            } else
                {
                    Personajes.RemoveAt(i);
                    Personajes[i].Ganador(fase);
                }
        }

        return Personajes;
    }

    static void Transformarse(Personaje Pers, bool ver)
    {
        bool seguir = true;

        if(Pers.Energia > 70 && !Pers.Estado)
        {
            Pers.Transfor();
            if (ver)
            {
                Console.WriteLine("\n" + Pers.Nombre + " se transforma incrementando sus poderes...");
                Thread.Sleep(1500);
            }
            seguir = false;
        }

        if(Pers.Estado && seguir)
        {   
            Pers.Transfor();
            Console.WriteLine("\n" + Pers.Nombre + " vuelve a su estado base.");
        }
    }

    
    static int Get()
    {
        var url = $"https://api.open-meteo.com/v1/forecast?latitude=-26.81&longitude=-65.21&current_weather=true";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        var clima = JsonSerializer.Deserialize<Weather>(responseBody);

                        return (int)clima.current_weather.temperature;

                    } else return 0;
                }
            }
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    } 

}