// Define the routes
crossroads.addRoute('/', function() {
    $('#routeContent').load('ToDoListItem.html');
});
crossroads.addRoute('/user/{userId}', function(userId) {
    $('#routeContent').load('user/details.html');
});
crossroads.bypassed.add(function(request) {
    console.error(request + ' seems to be a dead end...');
});

//Listen to hash changes
window.addEventListener("hashchange", function() {
    var route = '/';
    var hash = window.location.hash;
    if (hash.length > 0) {
        route = hash.split('#').pop();
    }
    crossroads.parse(route);
});

// trigger hashchange on first page load
window.dispatchEvent(new CustomEvent("hashchange"));