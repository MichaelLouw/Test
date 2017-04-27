var UpdateProjectView = {};

UpdateProjectView.UpdateProject = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },

  event: {
    'click #btnUpdateProject': "UpdateProject"
  },

  render: function(){
    var project = sessionStorage.getItem("project");
    var UpdateProjectPage = "<div class='container'>"+
            "<h1 class='form-control' style='background-color: lightblue'>Add Task For Project</h1>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Title</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='text' class='form-control' id='title'/>"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Description</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='text' class='form-control' id='description' />"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Start Date</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='date' class='form-control' id='start_date' />"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>End Date</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='date' class='form-control' id='end_date'/>"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Is Billable</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<select id='is_billable'>"+
                      "<option>true</option>"+
                      "<option>false</option>"+
                    "</select>"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Is Active</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                  "<select id='is_active'>"+
                    "<option>true</option>"+
                    "<option>false</option>"+
                  "</select>"+
                "</div>"+
            "</div>"+
            "<br />"+
            "<div class='row' style='padding-left: 70%;'>"+
                "<a href='main.html'><button id='btnUpdateProject' class='form-control' style='width: 50%;'>Update Project</button></a>"+
            "</div>"+
        "</div>";

        this.$el.html(UpdateProjectView);
        return this;
  },

  UpdateProject: function(){
    //post new task to api.
    $.ajax({
      method: "PATCH",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/",
      headers:{
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      data: JSON.stringify(),
      processData: false,
      success: function(data){

      },
      error: function(data){
        //handle error.
      }
    });
  }
});
