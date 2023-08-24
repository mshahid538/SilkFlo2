
function dateRangeClicked(event){
    $('#exampleModalCenter').modal('show');
}

function closeFilterModal() {
    $('#exampleModalCenter').modal('hide');
}


function onRangeFilter() {
    var perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption;

    //for (const rB of perfFilterRadioOptions) {
    //    if (rB.checked) {
    //        selectedOption = rB.value;
    //        break;
    //    }
    //}

    //let selectedValue;
    for (var i = 0; i < perfFilterRadioOptions.length; i++) {
        if (perfFilterRadioOptions[i].checked) {
            selectedOption = perfFilterRadioOptions[i].value;
            break;
        }
    }

    if (selectedOption == "DATERANGE") {
        const startDatepicker = document.getElementById("startDatepicker");
        startDatepicker.disabled = false;
        const endDatepicker = document.getElementById("endDatepicker");
        endDatepicker.disabled = false;
    }
    else{
        const startDatepicker = document.getElementById("startDatepicker");
        startDatepicker.disabled = true;
        const endDatepicker = document.getElementById("endDatepicker");
        endDatepicker.disabled = true;
    }
}


function applyAndAdjustFilteredDataIntoExistingView() {

    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }
    

    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $.when(
        $.get("/api/Dashboard/GetTotalIdeas" + queryParams),
        $.get("/api/Dashboard/GetTotalInBuild" + queryParams),
        $.get("/api/Dashboard/GetTotalDeployed" + queryParams),
        $.get("/api/Dashboard/GetTotalDeploymentBenefits" + queryParams),
        $.get("/api/Dashboard/GetPipelineBenefitsByStage" + queryParams)
    ).done(function (totalIdeasResponse, totalInBuildResponse, totalDeployedResponse, totalDeploymentBenefitsResponse, pipelineBenefitsByStage) {
        $("#totalIdeas").html(totalIdeasResponse[0]);
        $("#totalInBuild").html(totalInBuildResponse[0]);
        $("#totalDeployed").html(totalDeployedResponse[0]);
        $("#totalDeploymentBenefits").html(totalDeploymentBenefitsResponse[0]);


        var pipelineBenefitsByStageDiv = document.getElementById("Chart.PipelineBenefitsByStage");
        $(pipelineBenefitsByStageDiv).empty();
        pipelineBenefitsByStageDiv.innerHTML = pipelineBenefitsByStage[0];

        //destroy loader
        var loaderElement = document.getElementById("quickFilter-loader");
        loaderElement.innerHTML = '';

        //close modal
        $('#exampleModalCenter').modal('hide');
    });

    $.get("/api/Dashboard/GetAutomationProgramPerformance" + queryParams, function (getSummaryResponse) {
        
        var myDiv = document.getElementById("Chart.AutomationProgramPerformance");

        // Remove all child elements
        $(myDiv).empty();

        // Set the div's innerHTML property to the new HTML string
        myDiv.innerHTML = getSummaryResponse;
    });

    $.get("/api/Business/Idea/GetSummary" + queryParams, function (getSummaryResponse) {
        
        var myDiv = document.getElementById("Business.Idea.Summary");

        // Remove all child elements
        $(myDiv).empty();

        // Set the div's innerHTML property to the new HTML string
        myDiv.innerHTML = getSummaryResponse;
    });

    //end of process
}


function clearAllFiltersAndAdjustDataIntoExistingView() {
    $('#exampleModalCenter').modal('hide');
    reRenderFilterOptionModal();
    
    //
    $("#totalIdeas").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    $("#totalInBuild").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    $("#totalDeployed").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    $("#totalDeploymentBenefits").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    
    //close modal
    $('#exampleModalCenter').modal('hide');

    //now api's
    $.when(
        $.get("/api/Dashboard/GetTotalIdeas"),
        $.get("/api/Dashboard/GetTotalInBuild"),
        $.get("/api/Dashboard/GetTotalDeployed"),
        $.get("/api/Dashboard/GetTotalDeploymentBenefits"),
        $.get("/api/Dashboard/GetPipelineBenefitsByStage")
    ).done(function (totalIdeasResponse, totalInBuildResponse, totalDeployedResponse, totalDeploymentBenefitsResponse, pipelineBenefitsByStage) {
        $("#totalIdeas").html(totalIdeasResponse[0]);
        $("#totalInBuild").html(totalInBuildResponse[0]);
        $("#totalDeployed").html(totalDeployedResponse[0]);
        $("#totalDeploymentBenefits").html(totalDeploymentBenefitsResponse[0]);


        var pipelineBenefitsByStageDiv = document.getElementById("Chart.PipelineBenefitsByStage");
        $(pipelineBenefitsByStageDiv).empty();
        pipelineBenefitsByStageDiv.innerHTML = pipelineBenefitsByStage[0];

        //destroy loader
        var loaderElement = document.getElementById("quickFilter-loader");
        loaderElement.innerHTML = '';
    });

    $.get("/api/Dashboard/GetAutomationProgramPerformance", function (getSummaryResponse) {

        var myDiv = document.getElementById("Chart.AutomationProgramPerformance");

        // Remove all child elements
        $(myDiv).empty();

        // Set the div's innerHTML property to the new HTML string
        myDiv.innerHTML = getSummaryResponse;
    });

    $.get("/api/Business/Idea/GetSummary", function (getSummaryResponse) {

        var myDiv = document.getElementById("Business.Idea.Summary");

        // Remove all child elements
        $(myDiv).empty();

        // Set the div's innerHTML property to the new HTML string
        myDiv.innerHTML = getSummaryResponse;
    });
}


function reRenderFilterOptionModal() {
    var myDiv = document.querySelector('[name="Dashboard.Performance.Content"]');
    $(myDiv).empty();
    myDiv.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $.get("/api/Dashboard/Performance", function (getPerformanceResponse) {
        var myDiv = document.querySelector('[name="Dashboard.Performance.Content"]');
        $(myDiv).empty();
        myDiv.innerHTML = getPerformanceResponse;
    });
}


//workshops filters behaviours
function showWorkshopModal() {
    $('#workshopFilterModal').modal('show');
}

function hideWorkshopModal() {
    $('#workshopFilterModal').modal('hide');
}

function applyFiltersAndAdjustWorkshopCurrentView() {
    var locations = window.location.href.split('/');
    if (locations && locations[locations.length - 1] == "Review") {
        applyFiltersAndAdjustWorkshopStatusView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Assess") {
        applyFiltersAndAdjustWorkshopAssessView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Decision") {
        applyFiltersAndAdjustWorkshopDecisionView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Build") {
        applyFiltersAndAdjustWorkshopBuildView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Deployed") {
        applyFiltersAndAdjustWorkshopDeployedView();
        return;
    }
    

    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }


    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#content").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/All" + queryParams),
    ).done(function (workResponse) {
        $("#content").html(workResponse);
        //$('#workshopFilterModal').modal('hide');

        //close modal
        //hideWorkshopModal();
    });

}


function applyFiltersAndAdjustWorkshopStatusView() {
    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }


    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Review" + queryParams),
        $.get("/api/tile/StageGroup/Review/TotalIdeas" + queryParams),
        $.get("/api/tile/StageGroup/Review/AwaitingReview" + queryParams)
    ).done(function (workResponse, totalIdeaResponse, awaitingReviewReponse) {
        $("#WorkshopContent").html(workResponse[0]);

        console.log("totalIdeaResponse", totalIdeaResponse);
        console.log("awaitingReviewReponse", awaitingReviewReponse)

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Review/TotalIdeas">'+ totalIdeaResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Review/AwaitingReview">' + awaitingReviewReponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);
    });

}

function applyFiltersAndAdjustWorkshopAssessView() {
    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }


    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Assess" + queryParams),
        $.get("/api/tile/StageGroup/Assess/TotalIdeas" + queryParams),
        $.get("/api/tile/StageGroup/Assess/PotentialHourSavings" + queryParams),
        $.get("/api/tile/StageGroup/Assess/AwaitingReview" + queryParams),
        $.get("/api/tile/StageGroup/Assess/PotentialBenefit" + queryParams)
    ).done(function (workResponse, totalIdeaResponse, potentialHourSavingsReponse, awaitingReviewResponse, potentialBenefitResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Assess/TotalIdeas">' + totalIdeaResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Assess/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Assess/PotentialHourSavings">' + potentialHourSavingsReponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Assess/AwaitingReview">' + awaitingReviewResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);
    });

}


function applyFiltersAndAdjustWorkshopDecisionView() {
    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }


    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Decision" + queryParams),
        $.get("/api/tile/StageGroup/Decision/PotentialBenefit" + queryParams),
        $.get("/api/tile/StageGroup/Decision/EstimatedOneTimeCost" + queryParams),
        $.get("/api/tile/StageGroup/Decision/EstimatedRunningCosts" + queryParams),
    ).done(function (workResponse, potentialBenefitResponse, estimatedOneTimeCostReponse, estimatedRunningCostsResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Decision/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Decision/EstimatedOneTimeCost">' + estimatedOneTimeCostReponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Decision/EstimatedRunningCosts">' + estimatedRunningCostsResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);
    });

}



function applyFiltersAndAdjustWorkshopBuildView() {
    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }


    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Build" + queryParams),
        $.get("/api/tile/StageGroup/Build/TotalInBuild" + queryParams),
        $.get("/api/tile/StageGroup/Build/PotentialBenefit" + queryParams),
        $.get("/api/tile/StageGroup/Build/TotalAtRisk" + queryParams),
        $.get("/api/tile/StageGroup/Build/TotalBenefitAtRisk" + queryParams),
        $.get("/api/tile/StageGroup/Build/EstimatedOneTimeCost" + queryParams),
    ).done(function (workResponse, totalInBuildResponse, potentialBenefitResponse, totalAtRiskResponse, totalBenefitAtRiskResponse,
        estimatedOneTimeCostResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Build/TotalInBuild">' + totalInBuildResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Build/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Build/TotalAtRisk">' + totalAtRiskResponse[0] + '</div>' + 
            '<div silkflo-url="tile/StageGroup/Build/TotalBenefitAtRisk">' + totalBenefitAtRiskResponse[0] + '</div>' + 
            '<div silkflo-url="tile/StageGroup/Build/EstimatedOneTimeCost">' + estimatedOneTimeCostResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);

        setTimeout(() => {
            $.get("/api/Workshop/GetAutomationBuildPipeline" + queryParams, function (getSummaryResponse) {
                //var myDiv = document.getElementById("Chart.AutomationBuildPipeline");
                var myDiv = document.querySelector("[name='Chart.AutomationBuildPipeline'], [id='automationMatrix']");

                console.log("myDiv", myDiv)

                // Remove all child elements
                $(myDiv).empty();

                // Set the div's innerHTML property to the new HTML string
                myDiv.innerHTML = getSummaryResponse;
            });
        });

    });
}



function applyFiltersAndAdjustWorkshopDeployedView() {
    const perfFilterRadioOptions = document.querySelectorAll('input[name="dateSpanRd"]');
    let selectedOption, queryParams = "";

    for (const rB of perfFilterRadioOptions) {
        if (rB.checked) {
            selectedOption = rB.value;
            break;
        }
    }

    if (selectedOption) {
        if (selectedOption == "DATERANGE") {
            var startDate = document.getElementById("startDatepicker").value
            var endDate = document.getElementById("endDatepicker").value

            queryParams = "?startDate=" + startDate + "&endDate=" + endDate;
        }
        if (selectedOption == "WEEK") {
            queryParams = "?isWeekly=true";
        }
        if (selectedOption == "MONTH") {
            queryParams = "?isMonthly=true";
        }
        if (selectedOption == "YEAR") {
            queryParams = "?isYearly=true";
        }
    }


    const isCheckboxes = document.querySelectorAll('input[name="ISListCheckbox"]');
    const isCheckedValues = [];

    isCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            isCheckedValues.push(checkbox.value);
        }
    });

    if (isCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&ideaSubmitters=" + isCheckedValues.join(',');
        }
        else {
            queryParams = "?ideaSubmitters=" + isCheckedValues.join(',');
        }
    }

    const poCheckboxes = document.querySelectorAll('input[name="POListCheckbox"]');
    const poCheckedValues = [];

    poCheckboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            poCheckedValues.push(checkbox.value);
        }
    });

    if (poCheckedValues) {
        if (queryParams) {
            queryParams = queryParams + "&processOwners=" + poCheckedValues.join(',');
        }
        else {
            queryParams = "?processOwners=" + poCheckedValues.join(',');
        }
    }

    //checking BU checkboxes
    const perfFilterDepartmentRadioOptions = document.querySelectorAll('input[name="filterDepartment"]');
    let selectedFDOption = [];

    for (var i = 0; i < perfFilterDepartmentRadioOptions.length; i++) {
        if (perfFilterDepartmentRadioOptions[i].checked) {
            selectedFDOption.push(perfFilterDepartmentRadioOptions[i].id);
            break;
        }
    }

    if (selectedFDOption) {
        if (queryParams) {
            queryParams = queryParams + "&departmentsId=" + selectedFDOption.join(',');
        }
        else {
            queryParams = "?departmentsId=" + selectedFDOption.join(',');
        }
    }


    //for teams
    const perfFilterTeamRadioOptions = document.querySelectorAll('input[name="filterTeam"]');
    let selectedFTOption = [];

    for (var i = 0; i < perfFilterTeamRadioOptions.length; i++) {
        if (perfFilterTeamRadioOptions[i].checked) {
            selectedFTOption.push(perfFilterTeamRadioOptions[i].id);
            break;
        }
    }

    if (selectedFTOption) {
        if (queryParams) {
            queryParams = queryParams + "&teamsId=" + selectedFTOption.join(',');
        }
        else {
            queryParams = "?teamsId=" + selectedFTOption.join(',');
        }
    }


    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Deployed" + queryParams),
        $.get("/api/tile/StageGroup/Deployed/TotalInDeployed" + queryParams),
        $.get("/api/tile/StageGroup/Deployed/PotentialBenefit" + queryParams),
        $.get("/api/tile/StageGroup/Deployed/PotentialHourSavings" + queryParams)
    ).done(function (workResponse, totalInDeployedResponse, potentialBenefitResponse, potentialHourSavingsResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Deployed/TotalInDeployed">' + totalInDeployedResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Deployed/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Deployed/PotentialHourSavings">' + potentialHourSavingsResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);

        //setTimeout(() => {
        //    $.get("/api/Workshop/GetAutomationBuildPipeline" + queryParams, function (getSummaryResponse) {
        //        //var myDiv = document.getElementById("Chart.AutomationBuildPipeline");
        //        var myDiv = document.querySelector("[name='Chart.AutomationBuildPipeline'], [id='automationMatrix']");

        //        console.log("myDiv", myDiv)

        //        // Remove all child elements
        //        $(myDiv).empty();

        //        // Set the div's innerHTML property to the new HTML string
        //        myDiv.innerHTML = getSummaryResponse;
        //    });
        //});

    });
}


//clear--behaviours

function clearAllFiltersAndAdjustDataIntoCurrentView(){
    var locations = window.location.href.split('/');
    if (locations && locations[locations.length - 1] == "Review") {
        clearApplyFiltersAndAdjustWorkshopStatusView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Assess") {
        clearApplyFiltersAndAdjustWorkshopAssessView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Decision") {
        clearApplyFiltersAndAdjustWorkshopDecisionView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Build") {
        clearApplyFiltersAndAdjustWorkshopBuildView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "Deployed") {
        clearApplyFiltersAndAdjustWorkshopDeployedView();
        return;
    }
    else if (locations && locations[locations.length - 1] == "All") {
        clearApplyFiltersAndAdjustWorkshopCurrentView();
        return;
    }
}


function clearApplyFiltersAndAdjustWorkshopCurrentView() {
    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#content").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/All"),
    ).done(function (workResponse) {
        $("#content").html(workResponse);
    });
}

function clearApplyFiltersAndAdjustWorkshopStatusView() {
    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Review"),
        $.get("/api/tile/StageGroup/Review/TotalIdeas"),
        $.get("/api/tile/StageGroup/Review/AwaitingReview")
    ).done(function (workResponse, totalIdeaResponse, awaitingReviewReponse) {
        $("#WorkshopContent").html(workResponse[0]);

        console.log("totalIdeaResponse", totalIdeaResponse);
        console.log("awaitingReviewReponse", awaitingReviewReponse)

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Review/TotalIdeas">' + totalIdeaResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Review/AwaitingReview">' + awaitingReviewReponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);
    });

}

function clearApplyFiltersAndAdjustWorkshopAssessView() {
    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Assess"),
        $.get("/api/tile/StageGroup/Assess/TotalIdeas"),
        $.get("/api/tile/StageGroup/Assess/PotentialHourSavings"),
        $.get("/api/tile/StageGroup/Assess/AwaitingReview"),
        $.get("/api/tile/StageGroup/Assess/PotentialBenefit")
    ).done(function (workResponse, totalIdeaResponse, potentialHourSavingsReponse, awaitingReviewResponse, potentialBenefitResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Assess/TotalIdeas">' + totalIdeaResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Assess/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Assess/PotentialHourSavings">' + potentialHourSavingsReponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Assess/AwaitingReview">' + awaitingReviewResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);
    });

}

function clearApplyFiltersAndAdjustWorkshopDecisionView() {
    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Decision"),
        $.get("/api/tile/StageGroup/Decision/PotentialBenefit"),
        $.get("/api/tile/StageGroup/Decision/EstimatedOneTimeCost"),
        $.get("/api/tile/StageGroup/Decision/EstimatedRunningCosts"),
    ).done(function (workResponse, potentialBenefitResponse, estimatedOneTimeCostReponse, estimatedRunningCostsResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Decision/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Decision/EstimatedOneTimeCost">' + estimatedOneTimeCostReponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Decision/EstimatedRunningCosts">' + estimatedRunningCostsResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);
    });

}

function clearApplyFiltersAndAdjustWorkshopBuildView() {
    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Build"),
        $.get("/api/tile/StageGroup/Build/TotalInBuild"),
        $.get("/api/tile/StageGroup/Build/PotentialBenefit"),
        $.get("/api/tile/StageGroup/Build/TotalAtRisk"),
        $.get("/api/tile/StageGroup/Build/TotalBenefitAtRisk"),
        $.get("/api/tile/StageGroup/Build/EstimatedOneTimeCost"),
    ).done(function (workResponse, totalInBuildResponse, potentialBenefitResponse, totalAtRiskResponse, totalBenefitAtRiskResponse,
        estimatedOneTimeCostResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Build/TotalInBuild">' + totalInBuildResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Build/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Build/TotalAtRisk">' + totalAtRiskResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Build/TotalBenefitAtRisk">' + totalBenefitAtRiskResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Build/EstimatedOneTimeCost">' + estimatedOneTimeCostResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);

        setTimeout(() => {
            $.get("/api/Workshop/GetAutomationBuildPipeline", function (getSummaryResponse) {
                //var myDiv = document.getElementById("Chart.AutomationBuildPipeline");
                var myDiv = document.querySelector("[name='Chart.AutomationBuildPipeline'], [id='automationMatrix']");

                // Remove all child elements
                $(myDiv).empty();

                // Set the div's innerHTML property to the new HTML string
                myDiv.innerHTML = getSummaryResponse;
            });
        });

    });
}

function clearApplyFiltersAndAdjustWorkshopDeployedView() {
    var loaderElement = document.getElementById("quickFilter-loader");
    loaderElement.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

    $('#workshopFilterModal').modal('hide');
    $("#WorkshopContent").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');


    $.when(
        $.get("/api/Workshop/StageGroup/id/Deployed"),
        $.get("/api/tile/StageGroup/Deployed/TotalInDeployed"),
        $.get("/api/tile/StageGroup/Deployed/PotentialBenefit"),
        $.get("/api/tile/StageGroup/Deployed/PotentialHourSavings")
    ).done(function (workResponse, totalInDeployedResponse, potentialBenefitResponse, potentialHourSavingsResponse) {
        $("#WorkshopContent").html(workResponse[0]);

        var tilesViewerHtml = '<div silkflo-url="tile/StageGroup/Deployed/TotalInDeployed">' + totalInDeployedResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Deployed/PotentialBenefit">' + potentialBenefitResponse[0] + '</div>' +
            '<div silkflo-url="tile/StageGroup/Deployed/PotentialHourSavings">' + potentialHourSavingsResponse[0] + '</div>';
        $("#tilesViewer").html(tilesViewerHtml);

    });
}
