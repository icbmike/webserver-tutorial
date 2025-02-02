using Server.ActionResults;
using Server.Routing;

namespace App.Controllers;

public class PetsController
{
    private static readonly List<Pet> Pets = [new Pet("Freddie", "Dog"), new Pet("Todd", "Cat")];


    [HttpGet("pets")]
    public List<Pet> ListPets()
    {
        return Pets;
    }

    [HttpPost("pets")]
    public IActionResult CreatePet(string name, string species)
    {
        Pets.Add(new Pet(name, species));

        return new OkResult();
    }
}

public record Pet(string Name, string Species);