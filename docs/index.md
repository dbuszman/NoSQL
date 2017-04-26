
Technologie NoSQL
================
Dariusz Buszman

## Transport releated data

> Dane z których korzystam pochodzą z [OpenStreetMap](openstreetmap.org), ze zbioru wszystkich danych z Polski -> [Źródło](http://download.geofabrik.de/europe/poland-latest.osm.bz2).

>Powyższy zbiór danych został poddany preprocesingowi, po to aby ograniczyć ilość informacji do porządanych przeze mnie. W tym celu utworzyłem aplikację konsolową, której odpowiedzialnością jest wybranie interesujących danych oraz ich ustandaryzowanie. Aplikacja ta w pierwszej kolejności tworzy plik *Output.xml*, który zawiera zbiór dancyh związanych z wybranym przeze mnie tematem, a następnie korzystając z tego pliku, przygotowuje plik *CsvOutput.csv*, który zawiera dane na temat przystanków tramwajowych w Polsce.

>Plik *CsvOutput.csv* jest wykorzysywany przeze mnie do utworzenia danych GeoJSON.

## Lista zadań:
------------

1.  [Zadanie GEO](geo.nb) - prezentowanie zbioru przystanków tramwajowych w Polsce (na bazie openstreetmap.org)
