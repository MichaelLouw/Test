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
    contentType: "application/json"
});

app.Projects = Backbone.Collection.extend({
  model: app.project
});


//tasks
app.task = Backbone.Model.extend({
  //do data validation
  urlRoot: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
  idAttribute: "id",
  contentType: "application/json"
});

app.Tasks = Backbone.Collection.extend({
  model: app.task
});
