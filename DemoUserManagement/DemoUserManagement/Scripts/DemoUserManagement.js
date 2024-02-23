document.addEventListener("DOMContentLoaded", function () {

    //change navbar item background to show on which page we are
    var currentPage = window.location.pathname.toLowerCase();
    var navLinks = document.querySelectorAll('.navbar-nav a');

    // Loop through each nav item and check if its link matches the current page
    navLinks.forEach(function (linkElement) {
        var link = linkElement.getAttribute('href').toLowerCase();
        if (currentPage.indexOf(link) !== -1) {
            // adding active class to list item
            linkElement.closest('a').classList.add('active-link');
        }
    });



    //Load UserDetails
    var urlParams = new URLSearchParams(window.location.search);
    var userId = urlParams.get('id');
    console.log("user id ---" + userId)

    if (userId) {
        loadUser(userId)
    }

    //checking if email is unique or not
    $('#primaryEmailID').on('blur', CheckEmailExist);
    console.log("Inside latest jquery");
    console.log($('#primaryEmailID').val());

    function CheckEmailExist() {
        console.log("Checking email");
        var email = $('#primaryEmailID').val();
        console.log(email);
        $.ajax({
            type: "POST",
            url: "UserDetails2.aspx/CheckEmailExists",
            data: JSON.stringify({ email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d) {
                    console.log(response.d);
                    $("#primaryEmailID").val("");
                    alert("Email already exists");
                }
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error:", xhr, status, error);
                alert("An error occurred while checking the email. Please try again later.");
            }
        });
    }


    // on clicking submit save user
    $('#submit-btn').click(function (e) {
        e.preventDefault();
        // Get form data
        var aadhaarFile = $("#aadharCard")[0].files[0];
        var profilePicFile = $("#profilePic")[0].files[0];
        var guidAadhaarFile = aadhaarFile ? (GenerateGuid() + "." + aadhaarFile.name.split('.').pop()) : "";
        var guidProfilePhoto = profilePicFile ? (GenerateGuid() + "." + profilePicFile.name.split('.').pop()) : "";

        var urlId = new URLSearchParams(window.location.search);
        var userid = urlId.get('id');
        var id = userid ? userid : 0;
        console.log(id);

        var userDetails = {
            UserId: id,
            FirstName: $("#firstName").val(),
            MiddleName: $("#middleName").val(),
            LastName: $("#lastName").val(),
            FathersName: $("#fatherName").val(),
            MothersName: $("#motherName").val(),
            DateOfBirth: $("#dateOfBirth").val(),
            Gender: $("input[name='Gender']:checked").val(),
            AadhaarNo: $("#aadharCardNo").val(),
            AadhaarFile: aadhaarFile ? aadhaarFile.name : "", // Get Aadhaar file name
            GuidAadhaarFile: guidAadhaarFile,
            ProfilePhoto: profilePicFile ? profilePicFile.name : "", // Get profile photo file name
            GuidProfilePhoto: guidProfilePhoto,
            PrimaryPhoneNo: $("#primaryMobileno").val(),
            AlternatePhoneNo: $("#alternateMobileno").val(),
            PrimaryEmailId: $("#primaryEmailID").val(),
            AlternateEmailId: $("#alternateEmailId").val(),
            Password: $("#password").val(),
            Hobbies: fetchHobbies()
        };

        var addressDetails = [
            {
                UserID: id,
                Locality: $("#currentLocality").val(),
                City: $("#currentCity").val(),
                CountryId: $("#currentCntry").val(),
                StateId: $("#currentState").val(),
                Pincode: $("#currentPincode").val(),
                AddressType: 1
            },
            {
                UserID: id,
                Locality: $("#permanentLocality").val(),
                City: $("#permanentCity").val(),
                CountryId: $("#permanentCntry").val(),
                StateId: $("#permanentState").val(),
                Pincode: $("#permanentPincode").val(),
                AddressType: 2
            }]
        var educationDetails = [
            {
                UserId: id,
                Institution: $("#10thInst").val(),
                University: $("#10thBoard").val(),
                Marks: parseFloat($("#10thMarks").val()),
                EducationType: 1
            },

        {
            UserId: id,
            Institution: $("#12thInst").val(),
            University: $("#12thBoard").val(),
            Marks: parseFloat($("#12thMarks").val()),
            EducationType: 2
        },
        {
            UserId: id,
            Institution: $("#gradInst").val(),
            University: $("#gradUni").val(),
            Marks: parseFloat($("#gradMarks").val()),
            EducationType: 3
            }
        ];

        var userData = {
            user: userDetails,
            addresses: addressDetails,
            educations: educationDetails,
        };



        // AJAX call to save user details
        $.ajax({
            type: 'POST',
            url: 'UserDetails2.aspx/SaveAllDetails',
            data: JSON.stringify({userData : userData} ),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                // Handle success response
                console.log("User saved");               

                uploadFile(aadhaarFile, guidAadhaarFile);
                uploadFile(profilePicFile, guidProfilePhoto);

                alert("Registered");

            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });


    function GenerateGuid() {
        // Generate a random 8-character alphanumeric string
        var randomString = Math.random().toString(36).substring(2, 36);
        var uniqueFileName = randomString;
        return uniqueFileName;
    }


    //upload aadhaar and profile pic
    function uploadFile(fileInput, filename) {
        var file = fileInput;

        if (file) {
            var formData = new FormData();
            formData.append('File', file);
            formData.append('Name', filename);

            $.ajax({
                url: '/UploadHandler.ashx',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    console.log("File uploaded");
                    return response;
                },
                error: function (error) {
                    console.log("File uploaded failed");
                    return error;
                }
            });
        } else {
            console.error('No file selected.');
        }
    }



});



function fetchHobbies() {
    var hobbies = "Dance";
    $("input[name='Hobbies']:checked").each(function () {
        hobbies = hobbies + ", " + ($(this).val());
    });
    return hobbies;
}
// Similar AJAX calls for other methods

//load country and state
AddItemsToCountryDropDown();

function AddItemsToCountryDropDown() {
    console.log("Fetching country");
    $.ajax({
        type: 'POST',
        url: 'UserDetails2.aspx/GetCountries',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            var countries = response.d;
            var Countrydropdowns = ['currentCntry', 'permanentCntry'];

            for (var i = 0; i < Countrydropdowns.length; i++) {

                var CountrydropdownId = Countrydropdowns[i];
                var Countrydropdown = $('#' + CountrydropdownId);
                Countrydropdown.empty();

                // Add options from the received data
                Countrydropdown.append($('<option>', { value: '', text: 'Select Country' }));
                for (var j = 0; j < countries.length; j++) {
                    Countrydropdown.append($('<option>', { value: countries[j].CountryId, text: countries[j].CountryName }));
                }
            }
        },
        error: function (xhr, status, error) {
            console.log('Error loading countries: ', error);
            console.log(xhr.responseText);
        }

    });
}


function UpdateStates(CountryId, StatedropdownId) {
    console.log("Fetching states");
    var countryId = $("#" + CountryId).val();
    $.ajax({
        url: 'UserDetails2.aspx/GetStates',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ countryId: countryId }),
        dataType: 'json',
        success: function (data) {

            var states = data.d;
            console.log("states");
            var Statedropdown = $('#' + StatedropdownId);

            Statedropdown.empty();
            Statedropdown.append($('<option>', { value: '', text: 'Select State' }));

            $.each(states, function (index, state) {
                Statedropdown.append($('<option>', { value: state.StateId, text: state.StateName }));
            });
        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}


$("#currentCntry").change(function () {
    UpdateStates("currentCntry", "currentState");
});

$("#permanentCntry").change(function () {
    UpdateStates("permanentCntry", "permanentState");
});




function loadUser(userId) {
    // AJAX call to load user details
    $.ajax({
        type: 'POST',
        url: 'UserDetails2.aspx/LoadUser',
        data: JSON.stringify({ id: userId }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            // Handle success response
            if (response.d != null) {
                console.log("Inside respone - " + response.d);

                var userInfo = response.d;

                // Populate form fields with user details
                $("#firstName").val(userInfo.FirstName);
                $("#middleName").val(userInfo.MiddleName);
                $("#lastName").val(userInfo.LastName);
                $("#fatherName").val(userInfo.FathersName);
                $("#motherName").val(userInfo.MothersName);

                var timestamp = parseInt(userInfo.DateOfBirth.match(/\d+/)[0]);
                var dateOfBirth = new Date(timestamp);
                $("#dateOfBirth").val(dateOfBirth.toISOString().split('T')[0]);

                console.log("date -- " + $("#dateOfBirth").val());


                $("input[name='Gender'][value='" + userInfo.Gender + "']").prop("checked", true);

                $("#aadharCardNo").val(userInfo.AadhaarNo);
                $("#aadharCardFileName").text(userInfo.AadhaarFile);
                $("#profilPicFilename").text(userInfo.ProfilePhoto);
                $("#primaryMobileno").val(userInfo.PrimaryPhoneNo);
                $("#alternateMobileno").val(userInfo.AlternatePhoneNo);
                $("#primaryEmailID").val(userInfo.PrimaryEmailId);
                $("#alternateEmailId").val(userInfo.AlternateEmailId);
                $("#password").val(userInfo.Password);

                // Split and set hobbies
                var hobbies = userInfo.Hobbies.split(',');
                hobbies.forEach(function (hobby) {
                    $("input[name='Hobbies'][value='" + hobby.trim() + "']").prop("checked", true);
                });

                // Set address
                userInfo.Address.forEach(function (address) {
                    if (address.AddressType === 1) {
                        $("#currentLocality").val(address.Locality);
                        $("#currentCity").val(address.City);
                        $("#currentCntry").val(address.CountryId);
                        UpdateStates("currentCntry", "currentState");
                        console.log("state id - " + address.StateId);
                        $("#currentState").val(address.StateId);

                        $("#currentPincode").val(address.Pincode);
                    } else if (address.AddressType === 2) {
                        $("#permanentLocality").val(address.Locality);
                        $("#permanentCity").val(address.City);
                        $("#permanentCntry").val(address.CountryId);
                        UpdateStates("permanentCntry", "permanentState");
                        $("#permanentState").val(address.StateId);
                        $("#permanentPincode").val(address.Pincode);
                    }
                });


                // Set education details
                userInfo.Educations.forEach(function (education) {
                    if (education.EducationType === 1) {
                        $("#10thInst").val(education.Institution);
                        $("#10thBoard").val(education.University);
                        $("#10thMarks").val(education.Marks);
                    } else if (education.EducationType === 2) {
                        $("#12thInst").val(education.Institution);
                        $("#12thBoard").val(education.University);
                        $("#12thMarks").val(education.Marks);
                    } else if (education.EducationType === 3) {
                        $("#gradInst").val(education.Institution);
                        $("#gradUni").val(education.University);
                        $("#gradMarks").val(education.Marks);
                    }
                });
            } else {
                console.log("Inside alert");
                alert("You are not authorized to access")
            }

        },
        error: function (xhr, status, error) {
            // Handle error
            console.error(xhr.responseText);
        }
    });
}


//logOut
function LogOut() {
    console.log("Logout Clicked");

    $.ajax({
        type: "POST",
        url: "LoginPage.aspx/LogOut",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("Logout successful");
            window.location.href = "LoginPage.aspx";
        },
        error: function (xhr, status, error) {
            console.error("An error occurred while logging out:", error);
            alert('Something went wrong while logging out.');
        }
    });
}

//scroll to inavlid input
function scrollToInvalid() {
    // Check if there are any validation errors
    if (Page_ClientValidate()) {
        return true;
    } else {
        for (var i = 0; i < Page_Validators.length; i++) {
            var validator = Page_Validators[i];
            // Check if the validator is invalid
            if (!validator.isvalid) {
                var control = document.getElementById(validator.controltovalidate);
                if (control) {
                    control.scrollIntoView();
                    control.focus();
                    break;
                }
            }
        }
        return false;
    }
}


