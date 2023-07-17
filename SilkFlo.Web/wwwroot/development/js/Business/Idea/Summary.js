// Add the model
if (SilkFlo.Business === undefined)
    SilkFlo.Business = {};

if (SilkFlo.Business.Idea === undefined)
    SilkFlo.Business.Idea = {};


SilkFlo.Business.Idea.Summary = {

    DeleteConfirmation: null,
    IdeaGUID: null,

    GetParent: function ()
    {
        const id = 'Detail_BusinessIdea';
        const parent = document.getElementById(id);
        if (!parent) {
            const logPrefix = 'SilkFlo.Business.Idea.Summary.GetParent: ';
            console.log(`${logPrefix}Element with id "${id}" missing`);
            return null;
        }

        return parent;
    },

    // SilkFlo.Business.Idea.Summary.ContextMenu
    ContextMenu:
    {
        Width: null,
        Height: null,
        Left: null,
        Top: null,

        // SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu
        GetMenu: function ()
        {
            const id = 'idea_summary_menu';
            const menu = document.getElementById(id);

            // Guard Clause
            if (!menu)
            {
                return null;
            }

            return menu;
        },

        // SilkFlo.Business.Idea.Summary.ContextMenu.Show
        Show: function (menuItem, ideaGuid, isDraft)
        {
            SilkFlo.Business.Idea.Summary.IdeaGUID = ideaGuid;
            SilkFlo.Business.Idea.Summary.RowButton.GetGeometry(menuItem);
            SilkFlo.Business.Idea.Summary.ContextMenu.Get();

            const windowHeight = window.innerHeight;


            // Choose vertical position;
            const deltaX = (SilkFlo.Business.Idea.Summary.RowButton.Left + SilkFlo.Business.Idea.Summary.RowButton.Width - SilkFlo.Business.Idea.Summary.ContextMenu.Left) + 6;
            let deltaY = (SilkFlo.Business.Idea.Summary.ContextMenu.Top - SilkFlo.Business.Idea.Summary.RowButton.Top) * -1;

            if (SilkFlo.Business.Idea.Summary.RowButton.Top + SilkFlo.Business.Idea.Summary.ContextMenu.Height > windowHeight - 42)
            {
                //Set menu top
                deltaY -= SilkFlo.Business.Idea.Summary.ContextMenu.Height - SilkFlo.Business.Idea.Summary.RowButton.Height;
            }

            const matrix = `matrix( 1, 0, 0, 1, ${deltaX}, ${deltaY})`;


            const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
            if (!menu)
                return;

            menu.style.display = 'block';
            menu.style.transform = matrix;
            const manageStages = menu.querySelector('[name="ManageStages"]');


            if (manageStages)
            {
                if (isDraft)
                {
                    manageStages.style.display = 'none';
                }
                else
                {
                    manageStages.style.display = 'block';
                }
            }
        },

        // SilkFlo.Business.Idea.Summary.ContextMenu.Hide
        Hide: function (event)
        {
            const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
            if (!menu)
                return;

            window.removeEventListener('mouseup', SilkFlo.Business.Idea.Summary.ContextMenu.Hide);
            setTimeout(
                function ()
                {
                    if (event.target !== menu
                        && event.target.parentNode !== menu) {
                        menu.style.display = 'none';
                    }
                },
                0);
        },


        // SilkFlo.Business.Idea.Summary.ContextMenu.HideDeleteConfirmation
        HideDeleteConfirmation: function ()
        {
            const id = 'DeleteConfirmation';
            const deleteConfirmation = document.getElementById(id);

            // Guard Clause
            if (!deleteConfirmation)
            {
                const logPrefix = 'SilkFlo.Business.Idea.Summary.Delete: ';
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            deleteConfirmation.classList.remove('show');
            deleteConfirmation.style.display = 'none';
        },



        // SilkFlo.Business.Idea.Summary.ContextMenu.Get
        Get: function ()
        {
            const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
            if (!menu)
                return;


            window.removeEventListener('mouseup', SilkFlo.Business.Idea.Summary.ContextMenu.Hide);
            window.addEventListener('mouseup', SilkFlo.Business.Idea.Summary.ContextMenu.Hide);

            menu.style.transform = '';
            menu.style.display = 'block';

            // Store the menu geometry
            const rect = menu.getBoundingClientRect();

            if (SilkFlo.Business.Idea.Summary.ContextMenu.Width === null)
            {
                SilkFlo.Business.Idea.Summary.ContextMenu.Width = menu.clientWidth;
            }

            if (SilkFlo.Business.Idea.Summary.ContextMenu.Height === null)
            {
                SilkFlo.Business.Idea.Summary.ContextMenu.Height = menu.clientHeight;
            }

            SilkFlo.Business.Idea.Summary.ContextMenu.Left = rect.left;
            SilkFlo.Business.Idea.Summary.ContextMenu.Top = rect.top;
        }
    },

    RowButton:
    {
        Width: null,
        Height: null,
        Left: null,
        Top: null,

        GetGeometry: function (menuItem)
        {
            const rect = menuItem.getBoundingClientRect();

            if (SilkFlo.Business.Idea.Summary.RowButton.Width === null)
                SilkFlo.Business.Idea.Summary.RowButton.Width = rect.width;

            if (SilkFlo.Business.Idea.Summary.RowButton.Height === null)
                SilkFlo.Business.Idea.Summary.RowButton.Height = rect.height;

            SilkFlo.Business.Idea.Summary.RowButton.Left = rect.left;
            SilkFlo.Business.Idea.Summary.RowButton.Top = rect.top;
        }
    },


    // SilkFlo.Business.Idea.Summary.ManageStageAndStatus
    ManageStageAndStatus: function ()
    {
        Delaney.UI.ToolTip.Hide();

        SilkFlo.VieModels.Business.ManageStageAndStatus.Get(
            SilkFlo.Business.Idea.Summary.IdeaGUID,
            SilkFlo.Business.Idea.Summary.UpdateDashboard);


        const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
        if (!menu)
            return;

        menu.style.display = 'none';
    },


    // SilkFlo.Business.Idea.Summary.UpdateDashboard
    UpdateDashboard: function ()
    {
        const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
        if (!menu)
            return;

        menu.style.display = 'none';

        SilkFlo.DataAccess.UpdateElementFromAttribute(`StageCell_${SilkFlo.Business.Idea.Summary.IdeaGUID}`);
        SilkFlo.DataAccess.UpdateElementFromAttribute(`StatusCell_${SilkFlo.Business.Idea.Summary.IdeaGUID}`);
        SilkFlo.DataAccess.UpdateElementFromAttribute('totalIdeas');
        SilkFlo.DataAccess.UpdateElementFromAttribute('totalInBuild');
        SilkFlo.DataAccess.UpdateElementFromAttribute('totalDeployed');

        const workshopContentElement = document.getElementById('WorkshopContent');

        if (workshopContentElement)
        {
            let url = workshopContentElement.getAttribute('url');

            if (!url)
            {
                const logPrefix = 'SilkFlo.Business.Idea.Summary.UpdateDashboard: ';
                console.log(logPrefix + 'url attribute missing');
                return;
            }


            const apiUrl = `/api/Workshop/StageGroup/Id/${url}`;
            url = `/workshop/${url}`;



            SilkFlo.DataAccess.UpdateElement(
                apiUrl,
                '',
                workshopContentElement,
                url);
        }
    },

    // SilkFlo.Business.Idea.Summary.DetailById
    DetailById: function (ideaGuid)
    {
        // Guard Clause
        if (!ideaGuid) {
            const logPrefix = 'SilkFlo.Business.Idea.Summary.DetailById: ';
            console.log(`${logPrefix}ideaGuid parameter missing`);
            return;
        }

        SilkFlo.Business.Idea.Summary.IdeaGUID = ideaGuid;
        SilkFlo.Business.Idea.Summary.Detail ();
    },

    // SilkFlo.Business.Idea.Summary.Detail
    Detail: function ()
    {
        const url = `/Idea/Detail/${SilkFlo.Business.Idea.Summary.IdeaGUID}`;

        const contentElement = document.getElementById('content');

        // Guard Clause
        if (!contentElement)
        {
            const logPrefix = 'SilkFlo.Business.Idea.Summary.Detail: ';
            console.log(logPrefix + 'element with id "content" missing');
            return;
        }


        Delaney.UI.ToolTip.Hide();

        const urlApi = `/api/Idea/Detail/${SilkFlo.Business.Idea.Summary.IdeaGUID}`;
        SilkFlo.DataAccess.UpdateElement(urlApi, null, contentElement, url, 'GET', SilkFlo.Business.Idea.Summary.DetailLoaded);
    },

    // SilkFlo.Business.Idea.Summary.DetailLoaded
    DetailLoaded: function ()
    {
        SilkFlo.SideBar.SetMenu();

        const parent = SilkFlo.Business.Idea.Summary.GetParent();
        if (!parent)
            return;

        SilkFlo.SVGTools.AnimatePaths(parent);

        const rate = parent.querySelector('[name="Business.Idea.Rating"]');
        if (rate)
        {
            DelaneysSlider_Main(rate);
        }


    },

    // SilkFlo.Business.Idea.Summary.Edit
    Edit: function ()
    {
        const url = `/Idea/Edit/${SilkFlo.Business.Idea.Summary.IdeaGUID}`;
        //window.location.href = url;

        const contentElement = document.getElementById('content');

        // Guard Clause
        if (!contentElement)
        {
            const logPrefix = 'SilkFlo.Business.Idea.Summary.Edit: ';
            console.log(logPrefix + 'element with id "content" missing');
            return;
        }


        Delaney.UI.ToolTip.Hide();

        const urlApi = `/api/Idea/Edit/${SilkFlo.Business.Idea.Summary.IdeaGUID}`;
        SilkFlo.DataAccess.UpdateElement(urlApi, null, contentElement, url, 'GET', SilkFlo.SideBar.SetMenu);
    },


    // SilkFlo.Business.Idea.Summary.Delete
    Delete: function ()
    {
       // Show are you sure message


        const id = 'DeleteConfirmation';
        const deleteConfirmation = document.getElementById(id);

        // Guard Clause
        if (!deleteConfirmation)
        {
            const logPrefix = 'SilkFlo.Business.Idea.Summary.Delete: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        deleteConfirmation.classList.add('show');
        deleteConfirmation.style.display = 'block';

        const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
        if (!menu)
            return;

        menu.style.display = 'none';
    },

    // SilkFlo.Business.Idea.Summary.Delete_No
    Delete_No: function () {
        SilkFlo.Business.Idea.Summary.ContextMenu.HideDeleteConfirmation();

        const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu();
        if (!menu)
            return;

        menu.style.display = 'none';
    },


    // SilkFlo.Business.Idea.Summary.Delete_Yes
    Delete_Yes: function () {
        SilkFlo.Business.Idea.Summary.ContextMenu.HideDeleteConfirmation();

        const url = '/api/business/Idea/Delete/' + SilkFlo.Business.Idea.Summary.IdeaGUID;

        const http = new XMLHttpRequest();
        http.open('DELETE', url, true);
        http.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
        http.onreadystatechange = function () {
            return function () {
                if (http.readyState === XMLHttpRequest.DONE
                    && http.status === 200) {

                    const row = document.getElementById(SilkFlo.Business.Idea.Summary.IdeaGUID);
                    row.parentNode.removeChild(row);
                    SilkFlo.DataAccess.UpdateElementFromAttribute('totalIdeas');
                    SilkFlo.DataAccess.UpdateElementFromAttribute('totalInBuild');
                    SilkFlo.DataAccess.UpdateElementFromAttribute('totalDeployed');
                }
            };
        }(this);
        http.send();

        const menu = SilkFlo.Business.Idea.Summary.ContextMenu.GetMenu ();
        if (!menu)
            return;

        menu.style.display = 'none';
    },


    // SilkFlo.Business.Idea.Summary.UpdateStageValue
    UpdateStageValue: function (
        target,
        value)
    {
        const logPrefix = 'SilkFlo.Business.Idea.Summary.UpdateStageValue: ';

        // Guard Clauses
        if (!target)
        {
            console.log(logPrefix + 'target parameter missing');
            return;
        }

        // Guard Clauses
        if (!value) {
            console.log(logPrefix + 'value parameter missing');
            return;
        }

        target.innerHTML = value;
        value = value.toLowerCase ();
        target.Value = value;
        target.setAttribute (
            'value',
            value);

        target.ResizeColumnHeader();
    },


    // SilkFlo.Business.Idea.Summary.UpdateStatusValue
    UpdateStatusValue: function (
                           target,
                           value)
    {
        const logPrefix = 'SilkFlo.Business.Idea.Summary.UpdateStatusValue: ';

        // Guard Clauses
        if (!target) {
            console.log(logPrefix + 'target parameter missing');
            return;
        }

        // Guard Clauses
        if (!value) {
            console.log(logPrefix + 'value parameter missing');
            return;
        }

        const obj = JSON.parse(value);

        if (!obj)
        {
            console.log(logPrefix + 'Not an obj');
            return;
        }

        target.innerHTML = obj.content;
        target.Value = value;
        target.setAttribute (
            'value',
            value);

        target.ResizeColumnHeader ();
    }
};


SilkFlo.Dashboard = {

    // SilkFlo.Dashboard.GetParent
    GetParent: function () {
        const id = 'dashboard';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Workshop.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },

    Chart: {
        // SilkFlo.Dashboard.Chart.AutomationProgramPerformance
        AutomationProgramPerformance: {
            // SilkFlo.Dashboard.Chart.AutomationProgramPerformance.FilterYear
            FilterYear: function (element) {
                const logPrefix = 'SilkFlo.Dashboard.Chart.AutomationProgramPerformance.FilterYear: ';

                // Guard Clause
                if (!element) {
                    console.log(`${logPrefix}element parameter missing`);
                    return;
                }


                const parent = SilkFlo.Dashboard.GetParent();
                if (!parent)
                    return;


                const name = 'Chart.AutomationProgramPerformance';
                const target = parent.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (!target) {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                    return;
                }


                const url = `/api/Dashboard/GetAutomationProgramPerformance/year/${element.value}`;

                console.log(url);

                SilkFlo.DataAccess.UpdateElement(
                    url,
                    null,
                    target);
            }
        } 
    }
};