var app = app || {};

app.UpdateProject = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },
  template: $("#updateproject").html(),

  events: {
    'click #btnUpdateProject': "UpdateProject"
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

  UpdateProject: function(){
    //post new task to api.
    var project = sessionStorage.getItem("project");
    $.ajax({
      method: "PATCH",
      contentType: "application/json",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.pk.substr(1) + "/",
      headers:{
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      data: JSON.stringify({"title": project.title, "description": project.description,"start_date": project.start_date,"end_date": project.end_date,"is_billable": project.is_billable,"is_active": project.is_active}),
      processData: false,
      success: function(data){
        $.notify("Project updated", "success");
        window.location.replace("#/main");
      },
      error: function(data){
        //handle error.
        $.notify("error updating project", "error");
      }
    });
  }
});
