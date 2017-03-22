using System.IO;
using System.Text;
using System.Xml;

namespace XmlDataParse
{
    class Program
    {
        public static void ParseXml()
        {
            using (var myReader = XmlReader.Create(@"D:\poland-latest.osm"))
            {
                Stream xmlFile = new FileStream("D:\\Output.xml", FileMode.Create, FileAccess.Write);
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t"
                };

                var writer = XmlWriter.Create(xmlFile, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("nodes");
                var nodeData = new NodeData();
                var mainData = new MainData();

                while (myReader.Read())
                {
                    if (myReader.NodeType != XmlNodeType.Element) continue;

                    if (myReader.Name != "node"
                        && myReader.Name != "way") continue;

                    nodeData.Id = myReader.GetAttribute("id");
                    var longitudeAttr = myReader.GetAttribute("lon");
                    var latitudeAttr = myReader.GetAttribute("lat");
                    if (!string.IsNullOrEmpty(longitudeAttr))
                        nodeData.Lon = longitudeAttr;
                    if (!string.IsNullOrEmpty(latitudeAttr))
                        nodeData.Lat = latitudeAttr;

                    var node = myReader.ReadSubtree();
                    
                    while (node.Read())
                    {
                        if (node.NodeType != XmlNodeType.Element) continue;

                        var nodeValue = node.GetAttribute("v");
                        var nodeKind = node.GetAttribute("k");
                        switch (nodeKind)
                        {
                            case "name":
                                mainData.Name = nodeValue;
                                break;
                            case "railway":
                                switch (nodeValue)
                                {
                                    case "halt":
                                    case "station":
                                    case "tram_stop":
                                    case "crossing":
                                    case "level_crossing":
                                    case "signal":
                                        mainData.Type = nodeValue;
                                        mainData.Kind = nodeKind;
                                        break;
                                }
                                break;
                            case "highway":
                                switch (nodeValue)
                                {
                                    case "bus_stop":
                                    case "crossing":
                                    case "traffic_signals":
                                    case "speed_camera":
                                        mainData.Type = nodeValue;
                                        mainData.Kind = nodeKind;
                                        break;
                                }
                                break;
                            case "amenity":
                                switch (nodeValue)
                                {
                                    case "ferry_terminal":
                                    case "taxi":
                                    case "parking":
                                    case "fuel":
                                    case "bicycle_parking":
                                    case "car_wash":
                                        mainData.Type = nodeValue;
                                        mainData.Kind = nodeKind;
                                        break;
                                }
                                break;
                            case "aerialway":
                                if (nodeValue == "station")
                                {
                                    mainData.Type = nodeValue;
                                    mainData.Kind = nodeKind;
                                }
                                break;
                            case "aeroway":
                                if (nodeValue == "terminal")
                                {
                                    mainData.Type = nodeValue;
                                    mainData.Kind = nodeKind;
                                }
                                break;
                            case "shelter_type":
                                if (nodeValue == "public_transport")
                                {
                                    mainData.Type = nodeValue;
                                    mainData.Kind = nodeKind;
                                }
                                break;
                            case "junction":
                                if (nodeValue == "roundabout")
                                {
                                    mainData.Type = nodeValue;
                                    mainData.Kind = nodeKind;
                                }
                                break; 
                            case "bridge":
                                if (nodeValue == "yes")
                                {
                                    mainData.Type = nodeValue;
                                    mainData.Kind = nodeKind;
                                }
                                break; 
                        }
                    }
                    if (!string.IsNullOrEmpty(mainData.Type))
                    {
                        writer.WriteStartElement("node");
                        writer.WriteAttributeString("id", nodeData.Id);
                        writer.WriteAttributeString("lon", nodeData.Lon);
                        writer.WriteAttributeString("lat", nodeData.Lat);

                        writer.WriteStartElement("tag");
                        writer.WriteAttributeString("name", mainData.Name);
                        writer.WriteAttributeString("type", mainData.Type);
                        writer.WriteAttributeString("kind", mainData.Kind);

                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        
                        mainData.Name = string.Empty;
                        mainData.Type = string.Empty;
                    }
                }
                writer.WriteEndDocument();
                writer.Close();
                xmlFile.Close();
            }
        }
        
        static void Main()
        {
            ParseXml();
            PrepareCsv();
        }

        private static void PrepareCsv()
        {
            using (var reader = XmlReader.Create(@"D:\Output.xml"))
            {
                Stream csvFile = new FileStream("D:\\CsvOutput.csv", FileMode.Create, FileAccess.Write);
                var file = new StreamWriter(csvFile, Encoding.UTF8);
                string firstLine = $"id, lon, lat, name, type";
                
                file.WriteLine(firstLine);
                
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element
                        || reader.Name != "node") continue;

                    var nodeId = reader.GetAttribute("id");
                    var longitudeAttr = reader.GetAttribute("lon");
                    var latitudeAttr = reader.GetAttribute("lat");

                    string name = string.Empty;
                    string type = string.Empty;
                    string kind = string.Empty;

                    var node = reader.ReadSubtree();

                    while (node.Read())
                    {
                        if (reader.NodeType != XmlNodeType.Element
                           || reader.Name != "tag") continue;

                        name = node.GetAttribute("name");
                        type = node.GetAttribute("type");
                    }
                    if (type == "tram_stop")
                    {
                        string line = $"{nodeId}, {longitudeAttr}, {latitudeAttr},\"{name}\", {type}";

                        file.WriteLine(line);
                    }
                }
                file.Close();
            }
        }
    }
}
