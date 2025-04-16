using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;

namespace TestingLayer
{
    public class FieldsContextTests
    {
        static FieldsContext fieldsContext;
        static FieldsContextTests()
        {
            fieldsContext = new FieldsContext(Tests.dbContext);
        }

        [Test]
        public void CreateGenre()
        {
            // Arrange
            Field field = new Field(1000,"Economics");
            int fieldsBefore = Tests.dbContext.Fields.Count();

            // Act
            fieldsContext.Create(field);

            // Assert
            int fieldsAfter = Tests.dbContext.Fields.Count();
            Field lastField = Tests.dbContext.Fields.Last();
            Assert.That(fieldsBefore + 1 == fieldsAfter && lastField.Name == field.Name,
                "Names are not equal or field is not created!");
        }

        [Test]
        public void ReadField()
        {
            Field newField = new Field(1000,"Economics");
            fieldsContext.Create(newField);

            Field field = fieldsContext.Read(1000);

            Assert.That(field.Name == "Economics", "Read() does not get Field by id!");
        }

        [Test]
        public void ReadAllFields()
        {
            int fieldsBefore = Tests.dbContext.Fields.Count();

            int fieldsAfter = fieldsContext.ReadAll().Count;

            Assert.That(fieldsBefore == fieldsAfter, "ReadAll() does not return all of the Fields!");
        }


        [Test]
        public void UpdateField()
        {
            Field newField= new Field(1000,"Economics");
            fieldsContext.Create(newField);

            Field lastField = fieldsContext.ReadAll().Last(); 
            lastField.Name = "Updated Field";

            fieldsContext.Update(lastField, false);

            Assert.That(fieldsContext.Read(lastField.Id).Name == "Updated Field",
            "Update() does not change the Field's name!");
        }

        [Test]
        
        public void DeleteField()
        {
            Field newField = new Field(1000,"Economics");
            fieldsContext.Create(newField);

            Field field= fieldsContext.ReadAll().Last();
            int fieldId = field.Id;

            fieldsContext.Delete(fieldId);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => fieldsContext.Read(fieldId));
            Assert.That(ex.Message, Is.EqualTo($"Field with id = {fieldId} does not exist!"));
        }
    }
}
