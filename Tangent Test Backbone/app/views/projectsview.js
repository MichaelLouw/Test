var app = app || {};

app.ProjectView = Backbone.View.extend({
  //tagName: 'tr',

  //render: function(){
  //  jQuery(this.el).empty();

    //add table columns
  //  jQuery(this.el).append(jQuery('<tr>' + this.model.get('Title') + '</tr>'));
  //  jQuery(this.el).append(jQuery('<tr>' + this.model.get('Description') + '</tr>'));
  //  jQuery(this.el).append(jQuery('<tr>' + this.model.get('Start Date') + '</tr>'));
  //  jQuery(this.el).append(jQuery('<tr>' + this.model.get('End Date') + '</tr>'));
  //  jQuery(this.el).append(jQuery('<tr>' + this.model.get('Is Billable') + '</tr>'));
  //  jQuery(this.el).append(jQuery('<tr>' + this.model.get('Is Active') + '</tr>'));

  //  return this;
  //}
  render: function(){
    var string = "<table class='table table-hover table-responsive'><thead><tr><th>Title</th><th>Description</th><th>Start Date</th><th>End Date</th><th>Is Billable</th><th>Is Active</th><th>Actions</th></thead><tbody>";
    var getProjects = JSON.parse(sessionStorage.getItem("Projects"));
    console.log(getProjects);
    for (var count = 0; count < Object.keys(getProjects).length; count++){
      console.log(getProjects[count].pk);
      string += "<tr><td>" + getProjects[count].title +"</td><td>" + getProjects[count].description + "</td><td>" + getProjects[count].start_date +"</td><td>" + getProjects[count].end_date +"</td><td>" +
      getProjects[count].is_billable +"</td><td>" + getProjects[count].is_active +
      "</td><td><a href='#/projectAddTask/:"+ getProjects[count].pk +"'>Add Task</a><a href='#/projectDelete/:"+ getProjects[count].pk +"'>Delete</a><a href='#/projectUpdate/:"+ getProjects[count] +"'>Update</a></td></tr>";
    };
    string += "</tbody></table>"
    this.$el.html(string);
    return this;
  }
});

// project.ProjectViewBody = Backbone.View.extend({
//   collection: null,
//
//   el: 'tbody',
//
//   initialize: function(options){
//     this.collection = options.collection;
//
//     _.bindAll(this, 'render');
//
//     this.collection.bind('reset', this.render);
//     this.collection.bind('add', this.render);
//     this.collection.bind('remove', this.render);
//     this.collection.bind('update', this.render);
//   },
//
//   render: function(){
//     var element = jQuery(this.el);
//
//     element.empty();
//
//     this.collection.forEach(function(item){
//       var itemView = new views.ProjectViewItem({
//         projectmodel: item
//       });
//
//       element.append(itemView.render().el);
//     });
//
//     return this;
//   }
// });
