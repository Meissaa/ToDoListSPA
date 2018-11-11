var basePathPages = '../Templates/';

var app = $.sammy(function () {

    this.use(Sammy.OAuth2);
    this.authorize = "#/oauth/login";

    function loadView(context, page, element, viewModel) {
        context.swap('');
        context.$element().load(page, function () {
            ko.applyBindings(viewModel, $(element)[0]);
        });
    }

    this.get('#/', function (context) {
        loadView(context, basePathPages + 'Login.html', '#divLogin', userViewModel);
    });

    this.get('todolist', function (context) {

        if (app.getAccessToken() != "") {
            loadView(context, basePathPages + 'ToDoListItems.html', '#divGetLists', toDoListItemViewModel);
            
        }
        else {
            this.requireOAuth();
        }
    });

    this.get('register', function (context) {
        loadView(context, basePathPages + 'Register.html', '#divRegister', userViewModel);
     
    });

    this.get('todolist/create', function (context) {

        if (app.getAccessToken() != "") {
            loadView(context, basePathPages + 'CreateList.html', '#divCreateList', toDoListItemViewModel);
        }
        else {
            this.requireOAuth();
        }
    });

    this.get('todolist/edit', function (context) {
        if (app.getAccessToken() != "") {
            loadView(context, basePathPages + 'EditList.html', '#divEditLists', toDoListItemViewModel);
        }
        else {
            this.requireOAuth();
        }
    });

    this.get('todolist/task', function (context) {
        if (app.getAccessToken() != "") {
            loadView(context, basePathPages + 'ToDoListTasks.html', '#divGetTasks', toDoListTaskViewModel);
        }
        else {
            this.requireOAuth();
        }
    });

    this.get('todolist/task/create', function (context) {
        if (app.getAccessToken() != "") {
            loadView(context, basePathPages + 'CreateTask.html', '#divCreateItemTask', toDoListTaskViewModel);
        }
        else {
            this.requireOAuth();
        }
    });

    this.get('todolist/task/edit', function (context) {
        if (app.getAccessToken() != "") {
            loadView(context, basePathPages + 'EditTask.html', '#divEditTask', toDoListTaskViewModel);
        }
        else {
            this.requireOAuth();
        }
    });

    this.get("#/oauth/login", function (context) {
        this.redirect("#/");
    });
})

$(document).ready(function () {
    app.run("#/");
});