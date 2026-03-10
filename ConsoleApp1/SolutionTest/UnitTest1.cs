using ConsoleApp1;

namespace SolutionTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Solve_AtLeastOneItemFits_ReturnsAtLeastOneItem()
    {
        // Arrange
        var problem = new Problem(10, 1);
        var capacity = 100; // Ekstremalnie duży plecak, na pewno coś wejdzie

        // Act
        var result = problem.Solve(capacity);

        // Assert
        Assert.IsTrue(result.Id.Count > 0, "Plecak nie powinien być pusty.");
    }

    [TestMethod]
    public void Solve_NoItemFits_ReturnsEmptySolution()
    {
        // Arrange
        var problem = new Problem(10, 1);
        var capacity = 0; // Limit 0 - nic nie ma prawa wejść (wagi losują się od 1 do 10)

        // Act
        var result = problem.Solve(capacity);

        // Assert
        Assert.AreEqual(0, result.Id.Count);
        Assert.AreEqual(0, result.Value);
        Assert.AreEqual(0, result.Weight);
    }

    [TestMethod]
    public void Solve_SpecificInstance_ReturnsCorrectExpectedResult()
    {
        // Arrange (Używamy instancji, którą testowaliśmy już w konsoli i znamy jej twardy wynik)
        var problem = new Problem(15, 232);
        var capacity = 23;

        // Act
        var result = problem.Solve(capacity);

        // Assert
        Assert.AreEqual(43, result.Value); // Spodziewana wartość: 43
        Assert.AreEqual(23, result.Weight); // Spodziewana waga: 23
    }

    [TestMethod]
    public void Solve_RandomInstance_WeightDoesNotExceedCapacity()
    {
        // Arrange
        var problem = new Problem(50, 123); // Duża liczba przedmiotów
        var capacity = 45;

        // Act
        var result = problem.Solve(capacity);

        // Assert
        Assert.IsTrue(result.Weight <= capacity, "Waga zapakowanych przedmiotów przekroczyła limit!");
    }

    [TestMethod]
    public void Solve_ZeroItemsInProblem_ReturnsEmptySolution()
    {
        // Arrange
        var problem = new Problem(0, 1); // Generujemy problem bez żadnych przedmiotów
        var capacity = 50;

        // Act
        var result = problem.Solve(capacity);

        // Assert
        Assert.AreEqual(0, result.Id.Count);
        Assert.AreEqual(0, result.Value);
        Assert.AreEqual(0, result.Weight);
    }
}