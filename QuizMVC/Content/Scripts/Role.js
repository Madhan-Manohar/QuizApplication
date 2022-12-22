// post Method 

function saverole() {
    var url = "http://localhost:5195/api/RoleDetails";
    var role = {};
    role.RoleDescription = $("#description").val();
    role.Status = $("#status").val();
    role.IsActive = 1;
    role.CreatedBy = 1;
    role.CreatedOn = "2022-01-01";
    role.ModifiedBy = 1;
    role.ModifiedOn = "2022-01-01";
    if (role) {
        $.ajax({
            url: url,
            datatype: "json",
            data: JSON.stringify(role),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: "Post",
            success: function (result) {
                clear();
                //Manager.GetAllProducts();
                location.reload();
                /*alert("Role Added ");*/
            },
            error: function (msg) {
                clear();
                alert("Please Check Credientials");
            }
        });
    }

}


function clear() {
    $("#description").val('');
    $("#status").val('');
}


// get method  

$(document).ready(function () {
    Manager.GetAllProducts();
})

var Manager = {
    GetAllProducts: function () {
        var obj = "";
        var serviceUrl = "http://localhost:5195/api/RoleDetails";
        window.Manager.GetAPI(serviceUrl, onSuccess, onFailed);
        function onSuccess(jsonData) {
            obj = jsonData;
            //$('Table').html('');
            $.each(jsonData, function (i, item) {
                var rows = "<tr>" +
                    /*"<td id='RoleId'>" + item.roleId + "</td>" +*/
                    "<td id='RoleDescription'>" + item.roleDescription + "</td>" +
                    "<td id='Status'>" + item.status + "</td>" +
                    "<td> <buton class='btn btn-danger' onclick='deleterole(" + item.roleId + ")'> Delete </button> </td>" +
                    "<td> <buton class='btn btn-primary'onclick='getbyid(" + item.roleId + ")'> Edit </button> </td>" +
                    "</tr>";
                $('Table').append(rows);

            });

        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return obj;

    },
    GetAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            datatype: "json",
            success: successCallback,
            error: errorCallback
        });
    },
}

// Delete Method

function deleterole(id) {
    var url = "http://localhost:5195/api/RoleDetails/" + id;

    $.ajax({
        url: url,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        type: "Delete",
        success: function (result) {
            clear();

            //Manager.GetAllProducts();
            location.reload();
            alert(result);
        },
        error: function (msg) {
            clear();
            alert("Please Check Credientials");
        }
    });
}



// edit Method



function getbyid(id) {
    var url = "http://localhost:5195/api/RoleDetails/" + id;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                $('#description').val(response.roleDescription);
                $('#status').val(response.status);
                $('#roleid').val(response.roleId);
                //$('#EmployeeLocation').val(response.Location);
            }
            else {
                alert("Something went wrong");
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editrole() {
    var url = "http://localhost:5195/api/RoleDetails";
    var role = {};

    role.RoleId = $("#roleid").val();
    role.RoleDescription = $("#description").val();
    role.Status = $("#status").val();

    role.IsActive = 1;
    role.CreatedBy = 1;
    role.CreatedOn = "2022-01-01";
    role.ModifiedBy = 1;
    role.ModifiedOn = "2022-01-01";
    if (role) {
        $.ajax({
            url: url,
            datatype: "json",
            data: JSON.stringify(role),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: "PUT",
            success: function (result) {
                clear();
                alert("Edited")
                //Manager.GetAllProducts();
                location.reload();
                /*alert("Role Added ");*/
            },
            error: function (msg) {
                clear();
                alert("Please Check Credientials");
            }
        });
    }

}





    //function productUpdate(product) {
    //    $.ajax({
    //        url: "/api/Product",
    //        type: 'PUT',
    //        contentType:
    //            "application/json;charset=utf-8",
    //        data: JSON.stringify(product),
    //        success: function (product) {
    //            return (product);
    //        },
    //        error: function (error) {
    //            handleException(request, message, error);
    //        }
    //    });
    //}




//$(document).ready(function () {
//    getroles();
//})




//function getroles() {

//    var url = "http://localhost:43073/api/RoleDetails";
//    $.ajax({
//        url: url,
//        datatype: "json",
//        type: "Get",
//        success: function (result) {
//            //alert(JSON.stringify(result));
//            if (result) {
//                var row = '';
//                for (let i = 0; i < result.lenght; i++) {
//                    //row += '<tr>' +
//                    //    '<td>' + result[i].oleDescription + '</td>' +
//                    //    '<td>' + result[i].Status + '</td>' +
//                    //    '</tr>';


//                    row = row
//                        + "<tr>"
//                        + "<td>" + result[i].roleDescription + "</td>"
//                        + "<td>" + result[i].status + "</td>"
//                        + "</tr>";
//                }
//                if (row != '') {
//                    $('#Table').append(row);
//                }
//            }
//        },
//        error: function (msg) {
//            alert(msg);
//        }

//    });

//}


//$(document).ready(function () {
//    $.ajax({
//        url: "http://localhost:43073/api/RoleDetails",
//        dataType: 'json',
//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                var row = $('<tr><td>' + data[i].RoleDescription + '</td><td>' + data[i].Status + '</td><td>' + data[i].IsActive + '</td></tr>');
//                $('#myTable').append(row);
//            }
//        },
//        error: function (jqXHR, textStatus, errorThrown) {
//            alert('Error: ' + textStatus + ' - ' + errorThrown);
//        }
//    });
//})


//$(document).ready(function () {
//    GetData();
//})
//function GetData() {
//    $.ajax({
//        url: "https://localhost:43073/api/RoleDetails",
//        method: "GET",
//        success: function (res) {
//            var tableString = "";
//            $.each(res, function (index, value) {
//                tableString += "<tr><td>" + value.RoleDescription + "</td><td>" + value.Status + "</td><td>" + value.IsActive + "</td><td>" + value.ModifiedBy + "</td><td>" + value.CreatedBy + "</td></tr>";
//            });
//            $("#table1").append(tableString);
//        },
//        error: function (error) {
//            console.log(error);
//        }
//    });
//}
