var toDoListTaskViewModel = function () {

    var self = this;
    self.ItemTasks = ko.observableArray();
    self.ItemId = ko.observable();
    self.TaskId = ko.observable();
    self.Index = ko.observable();
    self.Text = ko.observable();
    self.IsCompleted = ko.observable(false);
    self.CreateDateTask = ko.observable();
    self.CompleteDate = ko.observable();

    self.editIsCompleted = function (index, taskid, text, iscompleted, createDate, completeDate) {
        var listId = self.ItemId();
        var isDone = !iscompleted;
        var data = { Id: taskid, Text: text, IsCompleted: isDone, CreateDate: createDate, CompleteDate: completeDate }

        apiClient.EditTask(listId, taskid, text, isDone, createDate, completeDate).done(function (result) {
            if (result.StatusCode == 201) {
                self.ItemTasks.splice(index, 1, data);
                toastr.success('Edit isCompleted task');
                return isDone;
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    self.CreateItemTask = function () {

        location.hash = 'todolist/task/create';
        ClearDetailsTask();
    }

    self.CreateTask = function () {

        location.hash = 'todolist/task';
        var listId = self.ItemId();
        var text = self.Text();
        var iscompleted = self.IsCompleted();
        var createDate = self.CreateDateTask();
        var completeDate = self.CompleteDate();

        var index = self.ItemTasks().length;

        apiClient.CreateTask(listId, text, iscompleted, createDate, completeDate).done(function (result) {
            if (result.Data == null) {
                alert("Task not create  " + result.Message);
            }
            else {
                GetDetails(listId);
                toastr.success('Create task');
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    self.RemoveTask = function (taskId) {

        location.hash = 'todolist/task';
        var listId = self.ItemId();

        apiClient.RemoveTask(listId, taskId).done(function (result) {
            if (result.Data == null) {
                self.ItemTasks.remove(c => c.Id == taskId);
                toastr.success('Remove task');
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });

    }

    self.EditItemTask = function (index, taskid, text, iscompleted, createDate, completeDate) {

        location.hash = 'todolist/task/edit';
        self.Index(index);
        self.TaskId(taskid);
        self.Text(text);
        self.IsCompleted(iscompleted);
        self.CreateDateTask(createDate);
        self.CompleteDate(completeDate);
    }

    self.EditTask = function () {

        location.hash = 'todolist/task';
        var index = self.Index();
        var listId = self.ItemId();
        var taskId = self.TaskId();
        var text = self.Text();
        var iscompleted = self.IsCompleted();
        var createDate = self.CreateDateTask();
        var completeDate = self.CompleteDate();
        var data = { Id: taskId, Text: text, IsCompleted: iscompleted, CreateDate: createDate, CompleteDate: completeDate }

        apiClient.EditTask(listId, taskId, text, iscompleted, createDate, completeDate).done(function (result) {
            if (result.StatusCode == 201) {
                self.ItemTasks.splice(index, 1, data);
                toastr.success('Edit task');
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });
    }

    GetDetails = function (listId) {

        self.ItemTasks.removeAll();
        location.hash = 'todolist/task';

        apiClient.GetDetailsItem(listId).done(function (result) {
            if (result.Data == null) {
                alert("ToDoListTask is empty  " + result.Message);
            }
            else {
                self.ItemId(listId);
                ko.utils.arrayPushAll(self.ItemTasks, result.Data.Tasks);
            }
        }).fail(function (error) {
            alert('ERROR: ' + error);
        });

    }

    ClearDetailsTask = function () {

        self.Text('');
        self.IsCompleted('');
        self.CreateDateTask('');
        self.CompleteDate('');
    }
};
