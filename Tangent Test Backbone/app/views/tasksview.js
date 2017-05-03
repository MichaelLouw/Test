app.TaskView = Backbone.View.extend({

  tagName: 'tr',

  events: {
    "click #btnupdatetask": "UpdateTask",
    "click #btndeletetask": "DeleteTask"
  },

  initialize: function(){
    this.listenTo(this.model, "change", this.render);
  },

  template: _.template($("#taskTableTemplate").html()),

  render: function(){
    // var string = "<table class='table table-hover table-responsive'><thead><tr><th>Title</th><th>Due Date</th><th>Estimated Hours</th><th>Actions</th></thead><tbody>";
    // //var getTasks = JSON.parse(sessionStorage.getItem("Tasks"));
    // var getTasks = this.model.attributes;
    // console.log(getTasks + Object.keys(getTasks));
    // for (var i = 0; i < Object.keys(getTasks).length; i++){
    //   //console.log(getTasks[i].id);
    //   string += "<tr><td>" + getTasks[i].title +"</td><td>" + getTasks[i].due_date + "</td><td>" + getTasks[i].estimated_hours +"</td>" +
    //   "<td><a href='#/taskUpdate/:" + getTasks[i].id +"'><button>Update Task</button></a><a href='#/taskDelete/:" + getTasks[i].id +"'><button>Delete Task</button></a></td></tr>";
    // }
    // string += "</tbody></table>";
    // this.$el.html(string);
    // return this;
    this.$el.html(this.template(this.model.attributes));
    return this;
  },

  UpdateTask: function(e){
    e.preventDefault();
    var updatetask = new app.UpdateTask({
      model: this.model
    });
    $("#mainContainer").html(updatetask.render().el);
  },

  DeleteTask: function(e){
    e.preventDefault();
    this.model.DeleteTask({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}});
  }
});
