printjson( db.transport.find({ $and: [{ "properties.type": "tram_stop"}, { "geometry": {$near: {$geometry: {type: "Point", coordinates: [18.573107, 54.395897]}}}}]}).limit(10).toArray() )
