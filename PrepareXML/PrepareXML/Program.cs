using System.IO;
using System.Text;
using System.Xml;

namespace PrepareXML
{
    class Program
    {
        public static void ParseOSM_Nodes(string sourceFilePath, string outputFilePath,
            string elementKind, string elementType)
        {
            using (var myReader = XmlReader.Create(sourceFilePath))
            {
                Stream xmlFile = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t"
                };

                var writer = XmlWriter.Create(xmlFile, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement(@"nodes");

                var nodeData = new NodeData();
                var mainData = new MainData { Kind = elementKind, Type = elementType };

                while (myReader.Read())
                {
                    if (myReader.NodeType != XmlNodeType.Element) continue;

                    if (myReader.Name != @"node") continue;

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

                        if (nodeKind == "name")
                            mainData.Name = nodeValue;
                    }

                    writer.WriteStartElement(@"node");

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

                }
                writer.WriteEndDocument();
                writer.Close();
                xmlFile.Close();
            }
        }

        private static void PrepareCsv()
        {
            using (var reader = XmlReader.Create("D:\\example_data.xml"))
            {
                Stream csvFile = new FileStream("D:\\csv_example_data.csv", FileMode.Create, FileAccess.Write);
                var file = new StreamWriter(csvFile, Encoding.UTF8);
                string firstLine = $"id\tlon\tlat\tname\ttype\tkind";

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
                        kind = node.GetAttribute("kind");
                    }
                    string line = $"{nodeId}\t{longitudeAttr}\t{latitudeAttr}\t{name}\t{type}\t{kind}";

                    file.WriteLine(line);
                }
                file.Close();
            }
        }

        static void Main(string[] args)
        {
            var inputFilePath = args[0];
            var outputFilePath = args[1];
            var lookUpForKind = args[2];
            var lookUpForType = args[3];

            ParseOSM_Nodes(inputFilePath, outputFilePath,
                    lookUpForKind, lookUpForType);
                    
            /*ParseOSM_Nodes(@"D:\\highway_footway.osm", @"D:\\XML_OSM_nodes\\highway_footway.osm",
                    @"highway", @"footway");*/

            var mergeFiles = new MergeXmlFiles();

            mergeFiles.Merge();

            PrepareCsv();
        }
    }
}
