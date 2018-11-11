var toDoListItemViewModel = function () {

    var self = this;
    self.Items = ko.observableArray();
    self.IndexList = ko.observable();
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.CreateDate = ko.observable();

    self.CreateItem = function () {

        location.hash = 'todolist/create';
        ClearDetailsItem();
    }

    self.RemoveItem = function (listId) {

        location.hash = 'todolist';

        apiClient.RemoveList(listId).done(function (result) {
            if (result.Data == null) {
                self.Items.remove(c => c.Id == listId);
                toastr.success('Remove list');
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });

    }

    self.EditItem = function (indexList, listid, name, createDate) {

        location.hash = 'todolist/edit';
        self.IndexList(indexList);
        self.Id(listid);
        self.Name(name);
        self.CreateDate(createDate);
    }

    self.EditListItem = function () {

        location.hash = 'todolist';
        var indexList = self.IndexList();
        var listid = self.Id();
        var name = self.Name();
        var createDate = self.CreateDate();
        var data = { Id: listid, Name: name, CreateDate: createDate }

        apiClient.EditList(listid, name, createDate).done(function (result) {
            if (result.StatusCode == 201) {
                self.Items.splice(indexList, 1, data);
                toastr.success('Edit list');
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    self.GetDetail = function (listid) {
        GetDetails(listid);
    }

    GetLists = function () {
        self.Items.removeAll();
        apiClient.GetAllLists().done(function (result) {
            if (result.Data == null) {
                alert("ToDoList is empty  " + result.Message);
            }
            else {
                ko.utils.arrayPushAll(self.Items, result.Data);
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });

    }

    CreateListItem = function () {

        location.hash = 'todolist';
        var name = self.Name();
        var createDate = self.CreateDate();

        apiClient.CreateList(name, createDate).done(function (result) {
            if (result.StatusCode == 201) {
                GetLists();
                toastr.success('Create list');
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    ClearDetailsItem = function () {

        self.Name('');
        self.CreateDate('');
    }
};



