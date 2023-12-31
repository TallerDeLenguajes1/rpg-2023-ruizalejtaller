namespace EspPersonaje;

class FabricaDePersonajes
{
    public static Personaje Crear()
    {
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);
        int dado = rnd.Next(0,5);
        int dado2 = rnd.Next(0,5);

        return (Armar(dado, dado2));
    }

    public static Personaje Armar (int dado, int dado2)
    {
        var seed = Environment.TickCount;
        Random rnd = new Random(seed);
        var Personaje = new Personaje();
        Personaje.Tipo = (Razas)dado;
        switch (dado)
        {
            case 0:
                var Sayayin = new Sayayin();
                Personaje.Nombre = Sayayin.Nombres[dado2];
                Personaje.Velocidad = rnd.Next(6,10);
                Personaje.Destreza = rnd.Next(7,9);
                Personaje.Fuerza = rnd.Next(6,9);
                Personaje.Nivel = rnd.Next(5,9);
                Personaje.Armadura = rnd.Next(7,9);
                Personaje.Nac = Sayayin.Anionac[dado2];
                Personaje.Edad = (773 - Sayayin.Anionac[dado2]);

            break;
                
            case 1:
                var Namekiano = new Namekiano();
                Personaje.Nombre = Namekiano.Nombres[dado2];
                Personaje.Velocidad = rnd.Next(6,9);
                Personaje.Destreza = rnd.Next(6,9);
                Personaje.Fuerza = rnd.Next(6,10);
                Personaje.Nivel = rnd.Next(6,9);
                Personaje.Armadura = rnd.Next(7,10);
                Personaje.Nac = Namekiano.Anionac[dado2];
                Personaje.Edad = (773 - Namekiano.Anionac[dado2]);
            break;
                
            case 2:
                var Ice = new Ice();
                Personaje.Nombre = Ice.Nombres[dado2];
                Personaje.Velocidad = rnd.Next(7,11);
                Personaje.Destreza = rnd.Next(6,10);
                Personaje.Fuerza = rnd.Next(7,10);
                Personaje.Nivel = rnd.Next(6,9);
                Personaje.Armadura = rnd.Next(7,11);
                Personaje.Nac = Ice.Anionac[dado2];
                Personaje.Edad = (773 - Ice.Anionac[dado2]);
            break;

            case 3:
                var Androide = new Androide();
                Personaje.Nombre = Androide.Nombres[dado2];
                Personaje.Velocidad = rnd.Next(6,11);
                Personaje.Destreza = rnd.Next(6,10);
                Personaje.Fuerza = rnd.Next(6,9);
                Personaje.Nivel = rnd.Next(6,9);
                Personaje.Armadura = rnd.Next(7,11);
                Personaje.Nac = Androide.Anionac[dado2];
                Personaje.Edad = (773 - Androide.Anionac[dado2]);
            break;

            case 4:
                var Humano = new Humano();
                Personaje.Nombre = Humano.Nombres[dado2];
                Personaje.Velocidad = rnd.Next(5,9);
                Personaje.Destreza = rnd.Next(6,9);
                Personaje.Fuerza = rnd.Next(6,9);
                Personaje.Nivel = rnd.Next(6,9);
                Personaje.Armadura = rnd.Next(7,9);
                Personaje.Nac = Humano.Anionac[dado2];
                Personaje.Edad = (773 - Humano.Anionac[dado2]);
            break;
        }
        Personaje.SaludInicial = 100;
        Personaje.Salud = 100;
        Personaje.Energia = 100;
        Personaje.Semillas = rnd.Next(1,3);
        return Personaje;
    }

}