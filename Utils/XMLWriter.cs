using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using ModelLayer;

namespace Utils
{
    public class XMLWriter : XMLFileIOBase, IWriter
    {
        //public async Task WriteDummyData()
        //{
        //    var listOfCompanies = new List<Company>();

        //    var company = new Company()
        //    {
        //        Name = "Test Company1",
        //        Id = 1,
        //        City = "Test City1",
        //        Street = "Test Street1",
        //        Phone = "555-158 28 28"
        //    };
        //    company.Employees.Add(new Employee()
        //    {
        //        Id = 1,
        //        Name = "Test Person 1",
        //        Phone = "555-158 28 29",
        //        Mobile = "555-258 28 29",
        //        Email = "testperson1@fakemail.com",
        //        Position = "Test Position1"
        //    });
        //    company.Employees.Add(new Employee()
        //    {
        //        Id = 2,
        //        Name = "Test Person 2",
        //        Phone = "555-158 28 30",
        //        Mobile = "555-258 28 30",
        //        Email = "testperson2@fakemail.com",
        //        Position = "Test Position2"
        //    });
        //    company.Histories.Add(new HistoryPost()
        //    {
        //        Id = 1,
        //        Date = new DateTime(2016, 05, 12),
        //        Post = "History post 1"
        //    });
        //    company.Histories.Add(new HistoryPost()
        //    {
        //        Id = 2,
        //        Date = new DateTime(2016, 05, 13),
        //        Post = "History post 2"
        //    });
        //    company.Todos.Add(new Todo()
        //    {
        //        Id = 1,
        //        Date = new DateTime(2016, 05, 14),
        //        Description = "Todo1"
        //    });
        //    company.Todos.Add(new Todo()
        //    {
        //        Id = 2,
        //        Date = new DateTime(2016, 05, 15),
        //        Description = "Todo2"
        //    });

        //    listOfCompanies.Add(company);

        //    company = new Company()
        //    {
        //        Name = "Test Company2",
        //        Id = 2,
        //        City = "Test City2",
        //        Street = "Test Street2",
        //        Phone = "555-158 28 28"
        //    };
        //    company.Employees.Add(new Employee()
        //    {
        //        Id = 1,
        //        Name = "Test Person 3",
        //        Phone = "555-158 28 32",
        //        Mobile = "555-258 28 32",
        //        Email = "testperson3@fakemail.com",
        //        Position = "Test Position3"
        //    });
        //    company.Employees.Add(new Employee()
        //    {
        //        Id = 2,
        //        Name = "Test Person 4",
        //        Phone = "555-158 28 33",
        //        Mobile = "555-258 28 33",
        //        Email = "testperson4@fakemail.com",
        //        Position = "Test Position4"
        //    });
        //    company.Histories.Add(new HistoryPost()
        //    {
        //        Id = 1,
        //        Date = new DateTime(2016, 05, 12),
        //        Post = "History post 3"
        //    });
        //    company.Histories.Add(new HistoryPost()
        //    {
        //        Id = 2,
        //        Date = new DateTime(2016, 05, 13),
        //        Post = "History post 4"
        //    });
        //    company.Todos.Add(new Todo()
        //    {
        //        Id = 1,
        //        Date = new DateTime(2016, 05, 14),
        //        Description = "Todo3"
        //    });
        //    company.Todos.Add(new Todo()
        //    {
        //        Id = 2,
        //        Date = new DateTime(2016, 05, 15),
        //        Description = "Todo4"
        //    });

        //    listOfCompanies.Add(company);

        //    StorageFolder folder = ApplicationData.Current.LocalFolder;
        //    var xDoc = new XDocument(
        //        new XDeclaration("1.0", "utf-16", "true"),
        //        new XElement("Companies",
        //            listOfCompanies.Select(type =>
        //                new XElement("Company",
        //                    new XElement("Id", type.Id),
        //                    new XElement("Name", type.Name),
        //                    new XElement("Phone", type.Phone),
        //                    new XElement("City", type.City),
        //                    new XElement("Street", type.Street),
        //                    new XElement("Todos", type.Todos.Select(todo =>
        //                        new XElement("Todo",
        //                            new XElement("Id", todo.Id),
        //                            new XElement("Date", todo.Date),
        //                            new XElement("Description", todo.Description)))),
        //                    new XElement("Histories", type.Histories.Select(history =>
        //                        new XElement("HistoryPost",
        //                            new XElement("Id", history.Id),
        //                            new XElement("Date", history.Date),
        //                            new XElement("Post", history.Post)))),
        //                    new XElement("Employees", type.Employees.Select(employee =>
        //                        new XElement("Employee",
        //                            new XElement("Id", employee.Id),
        //                            new XElement("Name", employee.Name),
        //                            new XElement("Phone", employee.Phone),
        //                            new XElement("Mobile", employee.Mobile),
        //                            new XElement("Email", employee.Email),
        //                            new XElement("Position", employee.Position))))))));

        //    StorageFile file = await folder.CreateFileAsync("Data.xml", CreationCollisionOption.ReplaceExisting);
        //    Stream stream = await file.OpenStreamForWriteAsync();
        //    using (stream)
        //    {
        //        xDoc.Save(stream);
        //    }
        //}

        public async Task<int> SaveEntity(IEntity entity, int companyId)
        {
            int idToUse;

            if (entity.Id == 0)
                idToUse = await GetLowestAwailibleEntityId(entity, companyId);
            else
                idToUse = entity.Id;

            StorageFile file = await GetFilePath();
            Stream stream = await file.OpenStreamForWriteAsync();
            XDocument doc = XDocument.Load(stream);
            XElement element;
            var type = entity.GetType().ToString().Split('.');
            var comp = doc.Descendants().Elements("Company").FirstOrDefault(c => c.Element("Id").Value == companyId.ToString());

            element = new XElement(new XElement(type[type.Length - 1]));
            var elements = entity.GetType().GetProperties();
            element.Add(new XElement("Id", idToUse));
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Name == "Id")
                    continue;
                element.Add(new XElement(elements[i].Name, elements[i].GetValue(entity, null)));
            }

            string listType ="";
            switch (type[type.Length-1])
            {
                case "Employee":
                    listType = "Employees";
                    break;
                case "HistoryPost":
                    listType = "Histories";
                    break;
                case "Todo":
                    listType = "Todos";
                    break;
            }

            using (stream)
            {
                if (entity.Id == 0)
                {
                    comp.Elements(listType).FirstOrDefault().Add(element);
                }
                else
                {
                    comp.Descendants().Elements(type[type.Length - 1]).FirstOrDefault(x => x.Elements("Id").Any(e => e.Value == entity.Id.ToString())).ReplaceWith(element);
                }
                stream.SetLength(0);
                doc.Save(stream);
            }

            
            return idToUse;
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
            else
                companyElement.Add(new XElement("Todos"));
            
            if (company.Histories != null)
            {
                companyElement.Add(new XElement("Histories", company.Histories.Select(history =>
                                new XElement("HistoryPost",
                                    new XElement("Id", history.Id),
                                    new XElement("Date", history.Date),
                                    new XElement("Post", history.Post)))));
            }
            else
                companyElement.Add(new XElement("Histories"));

            if (company.Employees != null)
            {
                companyElement.Add(new XElement("Employees", company.Employees.Select(employee =>
                            new XElement("Employee",
                                new XElement("Id", employee.Id),
                                new XElement("Name", employee.Name),
                                new XElement("Phone", employee.Phone),
                                new XElement("Mobile", employee.Mobile),
                                new XElement("Email", employee.Email),
                                new XElement("Position", employee.Position)))));
            }
            else
                companyElement.Add(new XElement("Employees"));

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

        private async Task<int> GetLowestAwailibleEntityId(IEntity entity, int companyId)
        {
            StorageFile file = await GetFilePath();
            Stream stream = await file.OpenStreamForReadAsync();
            XDocument doc;
            var type = entity.GetType().ToString().Split('.');

            using (stream)
            {
                doc = XDocument.Load(stream);
            }

            var comp = doc.Descendants("Companies")
                        .Elements("Company")
                        .FirstOrDefault(x => x.Elements("Id").Any(e => e.Value == companyId.ToString()));

            var elements = comp.Descendants().Elements(type[type.Length - 1]).Elements("Id");

            int lowestIdAwailible = 0;
            foreach (var element in elements)
            {
                if (Int32.Parse(element.Value) > lowestIdAwailible)
                    lowestIdAwailible = Int32.Parse(element.Value);
            }
            

            //var postToControl = company.Descendants()

            //foreach (var xElement in company)
            //{
            //    if (int.Parse(xElement.Element("Id").Value) == companyId)
            //    {
            //        var listOfPosts = xElement.Descendants();
            //        foreach (var element in listOfPosts)
            //        {
            //            if(element.)
            //        }
            //    }
            //        lowestIdAwailible = int.Parse(xElement.Element("Id").Value);
            //}

            return lowestIdAwailible + 1;
        }

    }
}
