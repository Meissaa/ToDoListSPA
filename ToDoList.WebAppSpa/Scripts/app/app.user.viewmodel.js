
var userViewModel = function () {

    var self = this;
    self.UserName = ko.observable();
    self.Password = ko.observable();
    self.Token = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.EmailAddress = ko.observable();
    self.Register = function () {

        location.hash = 'register';
        ClearDetailsUser();
    }
    self.RegisterUser = function () {

        var username = self.UserName();
        var password = self.Password();
        var firstname = self.FirstName();
        var lastname = self.LastName();
        var emailaddress = self.EmailAddress();

        apiClient.Register(username, password, firstname, lastname, emailaddress).done(function (result) {
            if (result.Data == null) {
                alert("Account not created");
            }
            else {
                location.hash = '#/';
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    self.Error = function () {
        toastr.error('Invalid username or password. Please try again');
    }

    DoLogin = function () {

        var username = self.UserName();
        var password = self.Password();

        apiClient.login(username, password).done(function (result) {
            if (result.SessionToken == null) {
                self.Error();
            }
            else {
                location.hash = 'todolist';
                self.Token(result.SessionToken);
                GetLists();
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    Logout = function () {

        apiClient.Logout().done(function (result) {
            if (result.StatusCode == 200) {
                location.hash = '#/';
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    ClearDetailsUser = function () {

        self.UserName('');
        self.Password('');
    }
};
