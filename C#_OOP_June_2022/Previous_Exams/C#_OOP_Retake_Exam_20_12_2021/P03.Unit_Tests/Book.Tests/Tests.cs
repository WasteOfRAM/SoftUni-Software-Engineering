namespace Book.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using NUnit.Framework;

    public class Tests
    {
        [Test]
        public void BookConstructorCorectlySettsPropValues()
        {
            var expectedBookName = "Book";
            var expectedAuthorName = "Author";
            var expectedDictionary = new Dictionary<int, string>();

            var book = new Book(expectedBookName, expectedAuthorName);
            FieldInfo dictionaryFild = typeof(Book).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(fn => fn.Name == "footnote");
            var actualDictionary = (Dictionary<int, string>)dictionaryFild.GetValue(book);

            Assert.That(expectedBookName, Is.EqualTo(book.BookName));
            Assert.That(expectedAuthorName, Is.EqualTo(book.Author));
            CollectionAssert.AreEqual(expectedDictionary, actualDictionary);
        }

        [Test]
        public void FootNoteCountReturnsTheCountCorectly()
        {
            var book = new Book("Book", "Author");

            book.AddFootnote(2, "note 2");

            Assert.That(1, Is.EqualTo(book.FootnoteCount));
        }

        [Test]
        public void BookNameGetterReturnsTheCorectBookName()
        {
            var expectedBookName = "Book";
            var book = new Book(expectedBookName, "Author");

            Assert.That(expectedBookName, Is.EqualTo(book.BookName));
        }

        [TestCase(null)]
        [TestCase("")]
        public void BookNameSetterThrowsExceptionWithInvalidValue(string bookName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book(bookName, "Author");
            }, "Invalid BookName!");
        }

        [Test]
        public void AuthorNameGetterReturnsTheCorectAuthorName()
        {
            var expectedAuthorName = "Author";
            var book = new Book("Book", expectedAuthorName);

            Assert.That(expectedAuthorName, Is.EqualTo(book.Author));
        }

        [TestCase(null)]
        [TestCase("")]
        public void AuthorSetterThrowsExceptionWithInvalidValue(string author)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("Bool", author);
            }, "Invalid Author!");
        }

        [Test]
        public void AddingNewFootNoteShoudIncrimentCount()
        {
            var book = new Book("Book", "Author");
            book.AddFootnote(1, "Text");

            Assert.That(1, Is.EqualTo(book.FootnoteCount));
        }

        [Test]
        public void AddingFootNoteWithExistingNumberShouldThrowException()
        {
            var book = new Book("Book", "Author");
            book.AddFootnote(1, "Text");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(1, "Text2");
            }, "Footnote already exists!");
        }

        [Test]
        public void FindingFootNoteWithExistingNumberReturnsCorectString()
        {
            var book = new Book("Book", "Author");
            book.AddFootnote(1, "Text");
            book.AddFootnote(2, "Text2");

            var expectedString = "Footnote #1: Text";
            var actualString = book.FindFootnote(1);

            Assert.That(expectedString, Is.EqualTo(actualString));
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(1000)]
        public void FindFootNoteWithInExistingNumberThrowsException(int noteNumber)
        {
            var book = new Book("Book", "Author");
            book.AddFootnote(1, "Text");
            book.AddFootnote(2, "Text2");
            book.AddFootnote(3, "Text3");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(noteNumber);
            }, "Footnote doesn't exists!");
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(1000)]
        public void AlterFootNoteThatDontExistThrowsException(int noteNumber)
        {
            var book = new Book("Book", "Author");
            book.AddFootnote(1, "Text");
            book.AddFootnote(2, "Text2");
            book.AddFootnote(3, "Text3");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AlterFootnote(noteNumber, "Invalid");
            }, "Footnote does not exists!");
        }

        [Test]
        public void AlteringFootNoteChangesTheNoteText()
        {
            var book = new Book("Book", "Author");
            book.AddFootnote(1, "Text");
            book.AddFootnote(2, "Text2");

            book.AlterFootnote(1, "Valid Text");

            var expectedString = "Footnote #1: Valid Text";
            var actualString = book.FindFootnote(1);

            Assert.That(expectedString, Is.EqualTo(actualString));
        }
    }
}