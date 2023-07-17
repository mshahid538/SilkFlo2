// Add the Department
if (SilkFlo.Pages === undefined)
    SilkFlo.Pages = {};

if (SilkFlo.Pages.Cards === undefined)
    SilkFlo.Pages.Cards = {};

if (SilkFlo.Pages.Cards.Business === undefined)
    SilkFlo.Pages.Cards.Business = {};

if (SilkFlo.Pages.Cards.Business.Department === undefined)
    SilkFlo.Pages.Cards.Business.Department = {};

if (SilkFlo.Pages.Cards.Business.Teams === undefined)
    SilkFlo.Pages.Cards.Business.Teams = {};


if (SilkFlo.Pages.Cards.Shared === undefined)
    SilkFlo.Pages.Cards.Shared = {};


if (SilkFlo.Pages.Cards.Shared.Stage === undefined)
    SilkFlo.Pages.Cards.Shared.Stage = {};

if (SilkFlo.Pages.Cards.FilterCriteria === undefined)
    SilkFlo.Pages.Cards.FilterCriteria = {};


SilkFlo.Pages.Cards.Business.Department = {
    Collection: [],


    // SilkFlo.Pages.Cards.Business.Department.GetFilters
    GetFilters: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Business.Department.GetFilters: ';
        const obj = SilkFlo.Pages.Cards.Business.Department;

        obj.Collection = [];

        const id = 'filterDepartments';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return obj.Collection;
        }

        
        const filters = element.querySelectorAll('[name="filterDepartment"]');
        SilkFlo.Pages.Cards.FilterCriteria.Elements.Departments = filters;

        // Guard Clause
        if (!filters || filters.length === 0)
        {
            console.log(`${logPrefix}There are no filterDepartments elements`);
            return obj.Collection;
        }


        const length = filters.length;
        for (let i = 0; i < length; i++)
            if (filters[i].checked)
            {
                const model = { Id: filters[i].id, Name: filters[i].getAttribute('displayName') };
                obj.Collection.push(model);
            }

        return obj.Collection;
    },

    // SilkFlo.Pages.Cards.Business.Department.GetFilterTeamElements
    GetFilterTeamElements: function ()
    {
        const collection = SilkFlo.Pages.Cards.Business.Department.GetFilters();

        const url = '/api/Business/Department/GetFilterTeamElements';
        SilkFlo.DataAccess.UpdateElementById(
            url,
            collection,
            'filterTeams',
            '',
            'POST' );
    }
};
SilkFlo.Pages.Cards.Business.Team = {
    Collection: [],

    GetFilters: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Business.Team.GetFilters: ';

        this.Collection = [];

        const id = 'filterTeams';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return this.Collection;
        }


        const filters = element.querySelectorAll('[name="filterTeam"]');
        SilkFlo.Pages.Cards.FilterCriteria.Elements.Teams = filters;

        // Guard Clause
        if (!filters || filters.length === 0)
        {
            return this.Collection;
        }


        const length = filters.length;
        for (let i = 0; i < length; i++)
            if (filters[i].checked)
            {
                const model = { Id: filters[i].id, Name: filters[i].getAttribute('displayName') };
                this.Collection.push(model);
            }

        return this.Collection;
    }
};


SilkFlo.Pages.Cards.Shared.SubmissionPath = {
    Collection: [],

    GetFilters: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Shared.SubmissionPath.GetFilters: ';


        this.Collection = [];

        const id = 'filterSubmissionPaths';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return this.Collection;
        }

        const filters = element.querySelectorAll('[name="filterSubmissionPath"]');
        SilkFlo.Pages.Cards.FilterCriteria.Elements.SubmissionPaths = filters;

        // Guard Clause
        if (!filters || filters.length === 0)
        {
            console.log('There are no filterSubmissionPath elements');
            return this.Collection;
        }


        const length = filters.length;
        for (let i = 0; i < length; i++)
            if (filters[i].checked)
            {
                const model = { Id: filters[i].id, Name: filters[i].getAttribute('displayName') };
                this.Collection.push(model);
            }

        return this.Collection;
    }
};


SilkFlo.Pages.Cards.Shared.Version = {
    Collection: [],

    GetFilters: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Shared.Version.GetFilters: ';


        this.Collection = [];

        const id = 'filterVersions';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return this.Collection;
        }

        const filters = element.querySelectorAll('[name="filterVersion"]');
        SilkFlo.Pages.Cards.FilterCriteria.Elements.Versions = filters;

        // Guard Clause
        if (!filters || filters.length === 0)
        {
            console.log('There are no filterVersion elements');
            return this.Collection;
        }


        const length = filters.length;
        for (let i = 0; i < length; i++)
            if (filters[i].checked)
            {
                const model = { Id: filters[i].id, Name: filters[i].getAttribute('displayName') };
                this.Collection.push(model);
            }

        return this.Collection;
    }
};


SilkFlo.Pages.Cards.Shared.Stage = {
    Collection: [],

    GetCollection: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Shared.Stage.GetCollection: ';


        const obj = SilkFlo.Pages.Cards.Shared.Stage;
        obj.Collection = [];

        const id = 'filterStages';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return obj.Collection;
        }


        const filterStages = element.querySelectorAll('[name="filterStage"]');

        // Guard Clause
        if (!filterStages || filterStages.length === 0)
        {
            console.log('There are no filterStages elements');
            return obj.Collection;
        }


        const length = filterStages.length;
        for (let i = 0; i < length; i++)
            if (filterStages[i].checked)
            {
                const model = { Id: filterStages[i].id, Name: filterStages[i].getAttribute('displayName') };
                obj.Collection.push(model);
            }

        return obj.Collection;
    },

    GetFilters: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Shared.Stage.GetFilters: ';
        const obj = SilkFlo.Pages.Cards.Shared.Stage;
        obj.Collection = [];

        const id = 'filterStages';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return obj.Collection;
        }


        const filters = element.querySelectorAll('[name="filterStage"]');
        SilkFlo.Pages.Cards.FilterCriteria.Elements.Stages = filters;

        // Guard Clause
        if (!filters || filters.length === 0)
        {
            console.log('There are no filterStage elements');
            return obj.Collection;
        }


        const length = filters.length;
        for (let i = 0; i < length; i++)
            if (filters[i].checked)
            {
                const model = { Id: filters[i].id, Name: filters[i].getAttribute('displayName') };
                obj.Collection.push(model);
            }

        return obj.Collection;
    },


    // SilkFlo.Pages.Cards.Shared.Stage.GetStatuses
    GetStatuses: function ()
    {
        const collection = SilkFlo.Pages.Cards.Shared.Stage.GetCollection();

        const url = '/api/Shared/Stage/GetStatuses';
        SilkFlo.DataAccess.UpdateElementById(
            url,
            collection,
            'filterStatuses',
            '',
            'POST');
    }
};


SilkFlo.Pages.Cards.Shared.Status = {
    Collection: [],

    GetFilters: function ()
    {
        const logPrefix = 'SilkFlo.Pages.Cards.Shared.Status.GetFilters: ';

        this.Collection = [];

        const id = 'filterStatuses';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return this.Collection;
        }


        const filters = element.querySelectorAll('[name="filterStatus"]');
        SilkFlo.Pages.Cards.FilterCriteria.Elements.Statuses = filters;

        // Guard Clause
        if (!filters || filters.length === 0)
            return this.Collection;


        const length = filters.length;
        for (let i = 0; i < length; i++)
            if (filters[i].checked)
            {
                const model = { Id: filters[i].id, Name: filters[i].getAttribute('displayName') };
                this.Collection.push(model);
            }

        return this.Collection;
    }
};


SilkFlo.Pages.Cards.FilterCriteria = {

    Model: {
        IsDeployedOnly: false,
        SortById: null,
        FilterSearch: "",
        Departments: [],
        Teams: [],
        SubmissionPaths: [],
        Versions: [],
        Stages: [],
        Statuses: [],
        Count: 0,
        PageIndex: 0,
        LastPage: 0
    },

    Elements: {
        TxtFilterSearch: '',
        Departments: [],
        Teams: [],
        SubmissionPaths: [],
        Versions: [],
        Stages: [],
        Statuses: []
    },

    GetModel: function ()
    {
        const isDeployedOnly = document.getElementById('IsDeployedOnly');
        this.Model.IsDeployedOnly = isDeployedOnly.value;

        const sortById = document.getElementById('SortBy');
        this.Model.SortById = sortById.value;

        this.Elements.TxtFilterSearch = document.getElementById('filterSearch');
        this.Model.FilterSearch = this.Elements.TxtFilterSearch.value;

        this.Model.Departments = SilkFlo.Pages.Cards.Business.Department.GetFilters();
        this.Model.Teams = SilkFlo.Pages.Cards.Business.Team.GetFilters();
        this.Model.SubmissionPaths = SilkFlo.Pages.Cards.Shared.SubmissionPath.GetFilters();
        this.Model.Versions = SilkFlo.Pages.Cards.Shared.Version.GetFilters();
        this.Model.Stages = SilkFlo.Pages.Cards.Shared.Stage.GetFilters();
        this.Model.Statuses = SilkFlo.Pages.Cards.Shared.Status.GetFilters();

        const elementPageIndex = document.getElementById('pageIndex');
        if (elementPageIndex)
            this.Model.PageIndex = elementPageIndex.value;

        const elementLastPage = document.getElementById('lastPage');
        if (elementLastPage)
            this.Model.LastPage = elementLastPage.value;
    },


    // SilkFlo.Pages.Cards.FilterCriteria.ToggleFilters
    ToggleFilters: function (event, filterName, func = null)
    {
        const logPrefix = 'SilkFlo.Pages.Cards.FilterCriteria.ToggleFilters: ';

        // Guard Clause
        if (!event) {
            console.log(`${logPrefix}event parameter missing`);
            return;
        }

        // Guard Clause
        if (!filterName) {
            console.log(`${logPrefix}filterName parameter missing`);
            return;
        }

        event.stopPropagation();

        const element = event.target;
        const elements = document.querySelectorAll('[name="' + filterName + '"]');
        const length = elements.length;
        for (let i = 0; i < length; i++)
            elements[i].checked = element.checked;

        if (element.checked)
            element.innerHtml = '<i>Select None</i>';
        else
            element.innerHtml = '<i>Select All</i>';


        if (func)
            func();
    },

    //SilkFlo.Pages.Cards.FilterCriteria.UnSelectFilter('');
    UnSelectFilter: function (event, name)
    {
        const logPrefix = 'SilkFlo.Pages.Cards.FilterCriteria.UnSelectFilter: ';

        // Guard Clause
        if (!event) {
            console.log(`${logPrefix}event parameter missing`);
            return;
        }

        // Guard Clause
        if (!name) {
            console.log(`${logPrefix}name parameter missing`);
            return;
        }

        event.stopPropagation();
        const element = document.getElementById(name);
        element.checked = false;
    },

    //SilkFlo.Pages.Cards.FilterCriteria.DeselectAll('');
    DeselectAll: function ()
    {
        this.GetModel();

        this.Elements.TxtFilterSearch.value = '';

        let elements = SilkFlo.Pages.Cards.FilterCriteria.Elements.Departments;
        let length = elements.length;
        for (let i = 0; i < length; i++)
        {
            elements[i].checked = false;
        }

        elements = SilkFlo.Pages.Cards.FilterCriteria.Elements.Teams;
        length = elements.length;
        for (let i = 0; i < length; i++)
        {
            elements[i].checked = false;
        }

        elements = SilkFlo.Pages.Cards.FilterCriteria.Elements.SubmissionPaths;
        length = elements.length;
        for (let i = 0; i < length; i++)
        {
            elements[i].checked = false;
        }

        elements = SilkFlo.Pages.Cards.FilterCriteria.Elements.Versions;
        length = elements.length;
        for (let i = 0; i < length; i++)
        {
            elements[i].checked = false;
        }

        elements = SilkFlo.Pages.Cards.FilterCriteria.Elements.Stages;
        length = elements.length;
        for (let i = 0; i < length; i++)
        {
            elements[i].checked = false;
        }

        elements = SilkFlo.Pages.Cards.FilterCriteria.Elements.Statuses;
        length = elements.length;
        for (let i = 0; i < length; i++)
        {
            elements[i].checked = false;
        }


        const toggleFilterDepartment = document.getElementById('toggleFilterDepartment');
        toggleFilterDepartment.checked = false;

        const toggleFilterTeam = document.getElementById('toggleFilterTeam');
        if (toggleFilterTeam !== null)
        {
            toggleFilterTeam.checked = false;
        }

        const toggleFilterSubmissionPath = document.getElementById('toggleFilterSubmissionPath');
        toggleFilterSubmissionPath.checked = false;

        const toggleFilterVersion = document.getElementById('toggleFilterVersion');
        toggleFilterVersion.checked = false;

        const toggleFilterStage = document.getElementById('toggleFilterStage');
        toggleFilterStage.checked = false;

        const toggleFilterStatus = document.getElementById('toggleFilterStatus');
        if (toggleFilterStatus !== null)
        {
            toggleFilterStatus.checked = false;
        }

        const filterTeams = document.getElementById('filterTeams');
        filterTeams.innerHTML = 'No Teams Present';

        const filterStatuses = document.getElementById('filterStatuses');
        filterStatuses.innerHTML = 'No Statuses Present';

    },


    // SilkFlo.Pages.Cards.FilterCriteria.Close
    Close: function (event)
    {
        const logPrefix = 'SilkFlo.Pages.Cards.FilterCriteria.Close: ';
        console.log(logPrefix);
        const id = 'filter';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        if (!element.Card)
        {
            console.log(`${logPrefix}Filter.Card missing`);
            return;
        }


        if (event.target !== element.Card)
            element.Card.style.display = 'none';
    },

    // SilkFlo.Pages.Cards.FilterCriteria.Run
    Run: function (event)
    {
        SilkFlo.Pages.Cards.FilterCriteria.GetModel();

        $.ajax({
            contentType: 'application/json',
            type: "POST",
            url: "/api/Business/Idea/ValidateFiltered",
            data: JSON.stringify(this.Model),
            success: function (response) {
                console.log("Hello: ", response);

                if (response.isFilterByTextApplicable) {
                    console.log("this.Model", response.payload)
                    const url = '/api/Business/Idea/GetFiltered';
                        SilkFlo.DataAccess.UpdateElementById(
                            url,
                            response.payload,
                            'ideaCards',
                            '',
                            'POST',
                            (res) => { });
                    var filterSearchText = document.getElementById('filterSearch').value;
                    document.getElementById('filterSearchInvalidFeedback').innerHTML = "";
                    SilkFlo.Pages.Cards.FilterCriteria.Close(event);
                }
                else {
                    var filterSearchText = document.getElementById('filterSearch').value;
                    document.getElementById('filterSearchInvalidFeedback').innerHTML = "'" + filterSearchText + "' not found in text fields, you may try using the below filter options";
                }

            },
            failure: function (response) {
            },
            error: function (response) {
            }
        });

        //SilkFlo.Pages.Cards.FilterCriteria.Close(event);
    },


    // SilkFlo.Pages.Cards.FilterCriteria.Sort
    Sort: function (event)
    {
        const element = event.target;
        const value = element.getAttribute('value');

        const sortBy = document.getElementById('SortBy');
        sortBy.value = value;

        const sortByText = document.getElementById('SortByText');
        sortByText.innerHTML = `Sorted By: ${element.innerHTML}`;


        const sortList = document.getElementById('sort-List');
        const sorts = sortList.querySelectorAll('[name="selector"]');

        const length = sorts.length;
        for (let i = 0; i < length; i++)
        {
            sorts[i].style.background = '';
        }

        element.style.background = 'var(--bs-gray-even-lightest)';

        SilkFlo.Pages.Cards.FilterCriteria.Run(event);
    },


    // SilkFlo.Pages.Cards.FilterCriteria.SelectPage
    SelectPage: function (pageIndex, lastPage)
    {
        const elementPageIndex = document.getElementById('pageIndex');
        elementPageIndex.value = pageIndex;

        const elementLastPage = document.getElementById('lastPage');
        elementLastPage.value = lastPage;

        this.Run();
    }
};