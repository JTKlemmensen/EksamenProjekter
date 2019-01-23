using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Eksamenslignende_opgave___xml
{
    class Program
    {
        static void Main(string[] args)
        {

            PrintCars("CoolOutput.xml", LoadCars());
        }

        private static List<Car> LoadCars()
        {
            List<Car> cars = new List<Car>();

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            using (XmlReader reader = XmlReader.Create("http://www.fkj.dk/cars.xml", settings))
                while (reader.ReadToFollowing("car"))
                {
                    Car c = new Car();

                    c.Name = reader.GetAttribute("name");

                    if (reader.ReadToDescendant("cylinders"))
                        c.Cylinders = reader.ReadElementContentAsInt();

                    if (reader.ReadToNextSibling("country"))
                        c.Country = reader.ReadElementContentAsString();

                    cars.Add(c);
                }

            return cars;
        }

        private static void PrintCars(string path, List<Car> cars)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("cars");
                foreach (Car car in cars)
                {
                    writer.WriteStartElement("car");
                    writer.WriteAttributeString("name", car.Name);

                    writer.WriteElementString("cylinders", "" + car.Cylinders);
                    writer.WriteElementString("country", car.Country);

                    writer.WriteEndElement(); // </car>
                }
                writer.WriteEndElement(); // </cars>

                writer.WriteEndDocument();
            }
        }
    }
}