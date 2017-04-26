printjson( db.transport.find( { $and: [{ "properties.type": "tram_stop"}, { "geometry" :
                  { $geoWithin :
                    { $geometry :
                      { type : "Polygon" ,
                        coordinates : [ [
                                          [ 16.4240 , 54.5008 ] ,
                                          [ 19.3855 , 54.5008 ] ,
                                          [ 19.3855 , 53.2927 ] ,
                                          [ 16.4240 , 54.5008 ]
                                        ] ]
                } } } } ]}).toArray() )
