<script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>

    (g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]")?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })({
        key: "AIzaSyBWgAm8E5i4XXP7Qq-OLjc9h4WjOTtzEsw",
        v: "weekly",
        l: "places"
    });
<script async
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBWgAm8E5i4XXP7Qq-OLjc9h4WjOTtzEsw&libraries=geometry,places&callback=initMap&callback=initAutocomplete">
    </script>
    //Scrolls to map
    function scrollToMap() {
        const map = document.getElementById('map');
        map.scrollIntoView();
    }
    

    let map;
    async function initMap() {
        //Create Map
        const { Map } = await google.maps.importLibrary("maps");
        map = new Map(document.getElementById("map"), {
            center: { lat: 42.713552, lng: 25.115340 },
            zoom: 4,
            mapTypeControl: false,
            streetViewControl: false,
            fullscreenControl: false,
            zoomControl: false
        });

        //Place markers for all locations
        var marker;
    @foreach (var item in Model)
    {
        <text>
                   marker = new google.maps.Marker({
                    map: map,
                    position: { lat: @item.Latitude, lng: @item.Longitude },
                    title: '@item.Name'
                });
            var infowindow = new google.maps.InfoWindow();
            google.maps.event.addListener(marker, "click", () => {
                infowindow.setContent(`<div id="info-window">
                                                                                                                            <h2 class="ui header">@item.Name</h2>
                                                                                                                            <p>@item.Address</p>
                                                                                                                            <p>Hourly Rate: @item.HourlyRate$</p>
                                                                                                                            <p>Empty spots: @item.AvailableSpaces</p>
                @if (item.IsNonStop || (DateTime.Now.TimeOfDay >= item.OpeningHour.ToTimeSpan() && DateTime.Now.TimeOfDay <= item.ClosingHour.ToTimeSpan()))
                {
                                                                                                                                    <p class="open">Open</p>
                }
                else
                {
                    TimeSpan ts = new TimeSpan(24, 0, 0);
                    TimeSpan timeUntil = ts - DateTime.Now.TimeOfDay + item.OpeningHour.ToTimeSpan();
                    string output = timeUntil.Hours > 1 ? $"Closed (Opens in {@timeUntil.ToString(@"hh")} hours)" : $"Closed (Opens in 1 hour)";
                                                                                                                                    <p class="closed">@output</p>

                }
                                </div>`);
                infowindow.open(map, marker);
                infowindow.setPosition({ lat: @item.Latitude, lng: @item.Longitude});
            });

        </text>
    }
    }
    const locationButton = document.getElementById("near-me");
    async function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                async (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,

                    };
                    locationButton.classList.add(pos.lat);
                    locationButton.classList.add(pos.lng);
                    map.zoom = 9;
                    map.setCenter(pos);
                    await fillLocationList();
                    locationButton.classList = "";
                    document.getElementById('location').value = "";
                    removeEventListener('click', locationButton);
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                },
            );

        } else {
            handleLocationError(false, infoWindow, map.getCenter());
        }
        function handleLocationError(browserHasGeolocation, infoWindow, pos) {
            infoWindow.setPosition(pos);
            infoWindow.setContent(
                browserHasGeolocation
                    ? "Error: The Geolocation service failed."
                    : "Error: Your browser doesn't support geolocation.",
            );
            infoWindow.open(map);
        }
    }
    async function initAutocomplete()
    {
        
    
        //Get current location and zoom
        
        //Error handling
        
        

        //Search bar
        var marker=new google.maps.Marker();
        const locationInput = document.getElementById('location');
        const searchBox = new google.maps.places.SearchBox(locationInput);
        google.maps.event.addListener(searchBox, 'places_changed', function () {
            searchBox.set('map', null);

            var places = searchBox.getPlaces();

            var bounds = new google.maps.LatLngBounds();
            var i, place;
            for (i = 0; place = places[i]; i++) {
                (function (place) {
                    marker.bindTo('map', searchBox, 'map');
                    google.maps.event.addListener(marker, 'map_changed', function () {
                        if (!this.getMap()) {
                            this.unbindAll();
                        }
                    });
                    bounds.extend(place.geometry.location);


                }(place));

            }
            fillLocationList();
            map.fitBounds(bounds);
            searchBox.set('map', map);
            map.setZoom(Math.min(map.getZoom(), 9));
            scrollToMap();
        });

        google.maps.event.addDomListener(window, 'load', initMap); const input = document.getElementById("pac-input");

    }

    async function fillLocationList() {
        const { encoding } = await google.maps.importLibrary("geometry")
        const list = document.getElementById('location-list');
        list.innerHTML = "";

    @{
        foreach (var parking in Model)
        {

            <text>
                var parkingJson = @Json.Serialize(parking);
                geocoder = new google.maps.Geocoder();
                var nearMeButton=document.getElementById('near-me');
                var locationLat;
                var locationLng;
                var targetLatLng;
                var address = document.getElementById('location').value;
                var distance;
                var parkingLatLng = new google.maps.LatLng(parkingJson.latitude, parkingJson.longitude);

                if(nearMeButton.classList!="")
                {
                    targetLatLng = new google.maps.LatLng(nearMeButton.classList[0],nearMeButton.classList[1])
                }
                else
                {
                    targetLatLng= await getLatLng();
                }
                try{
                   
                    var distance = google.maps.geometry.spherical.computeDistanceBetween(targetLatLng, parkingLatLng) / 1000;
                    if (40 > distance) {


                        list.innerHTML += `
                                                                                        <div id="card-container">
                                                                                                        <h2 class="ui header">@parking.Name</h2>
                                                                                                                                                                                                                    <p>@parking.Address</p>
                                                                                                                                                                                                                    <p>Hourly Rate: @parking.HourlyRate$ | Empty spots: @parking.AvailableSpaces</p>
                    @if (parking.IsNonStop || (DateTime.Now.TimeOfDay >= parking.OpeningHour.ToTimeSpan() && DateTime.Now.TimeOfDay <= parking.ClosingHour.ToTimeSpan()))
                    {
                                        <p class="open">Open</p>
                    }
                    else
                    {
                        TimeSpan ts = new TimeSpan(24, 0, 0);
                        TimeSpan timeUntil = ts - DateTime.Now.TimeOfDay + parking.OpeningHour.ToTimeSpan();
                        string output = timeUntil.Hours > 9 ? $"Closed (Opens in {@timeUntil.ToString(@"hh")} hours)" : $"Closed (Opens in {timeUntil.ToString(@"hh")[1]} hours)";
                        output = timeUntil.Hours <= 1 ? $"Closed (Opens in 1 hour)" : output;
                                                        <p class="closed">@output</p>
                    }
                                             <hr>
                                     </div>
                                 `;
                    }
                    
                }
                catch(error)
                {
                    list.innerHTML = `<h2 id="no-locations">Invalid location.</h2>`
                }
                
            </text>

        }
    }
    if(list.innerHTML=="")
    {
        console.log(true);
            list.innerHTML = `<h2 id="no-locations">No nearby locations found.</h2>`
    }
        async function getLatLng() {
            try {
                var response = await fetch(`https://maps.googleapis.com/maps/api/geocode/json?address=${address}&key=AIzaSyBWgAm8E5i4XXP7Qq-OLjc9h4WjOTtzEsw`);
                var data = await response.json();
                console.log("request called");
                return new google.maps.LatLng(data.results[0].geometry.location.lat, data.results[0].geometry.location.lng);
            }
            catch (error) {
                console.log(error);
            }
        }
}        
