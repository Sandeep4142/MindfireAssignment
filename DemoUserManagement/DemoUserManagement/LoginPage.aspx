<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="DemoUserManagement.LoginPage" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login/Signup Page</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">

</head>
<body>
    <div class="container">
        <!-- Login Form -->
        <div id="login" class="tab-pane fade show active">
            <h3>Login</h3>
            <form runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                <div class="form-group">
                    <label for="loginEmail">Email address</label>
                    <input type="email" class="form-control" id="loginEmail" placeholder="Enter email">
                </div>
                <div class="form-group">
                    <label for="loginPassword">Password</label>
                    <input type="password" class="form-control" id="loginPassword" placeholder="Password">
                </div>
                <button type="submit" id="loginBtn" class="btn btn-primary" onclick="CheckUser(); return false;">Login</button>
                <div>
                    <a class="nav-link" href="UserDetails2.aspx">Sign Up/Register</a>
                </div>
            </form>
        </div>
    </div>


    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="Scripts/Login.js"></script>
   
</body>
</html>

