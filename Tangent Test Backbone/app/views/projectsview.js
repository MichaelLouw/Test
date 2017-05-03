app.projectItemView = Backbone.View.extend({

  tagName: 'tr',

  template: _.template($("#trTemplate").html()),

  events: {
    "click .btnAddTask": "AddTask",
    "click .btnUpdateProject": "UpdateProject",
    "click .btnDeleteProject": "DeleteProject"
  },

  initialize: function(options){
    _.bindAll(this, 'render');

    this.listenTo(this.model, "change", this.render);
  },

  render: function(){
    this.$el.html(this.template(this.model));

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
    alert("in");
    //e.preventDefault();
    this.model.destroy();
  },

  UpdateProject: function(e){
    e.preventDefault();
    var updateproject = new app.UpdateProject({
      model: this.model
    });
    $("#mainContainer").html(updateproject.render().el);
  }
})

app.ProjectView = Backbone.View.extend({

  el: 'table',
  collection: null,

  initialize: function(){
    this.collection = this.model;

    _.bindAll(this, 'render');

    this.listenTo(this.model, "change", this.render);
  },

  //template: _.template($("#projectTableTemplate").html()),

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
    var element = jQuery(this.el);

    for (k in this.model.attributes){
      console.log(k);
      var projectView = new app.projectItemView({
        model: this.model.attributes[k]
      });

      element.append(projectView.render().el);
    }
    return this;

  }
});
