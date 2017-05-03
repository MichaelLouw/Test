var app = app || {};

app.ProjectView = Backbone.View.extend({

  tagName: 'tr',

  events: {
    "click #AddTask": "AddTask",
    "click #DeleteProject": "DeleteProject",
    "click #UpdateProject": "UpdateProject"
  },

  initialize: function(){
    this.listenTo(this.model, "change", this.render);
  },

  template: _.template($("#projectTableTemplate").html()),

  render: function(){
    // var string = "<table class='table table-hover table-responsive'><thead><tr><th>Title</th><th>Description</th><th>Start Date</th><th>End Date</th><th>Is Billable</th><th>Is Active</th><th>Actions</th></thead><tbody>";
    // //var getProjects = JSON.parse(sessionStorage.getItem("Projects"));
    // var getProjects = this.model.attributes;
    // console.log(getProjects);
    // for (var count = 0; count < Object.keys(getProjects).length; count++){
    //   string += "<tr><td>" + getProjects[count].title +"</td><td>" + getProjects[count].description + "</td><td>" + getProjects[count].start_date +"</td><td>" + getProjects[count].end_date +"</td><td>" +
    //   getProjects[count].is_billable +"</td><td>" + getProjects[count].is_active +
    //   "</td><td><a href='#/projectAddTask/:"+ getProjects[count].pk +"'><button>Add Task</button></a><a href='#/projectDelete/:"+ getProjects[count].pk +"'><button>Delete</button></a><a href='#/projectUpdate/:"+ getProjects[count].pk +"'><button>Update</button></a></td></tr>";
    // };
    // string += "</tbody></table>"
    // this.$el.html(string);
    // return this;
    this.$el.html(this.template(this.model.attributes));
    return this;
  },

  AddTask: function(e){
    e.preventDefault();
    var addtasktoproject = new app.AddTask({
      model: this.model
    });
    $("#mainContainer").html(addtasktoproject.render().el);
  },

  DeleteProject: function(e){
    e.preventDefault();
    this.model.DeleteTask({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}});
  },

  UpdateProject: function(e){
    e.preventDefault();
    var updateproject = new app.UpdateProject({
      model: this.model
    });
    $("#mainContainer").html(updateproject.render().el);
  }
});
