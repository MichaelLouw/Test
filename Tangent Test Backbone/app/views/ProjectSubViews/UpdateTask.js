var app = app || {};

app.UpdateTask = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },
  template: $("#updatetask").html(),

  events: {
    'click #btnUpdateTask': "UpdateTask"
  },

  initialize: function(){
    this.render();
  },

  render: function(){
    // var project = sessionStorage.getItem("project");
    //
    //     this.$el.html(UpdateProjectView);
    this.$el.html(this.template(this.model.toJSON()));
    return this;
  },

  UpdateTask: function(){
    //post new task to api.
    var task = sessionStorage.getItem("task");
    $.ajax({
      method: "PATCH",
      contentType: "application/json",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.pk.substr(1) + "/",
      headers:{
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      data: JSON.stringify({"title": task.title, "description": task.description,"start_date": task.start_date,"end_date": task.end_date,"is_billable": task.is_billable,"is_active": task.is_active}),
      processData: false,
      success: function(data){
        $.notify("Task updated", "success");
        window.location.replace("#/main");
      },
      error: function(data){
        //handle error.
        $.notify("error updating task", "error");
      }
    });
  }
});
