var token = "";
var base_url = "http://localhost:49981/api/v1/";

function sendajaxRequest(httpMethod, url, reqData) {
    var d = $.Deferred()
    $.ajax({
        type: httpMethod,
        url: base_url + url,
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(reqData),
        headers: { 'Authorization': 'Custom ' + token }
    }).done(function (result) {
        d.resolve(result);
    }).fail(function (error) {
        d.reject(error);
    });
    return d.promise();
};

var apiClient = {

    login: function (username, password) {
        var d = $.Deferred();
        var user = { Username: username, Password: password }
        $.ajax({
            type: "POST",
            url: base_url + 'auth/login',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(user)
        }).done(function (result) {
            token = result.SessionToken;
            d.resolve(result);
        }).fail(function (error) {
            d.reject(error);
        });
        return d.promise();
    },

    GetAllLists: function GetAllLists() {
        return sendajaxRequest("GET", 'todolist', "");
    },

    GetDetailsItem: function (listId) {
        return sendajaxRequest("GET", 'todolist/' + listId, "")

    },

    CreateList: function (name, createDate) {
        var reqData = { Name: name, CreateDate: createDate }
        return sendajaxRequest("POST", '/todolist', reqData)
    },

    EditList: function (listid, name, createDate) {
        var reqData = { Id: listid, Name: name, CreateDate: createDate }
        return sendajaxRequest("PUT", '/todolist', reqData)
    },

    RemoveList: function (listId) {
        return sendajaxRequest("DELETE", '/todolist/' + listId, "")
    },

    CreateTask: function (listId, text, iscompleted, createDate, completeDate) {
        var reqData = { Text: text, IsCompleted: iscompleted, CreateDate: createDate, CompleteDate: completeDate }
        return sendajaxRequest("POST", 'todolist/' + listId + '/task', reqData)
    },

    RemoveTask: function (listId, taskId) {
        return sendajaxRequest("DELETE", 'todolist/' + listId + '/task/' + taskId, "")
    },

    EditTask: function (listId, taskId, text, iscompleted, createDate, completeDate) {
        var reqData = { Id: taskId, Text: text, IsCompleted: iscompleted, CreateDate: createDate, CompleteDate: completeDate }
        return sendajaxRequest("PUT", 'todolist/' + listId + '/task/', reqData)
    },

    Logout: function () {
        return sendajaxRequest("POST", 'auth/logout', "")
    },

    Register: function (username, password, firstname, lastname, emailAddress) {
        var reqData = { Username: username, Password: password, FirstName: firstname, LastName: lastname, EmailAddress: emailAddress }
        return sendajaxRequest("POST", 'registeruser', reqData)
    }
};
