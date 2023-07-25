namespace EspPersonaje;

public enum Razas
{
    Sayayin,
    Namekiano,
    Ice,
    Androide,
    Humano,
    Dios
}

class Sayayin
{
    private string[] nombres = {"Goku", "Vegeta", "Gohan", "Trunks", "Cabba"};
    private int[] anionac = {737, 732, 757, 766, 764};

    public string[] Nombres { get => nombres; }
    public int[] Anionac { get => anionac;}

}

class Namekiano
{
    private string[] nombres = {"Piccolo", "Nail", "Slug", "Saonel", "Pilina"};
    private int[] anionac = {753, 762, 261, 760, 758};

    public string[] Nombres { get => nombres; }
    public int[] Anionac { get => anionac;}
}

class Ice
{
    private string[] nombres = {"Freezer", "Cold", "Cooler", "Frost", "Kuriza"};
    private int[] anionac = {731, 650, 720, 735, 766};

    public string[] Nombres { get => nombres; }
    public int[] Anionac { get => anionac;}
}

class Androide
{
    private string[] nombres = {"Dr. Gero", "Numero 16", "Numero 17", "Numero 18", "Numero 19"};
    private int[] anionac = {697, 767, 746, 748, 767};

    public string[] Nombres { get => nombres; }
    public int[] Anionac { get => anionac;}
}

class Humano
{
    private string[] nombres = {"Ten Shin Han", "Krilin", "Chaoz", "Yamcha", "Roshi"};
    private int[] anionac = {733, 736, 738, 733, 430};

    public string[] Nombres { get => nombres; }
    public int[] Anionac { get => anionac;}
}

