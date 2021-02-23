using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscCSharp.Main
{
  public class RecordTypes
  {
    public RecordTypes()
    {
      var person = new Person { Name = "Test", Age = 31 };
      // With keyword, clone objects
      var secondPerson = person with { Age = 31 };

      var pet = new Pet("Buffy");
      Console.WriteLine($"pet.Name = {pet.Name}");

      var secondPet = pet with { Name = "Otter" };

      // compare records, comparing the properties
      // Not memory ref. like class
      Console.WriteLine($"person.Equals(secondPerson) = {person.Equals(secondPerson)}");
      Console.WriteLine($"person == secondPerson = {person == secondPerson}");

      Shape shape = new Shape("T");
      Shape circle = new Circle("S") { Circumference = 10 };
      // Compares every property, false
      Console.WriteLine($"shape == circle = {shape == circle}");
    }
  }

  public record Shape(string Name);

  public record Circle : Shape
  {
    public Circle(string name) : base(name) {}
    public int Circumference { get; init; }
  }

  // Record immutable
  // Creates a constructor with name prop
  // Makes the peroperty init only
  public record Pet(string Name);

  public record Person
  {
    public string Name { get; set; }
    public int Age { get; set; }
  }

}
