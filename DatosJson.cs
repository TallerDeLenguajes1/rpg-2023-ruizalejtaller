using EspPersonaje;
using System.Text.Json;

public class PersonajesJson
{

    public void GuardarPersonajes(string NombreArchivo, List<Personaje> Personajes)
    {
        string jsonString = JsonSerializer.Serialize(Personajes);

        try
        {
                using(StreamWriter archivo = new StreamWriter(NombreArchivo))
                {
                    archivo.WriteLine(jsonString);
                    
                    archivo.Close();
                }
                        
        }   catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
   
    }

    public List<Personaje> LeerPersonajes(string name)
    {
        var ListaPersonajes = new List<Personaje>();
            try
            {
                using (StreamReader archivo = new StreamReader(name))
                {
                    string objson = archivo.ReadToEnd();
                    ListaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(objson);
                    archivo.Close();
                }

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ListaPersonajes;
    }

    public bool Existe (string name)
    {
        return File.Exists(name) ? true : false;
    }

}
