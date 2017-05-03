app.Router = Backbone.Router.extend({
  routes: {
    "": "home",
    "login": "login",
    "project/new": "AddProject",
    "project/newtask/:project": "AddTask",
    "project/delete/:project": "DeleteProject",
    "project/update/:project": "UpdateProject",
    "task/update/:task": "UpdateTask",
    "task/delete/:task": "DeleteTask"
  }
});
