using System;
using System.Collections.Generic;
using NUnit.Framework;

public class HeroRepositoryTests
{
    [Test]
    public void HeroRepositoryInitializesReadOnlyCollection()
    {
        var heroRepository = new HeroRepository();
        IReadOnlyCollection<Hero> heroes = new List<Hero>();


        CollectionAssert.AreEqual(heroRepository.Heroes, heroes);
    }

    [Test]
    public void CreateMethodWithValidHeroShoudAddItToHeroesCollection()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);

        heroRepository.Create(validHero);

        Assert.AreEqual(1, heroRepository.Heroes.Count);
    }

    [Test]
    public void CreateMethodWithValidHeroShouldReturnCorectString()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);

        string expectedString = "Successfully added hero Valid with level 56";
        string actualString = heroRepository.Create(validHero);

        Assert.AreEqual(expectedString, actualString);
    }

    [Test]
    public void CreateMethodWithNullHeroShouldThrowArgumentNullException()
    {
        Hero nullHero = null;
        var heroRepository = new HeroRepository();

        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRepository.Create(nullHero);
        }, "Hero is null");
    }

    [Test]
    public void CreateMethodWithExistingHeroShouldThrowInvalidOperationException()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);
        var validHero2 = new Hero("Valid", 70);

        heroRepository.Create(validHero);

        Assert.Throws<InvalidOperationException>(() =>
        {
            heroRepository.Create(validHero2);
        }, "Hero with name Valid already exists");
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public void RemoveMethodWithNullOrWhiteSpaceNameShouldThrowArgumentNullException(string name)
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);
        heroRepository.Create(validHero);

        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRepository.Remove(name);
        }, "Name cannot be null");

    }

    [Test]
    public void RemoveMethodWithValidExistingHeroNameShouldReturnTrue()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);
        var validHero2 = new Hero("Valid2", 70);

        heroRepository.Create(validHero);
        heroRepository.Create(validHero2);

        bool actualBool = heroRepository.Remove("Valid");

        Assert.IsTrue(actualBool);
    }

    [Test]
    public void RemoveMethodWithValidHeroNameAndInexistingHeroShouldReturnFalse()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);
        var validHero2 = new Hero("Valid2", 70);

        heroRepository.Create(validHero);

        bool actualBool = heroRepository.Remove("Valid2");

        Assert.IsFalse(actualBool);
    }

    [Test]
    public void GetHeroWithHighestLevel()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);
        var validHero2 = new Hero("Valid2", 70);
        var validHero3 = new Hero("Valid3", 71);
        var expectedHero = new Hero("Valid4", 72);

        heroRepository.Create(validHero);
        heroRepository.Create(validHero2);
        heroRepository.Create(validHero3);
        heroRepository.Create(expectedHero);

        var actualHero = heroRepository.GetHeroWithHighestLevel();

        Assert.AreEqual(expectedHero, actualHero);
    }

    [Test]
    public void GetHeroByName()
    {
        var heroRepository = new HeroRepository();
        var validHero = new Hero("Valid", 56);
        var validHero2 = new Hero("Valid2", 70);
        var validHero3 = new Hero("Valid3", 71);
        var expectedHero = new Hero("Valid4", 72);

        heroRepository.Create(validHero);
        heroRepository.Create(validHero2);
        heroRepository.Create(validHero3);
        heroRepository.Create(expectedHero);

        var actualHero = heroRepository.GetHero("Valid4");

        Assert.AreEqual(expectedHero, actualHero);
    }
}