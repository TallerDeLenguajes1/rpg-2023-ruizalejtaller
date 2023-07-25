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

        Console.WriteLine($"{Personajes[0].Nombre}");
        Console.WriteLine($"{Personajes[10].Nombre}");
        Console.WriteLine($"{Personajes[15].Nombre}");

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



}