using System;
using System.IO;
using System.Xml;

namespace PrepareXML
{
    public class MergeXmlFiles
    {
        private Stream _xmlFile;
        private XmlWriter _writer;
        private int _counter;

        public MergeXmlFiles()
        {
            _xmlFile = new FileStream(@"D:\\example_data.xml", FileMode.Create, FileAccess.Write);
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t"
            };

            _writer = XmlWriter.Create(_xmlFile, settings);
            
        }

        public void Merge()
        {
            _writer.WriteStartDocument();
            _writer.WriteStartElement(@"nodes");
            AppendData(@"D:\\XML_OSM_nodes\\railway_tram_stop.osm");
            AppendData(@"D:\\XML_OSM_nodes\\railway_station.osm");
            AppendData(@"D:\\XML_OSM_nodes\\railway_halt.osm");
            AppendData(@"D:\\XML_OSM_nodes\\shelter_type_public_transport.osm");
            AppendData(@"D:\\XML_OSM_nodes\\highway_speed_camera.osm");
            AppendData(@"D:\\XML_OSM_nodes\\highway_bus_stop.osm");
            AppendData(@"D:\\XML_OSM_nodes\\highway_footway.osm");
            AppendData(@"D:\\XML_OSM_nodes\\bridge_yes.osm");
            AppendData(@"D:\\XML_OSM_nodes\\amenity_taxi.osm");
            AppendData(@"D:\\XML_OSM_nodes\\amenity_parking.osm");
            AppendData(@"D:\\XML_OSM_nodes\\amenity_fuel.osm");
            AppendData(@"D:\\XML_OSM_nodes\\amenity_ferry_terminal.osm");
            AppendData(@"D:\\XML_OSM_nodes\\amenity_car_wash.osm");
            AppendData(@"D:\\XML_OSM_nodes\\amenity_bicycle_parking.osm");
            AppendData(@"D:\\XML_OSM_nodes\\aeroway_terminal.osm");
            AppendData(@"D:\\XML_OSM_nodes\\aerialway_station.osm");
            _writer.WriteEndElement();
            _writer.WriteEndDocument();
            _writer.Close();
            _xmlFile.Close();

            Console.WriteLine($"{_counter}");
            Console.ReadKey();
        }

        public void AppendData(string filePath)
        {
            using (var myReader = XmlReader.Create(filePath))
            {
                while (myReader.Read())
                {
                    if (myReader.NodeType != XmlNodeType.Element) continue;

                    if (myReader.Name != @"node") continue;

                    _counter++;
                    _writer.WriteNode(myReader, true);
                }
            }
        }
    }
}
