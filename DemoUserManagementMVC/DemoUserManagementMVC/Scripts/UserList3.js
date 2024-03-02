$(document).ready(function() {
    var currentPage = 1;
    var sortExpression = "UserId";
    var sortDirection = "ASC";

    populateUserData();
    function populateUserData() {
        $.ajax({
            url: '/UserList3/LoadAllUser',
            type: 'GET',
            dataType: 'json',
            data: {
                sortExp: sortExpression,
                sortDir: sortDirection,
                page: currentPage
            },
            success: function (response) {
                var users = response.Users; 
                var tbody = $('#userListBody');
                tbody.empty();

                $.each(users, function (index, user) {
                    var row = $('<tr>');
                    row.append('<td><a href=\'/UserDetails2/Index?id=' + user.UserId + '\'>Edit</a></td>');
                    row.append('<td>' + user.UserId + '</td>');
                    row.append('<td>' + user.FirstName + '</td>');
                    row.append('<td>' + user.LastName + '</td>');
                    row.append('<td>' + user.FathersName + '</td>');
                    row.append('<td>' + user.MothersName + '</td>');
                    row.append('<td>' + user.DateOfBirth + '</td>');
                    row.append('<td>' + user.PrimaryPhoneNo + '</td>');
                    row.append('<td>' + user.PrimaryEmailId + '</td>');

                    tbody.append(row);
                });

                var totalPage = response.TotalUser / response.PageSize;
                updatePagination(totalPage);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching user data:', error);
            }
        });
    }

    // Function to update pagination
    function updatePagination(totalPages) {
        var pagination = $('.pagination');
        pagination.empty(); 

        for (var i = 1; i <= totalPages; i++) {
            var listItem = $('<li>').addClass('page-item');
            var link = $('<a>').addClass('page-link').attr('href', '#').text(i);
            listItem.append(link);
            pagination.append(listItem);
        }
    }

    // Event handler for sorting
    $('#userListTable3').on('click', 'th a', function(e) {
        e.preventDefault();
        var sortExp = $(this).data('sort-exp');
        if (sortExpression === sortExp) {
            sortDirection = sortDirection === "ASC" ? "DSC" : "ASC";
        } else {
            sortExpression = sortExp;
            sortDirection = "ASC";
        }
        currentPage = 1; 
        populateUserData();
    });

    // Event handler for pagination click
    $('.pagination').on('click', 'a', function(e) {
        e.preventDefault();
        currentPage = parseInt($(this).text());
        populateUserData();
    });


   
});

