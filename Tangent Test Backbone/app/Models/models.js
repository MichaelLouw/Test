var models = {};

//login
models.login = Backbone.Model.extend({

});

models.logincredentials = Backbone.Collection.extend({
  model: models.login
});

//projects
models.project = Backbone.Model.extend({
    //do data validation.
});

models.Projects = Backbone.Collection.extend({
  model: models.project
});


//tasks
models.task = Backbone.Model.extend({
  //do data validation
});

models.Tasks = Backbone.Collection.extend({
  model: models.task
})
