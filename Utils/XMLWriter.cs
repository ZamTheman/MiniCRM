using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using ModelLayer;

namespace Utils
{
    public class XMLWriter : XMLFileIOBase, IWriter
    {
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
                Id = 1,
                Name = "Test Person 1",
                Phone = "555-158 28 29",
                Mobil = "555-258 28 29",
                EMail = "testperson1@fakemail.com",
                Position = "Test Position1"
            });
            company.Employees.Add(new Employee()
            {
                Id = 2,
                Name = "Test Person 2",
                Phone = "555-158 28 30",
                Mobil = "555-258 28 30",
                EMail = "testperson2@fakemail.com",
                Position = "Test Position2"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 1,
                Date = new DateTime(2016, 05, 12),
                Post = "History post 1"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 2,
                Date = new DateTime(2016, 05, 13),
                Post = "History post 2"
            });
            company.Todos.Add(new Todo()
            {
                Id = 1,
                Date = new DateTime(2016, 05, 14),
                Description = "Todo1"
            });
            company.Todos.Add(new Todo()
            {
                Id = 2,
                Date = new DateTime(2016, 05, 15),
                Description = "Todo2"
            });

            listOfCompanies.Add(company);

            company = new Company()
            {
                Name = "Test Company2",
                Id = 2,
                City = "Test City2",
                Street = "Test Street2",
                Phone = "555-158 28 28"
            };
            company.Employees.Add(new Employee()
            {
                Id = 1,
                Name = "Test Person 3",
                Phone = "555-158 28 32",
                Mobil = "555-258 28 32",
                EMail = "testperson3@fakemail.com",
                Position = "Test Position3"
            });
            company.Employees.Add(new Employee()
            {
                Id = 2,
                Name = "Test Person 4",
                Phone = "555-158 28 33",
                Mobil = "555-258 28 33",
                EMail = "testperson4@fakemail.com",
                Position = "Test Position4"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 1,
                Date = new DateTime(2016, 05, 12),
                Post = "History post 3"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 2,
                Date = new DateTime(2016, 05, 13),
                Post = "History post 4"
            });
            company.Todos.Add(new Todo()
            {
                Id = 1,
                Date = new DateTime(2016, 05, 14),
                Description = "Todo3"
            });
            company.Todos.Add(new Todo()
            {
                Id = 2,
                Date = new DateTime(2016, 05, 15),
                Description = "Todo4"
            });

            listOfCompanies.Add(company);

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-16", "true"),
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
                                    new XElement("Id", todo.Id),
                                    new XElement("Date", todo.Date),
                                    new XElement("Description", todo.Description)))),
                            new XElement("Histories", type.Histories.Select(history =>
                                new XElement("HistoryPost",
                                    new XElement("Id", history.Id),
                                    new XElement("Date", history.Date),
                                    new XElement("Post", history.Post)))),
                            new XElement("Employees", type.Employees.Select(employee =>
                                new XElement("Employee",
                                    new XElement("Id", employee.Id),
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

        public async Task DeleteSingleCompanyByIdAsync(int id)
        {
            StorageFile file = await GetFilePath();
            Stream stream = await file.OpenStreamForWriteAsync();
            XDocument doc = XDocument.Load(stream);
            
            using (stream)
            {
                doc.Descendants("Companies").Elements("Company").FirstOrDefault(x => x.Elements("Id").Any(e => e.Value == id.ToString())).Remove();
                stream.SetLength(0);
                doc.Save(stream);
            }
        }

        public async Task DeleteSingleEntityByIdAsync(int id, IEntity entity)
        {
            StorageFile file = await GetFilePath();
            Stream stream = await file.OpenStreamForWriteAsync();
            XDocument doc = XDocument.Load(stream);
            string[] typeName = entity.ToString().Split('.');

            using (stream)
            {
                var comp = doc.Descendants("Companies")
                    .Elements("Company").FirstOrDefault(x => x.Elements("Id")
                    .Any(e => e.Value == id.ToString()));
                comp.Descendants(typeName[typeName.Length - 1])
                    .Where(x => x.Elements("Id").Any(e => e.Value == entity.Id.ToString()))
                    .Remove();

                stream.SetLength(0);
                doc.Save(stream);
            }
        }

        public Task UpdateSingleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveCompany(ICompany company)
        {
            var lowestAvailbeId = 0;
            var idToUse = company.Id;
            if (company.Id == 0)
            {
                lowestAvailbeId = await GetLowestAwailibleId();
                idToUse = lowestAvailbeId;
            }

            StorageFile file = await GetFilePath();
            Stream stream = await file.OpenStreamForWriteAsync();
            XDocument doc = XDocument.Load(stream);

            XElement companyElement = new XElement("Company",
                new XElement("Id", idToUse),
                new XElement("Name", company.Name),
                new XElement("Phone", company.Phone),
                new XElement("City", company.City),
                new XElement("Street", company.Street));

            if (company.Todos != null)
            {
                companyElement.Add(new XElement("Todos", company.Todos.Select(todo =>
                               new XElement("Todo",
                                   new XElement("Id", todo.Id),
                                   new XElement("Date", todo.Date),
                                   new XElement("Description", todo.Description)))));
            }

            if (company.Histories != null)
            {
                companyElement.Add(new XElement("Histories", company.Histories.Select(history =>
                                new XElement("HistoryPost",
                                    new XElement("Id", history.Id),
                                    new XElement("Date", history.Date),
                                    new XElement("Post", history.Post)))));
            }

            if (company.Employees != null)
            {
                companyElement.Add(new XElement("Employees", company.Employees.Select(employee =>
                            new XElement("Employee",
                                new XElement("Id", employee.Id),
                                new XElement("Name", employee.Name),
                                new XElement("Phone", employee.Phone),
                                new XElement("Mobile", employee.Mobil),
                                new XElement("Email", employee.EMail),
                                new XElement("Position", employee.Position)))));
            }
            
            using (stream)
            {
                if (company.Id == 0)
                {
                    doc.Root.Add(companyElement);
                    stream.SetLength(0);
                    doc.Save(stream);
                }
                else
                {
                    doc.Descendants("Companies").Elements("Company").FirstOrDefault(x => x.Elements("Id").Any(e => e.Value == company.Id.ToString())).ReplaceWith(companyElement);
                    stream.SetLength(0);
                    doc.Save(stream);
                }
            }
            return lowestAvailbeId;
        }

        private async Task<int> GetLowestAwailibleId()
        {
            StorageFile file = await GetFilePath();
            Stream stream = await file.OpenStreamForReadAsync();
            XDocument doc;

            using (stream)
            {
                doc = XDocument.Load(stream);
            }

            var companies = doc.Descendants("Company");
            int lowestIdAwailible = 0;

            foreach (var xElement in companies)
            {
                if (int.Parse(xElement.Element("Id").Value) > lowestIdAwailible)
                    lowestIdAwailible = int.Parse(xElement.Element("Id").Value);
            }

            return lowestIdAwailible + 1;
        }


    }
}
