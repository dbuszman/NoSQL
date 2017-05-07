
Technologie NoSQL
================
Dariusz Buszman

## Transport releated data

> Dane z których korzystam pochodzą z [OpenStreetMap](openstreetmap.org), ze zbioru wszystkich danych z Polski -> [Źródło](http://download.geofabrik.de/europe/poland-latest.osm.bz2).

>Powyższy zbiór danych został poddany preprocesingowi, po to aby ograniczyć ilość informacji do pożądanych przeze mnie. W tym celu utworzyłem aplikację konsolową, której odpowiedzialnością jest wybranie interesujących danych oraz ich ustandaryzowanie. Aplikacja ta w pierwszej kolejności tworzy pliki odpowiednie dla danych kategorii, a następnie dokonuje ich scalenie do pliku *example_data.xml* który zawiera zbiór dancyh związanych z wybranym przeze mnie tematem, a następnie korzystając z tego pliku, przygotowuje plik *csv_example_data.csv*, który jest odwzorowaniem przeprzedniego pliku w formacie CSV.

>Plik *csv_example_data* jest wykorzysywany przeze mnie do utworzenia danych GeoJSON.

## Lista zadań:
------------

1.  [Zadanie GEO](geo.nb.html) - prezentowanie zbioru przystanków tramwajowych i autobusowych w Polsce (na bazie openstreetmap.org)
