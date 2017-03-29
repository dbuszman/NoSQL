printjson( db.tramStops.find({ "geometry": {$near: {$geometry: {type: "Point", coordinates: [18.573107, 54.395897]}}}}, {_id: 0}).limit(10).toArray() )
