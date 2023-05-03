function obtenerCoordenadas(ubicacion, kiosko, color) {
    var api_key = '30f23ffc0b1248bc8a8f7efeff2a1542';
    var address = ubicacion;

    var api_url = 'https://api.opencagedata.com/geocode/v1/json'

    var request_url = api_url
        + '?'
        + 'key=' + api_key
        + '&q=' + encodeURIComponent(address)
        + '&language=es'
        + '&pretty=1'
        + '&countrycode=co'


    // see full list of required and optional parameters:
    // https://opencagedata.com/api#forward

    var request = new XMLHttpRequest();
    request.open('GET', request_url, true);

    request.onload = function () {

        if (request.status === 200) {
            // Success!
            var data = JSON.parse(request.responseText);
            var respuesta = data.results[0].geometry.lat + ',' + data.results[0].geometry.lng;
            console.log(respuesta);

        } else if (request.status <= 500) {
            // We reached our target server, but it returned an error

            console.log("unable to geocode! Response code: " + request.status);
            var data = JSON.parse(request.responseText);
            console.log('error msg: ' + data.status.message);
            alert("unable to geocode! Response code: " + request.status);
        } else {
            console.log("server error");
            respuesta = error;
        }
        var ubic = respuesta;

        return addMarker(ubicacion, ubic, kiosko, color)
    };

    request.onerror = function () {
        // There was a connection error of some sort
        console.log("unable to connect to server");
    };

    request.send();  // make the request


}

var map = L.map('map').setView([4.639386, -74.082412], 12);
L.tileLayer('https://{s}.tile.jawg.io/jawg-light/{z}/{x}/{y}{r}.png?access-token={accessToken}', {
    attribution: '<a href="http://jawg.io" title="Tiles Courtesy of Jawg Maps" target="_blank">&copy; <b>Jawg</b>Maps</a> &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    minZoom: 0,
    maxZoom: 22,
    subdomains: 'abcd',
    accessToken: 'f2wjW93lqtp6qwhrCPeYXyuDJpRhokq91YvSzfS0XqrZF2jUHra8LUJBraPBH2Cv'
}).addTo(map);

function addMarker(coordenadas, ubic, kiosko, color) {

    var splited = ubic.split(',');
    var lat = splited[0];
    var lng = splited[1];

    var greenIcon = new L.Icon({
        iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    //var yellowIcon = new L.Icon({
    //    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-gold.png',
    //    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
    //    iconSize: [25, 41],
    //    iconAnchor: [12, 41],
    //    popupAnchor: [1, -34],
    //    shadowSize: [41, 41]
    //});

    var redIcon = new L.Icon({
        iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-red.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
    });

    if (color == 0) {
        L.marker([lat, lng], { icon: greenIcon, zIndexOffset: 1 }).addTo(map).bindPopup("Cajero:" + kiosko + "<br>" + coordenadas);
    }
    else if (color == 1) {
        L.marker([lat, lng], { icon: yellowIcon, zIndexOffset: 1 }).addTo(map).bindPopup("Cajero:" + kiosko + "<br>" + coordenadas);
    }
    else if (color == 2) {
        L.marker([lat, lng], { icon: redIcon, zIndexOffset: 1 }).addTo(map).bindPopup("Cajero:" + kiosko + "<br>" + coordenadas);
    }
    else {
        L.marker([lat, lng], { zIndexOffset: 1 }).addTo(map).bindPopup("Cajero:" + kiosko + "<br>" + coordenadas);
    }
}

var button = document.getElementById("btnMapa");
button.onclick = function () {
    window.scrollTo(0, 990);

    return false;
}

var btnSubir = document.getElementById("btnSubir");
btnSubir.onclick = function () {
    window.scrollTo(0, 0);

    return false;
}