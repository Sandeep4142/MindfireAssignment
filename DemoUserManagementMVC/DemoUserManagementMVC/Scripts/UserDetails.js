$(document).ready(function () {

    var currStateId = $("#currentStateId").val()
    var permanentStateId = $('#permanentStateId').val()

    $("#currentCountry").off("change");
    $("#permanentCountry").off("change");

    $("#currentCountry").change(function () {
        UpdateStates($(this).val(), currStateId, "currentState");
    });

    $("#permanentCountry").change(function () {
        UpdateStates($(this).val(), permanentStateId, "permanentState");
    });

    
    if ($("#currentCountry").val() != null && $("#currentCountry").val() != "") {
        UpdateStates($("#currentCountry").val(), currStateId, "currentState")
    }
    if ($("#permanentCountry").val() != null && $("#permanentCountry").val() != "") {
        UpdateStates($("#permanentCountry").val(), permanentStateId, "permanentState")
    }



    function UpdateStates(countryId, stateId, stateDropdownId) {
        console.log("Fetching states");
        $.ajax({
            url: '/UserDetails/GetStates',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ countryId: countryId }),
            dataType: 'json',
            success: function (data) {
                var states = data;
                console.log("states");
                var stateDropdown = $('#' + stateDropdownId);

                stateDropdown.empty();
                stateDropdown.append($('<option>', { value: '', text: 'Select State' }));

                $.each(states, function (index, state) {
                    stateDropdown.append($('<option>', { value: state.StateId, text: state.StateName }));
                });

                if (stateId !== null && stateId !== undefined && stateId !== "") {
                    stateDropdown.val(stateId);
                }
            },
            error: function (xhr, status, error) {
                console.log('Error loading states: ', error);
                console.log(xhr.responseText);
            }
        });
    }


})