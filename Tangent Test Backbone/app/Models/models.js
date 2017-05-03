var app = app || {};

//login
app.login = Backbone.Model.extend({

});

app.logincredentials = Backbone.Collection.extend({
  model: app.login
});

//projects
app.project = Backbone.Model.extend({
    //do data validation.
    urlRoot: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/",
    idAttribute: "pk",
    contentType: "application/json",
    AddProject: function(header){
      this.add();
      this.save();
    },
    UpdateProject: function(header){
      this.set();
      this.save();
    },
    DeleteProject: function(header){
      this.destroy();
    }
});

app.Projects = Backbone.Collection.extend({
  model: app.project
});


//tasks
app.task = Backbone.Model.extend({
  //do data validation
  urlRoot: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
  idAttribute: "id",
  contentType: "application/json",
  AddTask: function(header){
    this.add();
    this.save();
  },
  UpdateTask: function(header){
    this.set();
    this.save();
  },
  DeleteTask: function(header){
    this.destroy();
  }
});

app.Tasks = Backbone.Collection.extend({
  model: app.task
});
