namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        // Testing constructor with valid data
        [Test]
        public void ConstructorTestWithNoParametersData()
        {
            Database personDatabase = new Database();

            FieldInfo colectionField = personDatabase.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fi => fi.Name == "persons");

            var privateFieldData = (Person[])colectionField.GetValue(personDatabase);

            // Assert
            Assert.AreEqual(16, privateFieldData.Length);
        }

        [Test]
        public void CountParameterGetter()
        {
            // Arrange
            Database database = new Database(new Person(1, "Petur"), new Person(2, "Gosho"));

            // Assert
            Assert.AreEqual(2, database.Count);
        }

        [Test]
        public void AddMethodWithValidData()
        {
            // Arrange
            var pesho = new Person(1, "Pesho");
            var gosho = new Person(2, "Gosho");
            var gergana = new Person(3, "Gergana");

            Database database = new Database(pesho, gosho);

            // Act
            database.Add(gergana);

            FieldInfo colectionField = database.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fi => fi.Name == "persons");

            var privateFieldData = (Person[])colectionField.GetValue(database);

            // Assert
            Assert.AreEqual(3, database.Count);

            Assert.AreEqual(gergana, privateFieldData[database.Count - 1]);
        }

        [Test]
        public void AddMethodWithOverTheMaxCapacity()
        {
            // Arrange
            var data = new Person[16];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Person(i + 1, $"Person {i + 1}");
            }

            Database database = new Database(data);


            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(new Person(17, "Georgi"));
            }, "Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void AddMethodAddingNewPersonWithNameThatExists()
        {
            // Arrange
            var georgi = new Person(1, "Georgi");
            var georgiAgain = new Person(2, "Georgi");

            Database database = new Database(georgi);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(georgiAgain);
            }, "There is already user with this username!");
        }

        [Test]
        public void AddMethodAddingNewPersonWithIdThatExists()
        {
            // Arrange
            var georgi = new Person(1, "Georgi");
            var pesho = new Person(1, "Pesho");

            Database database = new Database(georgi);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(pesho);
            }, "There is already user with this Id!");
        }

        [Test]
        public void AddRangeAddingOverTheMaxCapacityCollection()
        {
            // Arrange
            var data = new Person[17];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Person(i + 1, $"Person {i + 1}");
            }

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
               Database database = new Database(data);
            }, "Provided data length should be in range [0..16]!");
        }

        [Test]
        public void CollectionCountOnElementRemoveWithElements()
        {
            // Arrange
            var pesho = new Person(1, "Pesho");
            var gosho = new Person(2, "Gosho");
            var gergana = new Person(3, "Gergana");

            Database database = new Database(pesho, gosho, gergana);

            FieldInfo colectionField = database.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fi => fi.Name == "persons");

            var privateFieldData = (Person[])colectionField.GetValue(database);

            // Act
            database.Remove();

            
            int actualCount = database.Count;
            int expectedCount = 2;

            Person actualData = privateFieldData[privateFieldData.Length - 1];
            Person expectedData = null;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(expectedData, actualData);
        }

        [Test]
        public void RemoveElementFromEmptyCollection()
        {
            // Arrange
            Database database = new Database();

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
            });
        }

        [Test]
        public void FindUserByNameWithValidData()
        {
            // Arrange
            Person georgi = new Person(1, "Georgi");
            Database database = new Database(georgi);

            // Act
            Person actualPerson = database.FindByUsername("Georgi");
            Person expextedPerson = georgi;

            // Assert
            Assert.AreEqual(expextedPerson, actualPerson);
        }

        [TestCase(null)]
        [TestCase("")]
        public void FindUserByNameWithNullOrEmpty(string name)
        {
            // Arrange
            Person person = new Person(1, "Georgi");
            Database database = new Database(person);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                database.FindByUsername(name);
            }, "Username parameter is null!");
        }

        [Test]
        public void FindUserByNameWithNoSuchUser()
        {
            // Arrange
            Person person = new Person(1, "Georgi");
            Database database = new Database(person);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindByUsername("Dont Exist");
            }, "No user is present by this username!");
        }

        [Test]
        public void FindUserByIdWithValidData()
        {
            // Arrange
            Person georgi = new Person(1, "Georgi");
            Database database = new Database(georgi);

            // Act
            Person actualPerson = database.FindById(1);
            Person expextedPerson = georgi;

            // Assert
            Assert.AreEqual(expextedPerson, actualPerson);
        }

        [Test]
        public void FindUserByIdWithNegativeNumber()
        {
            // Arrange
            Person person = new Person(1, "Georgi");
            Database database = new Database(person);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                database.FindById(-1);
            }, "Id should be a positive number!");
        }

        [Test]
        public void FindUserByIdWithNoSuchUser()
        {
            // Arrange
            Person person = new Person(1, "Georgi");
            Database database = new Database(person);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindById(5);
            }, "No user is present by this ID!");
        }
    }
}