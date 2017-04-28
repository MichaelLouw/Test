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
});

app.Projects = Backbone.Collection.extend({
  model: app.project
});


//tasks
app.task = Backbone.Model.extend({
  //do data validation
});

app.Tasks = Backbone.Collection.extend({
  model: app.task
})
