function CheckUser() {
    console.log("login clicked");
    var email = document.getElementById('loginEmail').value;
    var password = document.getElementById('loginPassword').value;

    PageMethods.CheckUser(email, password, onSuccess, onError);
    function onSuccess(userId) {
        if (userId == -1) {
            alert("User does not exist");
        } else if (userId == 0) {
            alert("Wrong password");
        }
        else {
            PageMethods.CheckIfUserIsAdmin(userId, onSuccess, onError);
            function onSuccess(isAdmin) {
                if (isAdmin) {
                    window.location.href = "UserList.aspx";
                } else {
                    window.location.href = "UserDetails2.aspx?id=" + userId;
                }
            }
        }
    }
    function onError(result) {
        alert('Something wrong.');
    }
}

//function CheckUser2() {
//    console.log("login clicked");
//    var email = document.getElementById('loginEmail').value;
//    var password = document.getElementById('loginPassword').value;

//    $.ajax({
//        type: "POST",
//        url: "LoginPage.aspx/CheckUser",
//        data: JSON.stringify({ email: email, password: password }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (userId) {
//            if (userId == 0) {
//                alert('Wrong password.');
//            }
//            else if (userId == -1) {
//                alert('User does not exist.');
//            } else {         
//                CheckIfUserIsAdmin(userId);
//            }
//        },
//        error: function (xhr, status, error) {
//            console.error("An error occurred while checking user:", error);
//            alert('Something went wrong.');
//        }
//    });
//}

//function CheckIfUserIsAdmin(userId) {
//    $.ajax({
//        type: "POST",
//        url: "LoginPage.aspx/CheckIfUserIsAdmin",
//        data: JSON.stringify({ userId: userId }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (isAdmin) {
//            if (isAdmin) {
//                window.location.href = "UserList.aspx";
//            } else {
//                window.location.href = "UserDetails2.aspx?id=" + userId;
//            }
//        },
//        error: function (xhr, status, error) {
//            console.error("An error occurred while checking if user is admin:", error);
//            alert('Something went wrong.');
//        }
//    });
//}



