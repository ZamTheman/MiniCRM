using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using NoBSCRM.Models;

namespace NoBSCRM.Utils
{
    public class XMLWriter : IWriter
    {
        private Todo CreateTodo(string date, string description)
        {
            string[] tempDate = date.Split('-');
            return new Todo() {Date = new DateTime(int.Parse(tempDate[0]), int.Parse(tempDate[1]), int.Parse(tempDate[2])), Description = description};
        }

        public async Task WriteDummyData()
        {
            var listOfCompanies = new List<Company>();

            var company = new Company()
            {
                Name = "Test Company1",
                Id = 1,
                City = "Test City1",
                Street = "Test Street1",
                Phone = "555-158 28 28"
            };
            company.Employees.Add(new Employee()
            {
                Name = "Test Person 1",
                Phone = "555-158 28 29",
                Mobil = "555-258 28 29",
                EMail = "testperson1@fakemail.com",
                Position = "Test Position1"
            });
            company.Employees.Add(new Employee()
            {
                Name = "Test Person 2",
                Phone = "555-158 28 30",
                Mobil = "555-258 28 30",
                EMail = "testperson2@fakemail.com",
                Position = "Test Position2"
            });
            company.Histories.Add(new HistoryPost()
            {
                Date = new DateTime(2016-05-12),
                Post = "History post 1"
            });
            company.Histories.Add(new HistoryPost()
            {
                Date = new DateTime(2016-05-13),
                Post = "History post 2"
            });
            company.Todos.Add(new Todo()
            {
                Date = new DateTime(2016,05,14),
                Description = "Todo1"
            });
            company.Todos.Add(new Todo()
            {
                Date = new DateTime(2016,05,15),
                Description = "Todo2"
            });

            listOfCompanies.Add(company);

            company = new Company()
            {
                Name = "Test Company2",
                Id = 1,
                City = "Test City2",
                Street = "Test Street2",
                Phone = "555-158 28 28"
            };
            company.Employees.Add(new Employee()
            {
                Name = "Test Person 3",
                Phone = "555-158 28 32",
                Mobil = "555-258 28 32",
                EMail = "testperson3@fakemail.com",
                Position = "Test Position3"
            });
            company.Employees.Add(new Employee()
            {
                Name = "Test Person 4",
                Phone = "555-158 28 33",
                Mobil = "555-258 28 33",
                EMail = "testperson4@fakemail.com",
                Position = "Test Position4"
            });
            company.Histories.Add(new HistoryPost()
            {
                Date = new DateTime(2016-05-12),
                Post = "History post 3"
            });
            company.Histories.Add(new HistoryPost()
            {
                Date = new DateTime(2016-05-13),
                Post = "History post 4"
            });
            company.Todos.Add(new Todo()
            {
                Date = new DateTime(2016,05,14),
                Description = "Todo3"
            });
            company.Todos.Add(new Todo()
            {
                Date = new DateTime(2016,05,15),
                Description = "Todo4"
            });

            listOfCompanies.Add(company);

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-16", "true"),
                new XProcessingInstruction("test", "value"),
                new XElement("Companies",
                    listOfCompanies.Select(type =>
                    new XElement("Company",
                    new XElement("Id", type.Id),
                    new XElement("Name", type.Name),
                    new XElement("Phone", type.Phone),
                    new XElement("City", type.City),
                    new XElement("Street", type.Street),
                    new XElement("Todos", type.Todos.Select(todo =>
                        new XElement("Todo",
                            new XElement("Date", todo.Date),
                            new XElement("Description", todo.Description)))),
                    new XElement("Histories", type.Histories.Select(history =>
                        new XElement("History",
                            new XElement("Date", history.Date),
                            new XElement("Post", history.Post)))),
                    new XElement("Employees", type.Employees.Select(employee =>
                        new XElement("Employee",
                            new XElement("Name", employee.Name),
                            new XElement("Phone", employee.Phone),
                            new XElement("Mobile", employee.Mobil),
                            new XElement("Email", employee.EMail),
                            new XElement("Position", employee.Position))))))));

            StorageFile file = await folder.CreateFileAsync("Data.xml", CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();
            using (stream)
            {
                xDoc.Save(stream);
            }
        }

        public Task SaveSingle()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSingleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSingleById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
