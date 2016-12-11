using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Foundation;
using ModelLayer;

namespace Utils
{
    public class XMLReader : IReader
    {
        public async Task<Company> GetSingleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Company>> GetAll()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync("Data.xml");
            Stream stream = await file.OpenStreamForReadAsync();
            XDocument xDoc;
            
            using (stream)
            {
                xDoc = XDocument.Load(stream);
            }

            var companies = xDoc.Descendants("Company");
            var listToReturn = new List<Company>();

            foreach (var xElement in companies)
            {
                var todos = new List<Todo>();
                var histories = new List<HistoryPost>();
                var employees = new List<Employee>();

                foreach(var todo in xElement.Descendants("Todo"))
                {
                    string[] temdDateAsString = todo.Element("Date").Value.Split('-');
                    todos.Add(new Todo() {
                        Id = int.Parse(todo.Element("Id").Value),
                        Date = new DateTime(int.Parse(temdDateAsString[0]), int.Parse(temdDateAsString[1]), int.Parse(temdDateAsString[2].Substring(0,2))),
                        Description = todo.Element("Description").Value });
                    }

                foreach (var history in xElement.Descendants("HistoryPost"))
                {
                    string[] temdDateAsString = history.Element("Date").Value.Split('-');
                    histories.Add(new HistoryPost()
                    {
                        Id = int.Parse(history.Element("Id").Value),
                        Date = new DateTime(int.Parse(temdDateAsString[0]), int.Parse(temdDateAsString[1]), int.Parse(temdDateAsString[2].Substring(0,2))),
                        Post = history.Element("Post").Value
                    });
                }

                foreach (var employee in xElement.Descendants("Employee"))
                {
                    employees.Add(new Employee()
                    {
                        Id = int.Parse(employee.Element("Id").Value),
                        Name = employee.Element("Name").Value,
                        Phone = employee.Element("Phone").Value,
                        Mobil = employee.Element("Mobile").Value,
                        EMail = employee.Element("Email").Value,
                        Position = employee.Element("Position").Value
                    });
                }

                var company = new Company()
                {
                    Id = int.Parse(xElement.Element("Id").Value),
                    Name = xElement.Element("Name").Value,
                    Phone = xElement.Element("Phone").Value,
                    City = xElement.Element("City").Value,
                    Street = xElement.Element("Street").Value
                };

                company.Todos.AddRange(todos);
                company.Histories.AddRange(histories);
                company.Employees.AddRange(employees);

                listToReturn.Add(company);

            }

            return listToReturn;
        }
    }
}
