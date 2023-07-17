SilkFlo.SideBar = {

    // SilkFlo.SideBar.AfterResize
    AfterResize: function ()
    {
        const id = 'sidebar';
        const sidebar = document.getElementById (id);

        // Guard Clause
        if (!sidebar)
        {
            const logPrefix = 'SilkFlo.SideBar.AfterResize: ';
            console.log (`${logPrefix}element with id "${id}" was not found`);
            return;
        }


        SilkFlo.SideBar.SetSideBarDeltaX (sidebar);
        SilkFlo.SideBar.SetMenu();


        sidebar.removeEventListener ('transitionend',
            SilkFlo.SideBar.AfterResize);
    },



    // Rotate the drop down arrow
    ToggleDownArrow: function (element, arrowId)
    {
        const logPrefix = 'SilkFlo.SideBar.ToggleDownArrow: ';

        // Guard Clause
        if (!element)
        {
            console.log (`${logPrefix}element parameter missing`);
            return;
        }


        const arrow = document.getElementById(arrowId);

        // Guard Clause
        if (!arrow)
        {
            console.log(`${logPrefix}element with id "${arrowId}" missing`);
            return;
        }


        if (element.classList.contains ('collapsed'))
            arrow.classList.add ('rotate180');
        else
            arrow.classList.remove ('rotate180');
    },

    ToggleMenu: function (element)
    {
        const logPrefix = 'SilkFlo.SideBar.ToggleMenu: ';


        // Guard Clause
        if (!element)
        {
            console.log (`${logPrefix}element parameter is missing`);
            return;
        }


        const menu = document.getElementById ('menu');

        // Guard Clause
        if (!menu)
        {
            console.log (`${logPrefix}element with id "menu" was not found`);
            return;
        }

        // Get all the menu items
        const menuItems = menu.getElementsByTagName ('a');
        for (const menuItem of menuItems)
        {
            const isSelected = menuItem === element;

            const parent = menuItem.parentElement;
            // Set of unset active
            if (isSelected)
                parent.classList.add ('active');
            else
                parent.classList.remove ('active');
        }

        SilkFlo.SideBar.PositionSelectBar (element);
    },


    PositionSelectBar: function (selectedElement)
    {
        const logPrefix = 'SilkFlo.SideBarPositionSelectBar: ';

        if (!selectedElement)
        {
            console.log (`${logPrefix}selectedElement parameter is missing`);
            return;
        }

        let deltaY = 0;

        // Get Position
        if (selectedElement.id !== 'SideBar.Dashboard.Container')
        {
            const container = document.getElementById ('SideBar.Dashboard.Container');

            if (!container)
            {
                console.log (`${logPrefix}element with id "SideBar.Dashboard.Container" not found`);
                return;
            }

            const selectBarTop = container.getBoundingClientRect().top;
            const selectedTop = selectedElement.getBoundingClientRect().top;
            deltaY = selectedTop - selectBarTop;
        }


        // Get Matrix
        const matrix = `matrix( 1, 0, 0, 1, 1, ${deltaY})`;
        // matrix( scaleX(), skewY(), skewX(), scaleY(), translateX(), translateY() )


        // Apply Matrix
        const selectBar = document.getElementById ('selectBar');

        if (selectBar)
            selectBar.style.transform = matrix;
    },



    // SilkFlo.SideBar.SetMenu
    SetMenu: function ()
    {
        // Get the url from the address bar
        const url = window.location.pathname;

        // Update the side bar based on the URL value.
        let id = '';
        if (url === '/')
            id = 'SideBar.Dashboard.Container';
        else if (url.toLowerCase().indexOf ('/dashboard') > -1)
            id = 'SideBar.Dashboard.Container';
        else if (url.toLowerCase().indexOf('/agencydashboard') > -1)
            id = 'SideBar.AgencyDashboard.Container';
        else if (url.toLowerCase().indexOf ('/explore/ideas') > -1)
            id = 'SideBar.Explore.Ideas.Container';
        else if (url.toLowerCase().indexOf ('/explore/automations') > -1)
            id = 'SideBar.Explore.Automations.Container';
        else if (url.toLowerCase().indexOf ('/explore/people') > -1)
            id = 'SideBar.Explore.People.Container';
        else if (url.toLowerCase().indexOf ('/explore/leaderboard') > -1)
            id = 'SideBar.Explore.Leaderboard.Container';
        else if (url.toLowerCase().indexOf ('/explore/component') > -1)
            id = 'SideBar.Explore.Component.Container';
        else if (url.toLowerCase().indexOf ('/workshop') > -1)
            id = 'SideBar.Workshop.Container';
        else if (url.toLowerCase().indexOf ('/administration') > -1)
            id = 'SideBar.Administration.Container';
        else if (url.toLowerCase().indexOf ('/application/settings') > -1)
            id = 'SideBar.SilkFloSettings.Container';
        else if (url.toLowerCase().indexOf('/application/prospects') > -1)
            id = 'SideBar.SilkFloProspects.Container';

        // Setting Tenant
        else if (url.toLowerCase().indexOf ('/settings/tenant/account') > -1)
            id = 'SideBar.Settings.Tenant.Account.Container';
        else if (url.toLowerCase().indexOf ('/settings/tenant/people') > -1)
            id = 'SideBar.Settings.Tenant.People.Container';
        else if (url.toLowerCase().indexOf ('/settings/tenant/platform-setup') > -1)
            id = 'SideBar.Settings.Tenant.PlatformSetup.Container';
        // Setting Agent
        else if (url.toLowerCase().indexOf ('/settings/agency/account') > -1)
            id = 'SideBar.Settings.Agency.Account.Container';
        else if (url.toLowerCase().indexOf ('/settings/agency/people') > -1)
            id = 'SideBar.Settings.Agency.People.Container';
        else if (url.toLowerCase().indexOf ('/settings/agency/tenants') > -1)
            id = 'SideBar.Settings.Agency.Tenants.Container';
        else if (url.toLowerCase().indexOf ('/settings/agency/platformsetup') > -1)
            id = 'SideBar.Settings.Agency.PlatformSetup.Container';
        else if (url.toLowerCase().indexOf ('/settings') > -1)
            id = 'SideBar.Settings.Tenant.Container';
        else if (url.toLowerCase().indexOf ('/agencysettings') > -1)
            id = 'SideBar.Settings.Agency.Container';
        else
            id = '';

        SilkFlo.SideBar.FormatMenu (id);
    },

    // SilkFlo.SideBar.ClearMenu
    ClearMenu: function ()
    {
        SilkFlo.SideBar.FormatMenu ('');
    },

    FormatMenu: function (id)
    {
        const logPrefix = 'SilkFlo.SideBar.FormatMenu: ';


        // Hide SelectBar if there is no id
        const selectBar = document.getElementById ('selectBar');
        if (selectBar)
        {
            if (id)
                selectBar.style.display = 'block';
            else
                selectBar.style.display = 'none';
        }


        // Assign the active class to the <li> parent element.
        let elementId = 'menu';
        const menu = document.getElementById(elementId);

        if (!menu)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }

        const menuItems = menu.getElementsByTagName ('a');


        for (const menuItem of menuItems)
        {
            if (menuItem.parentNode === undefined)
                continue;


            if (menuItem.id === 'SideBar.Explore'
                && id.indexOf('SideBar.Explore') > -1)
            {
                if (menuItem.classList.contains ( 'collapsed' ))
                {
                    const menuId = menuItem.getAttribute('silkflo-sub-menu-id');

                    // Guard Clause
                    if (!menuId)
                    {
                        console.log(logPrefix +
                            'silkflo-sub-menu-id attribute is missing for element with id = ' +
                            menuItem.id);
                        return;
                    }


                    const subMenu = document.getElementById(menuId);

                    // Guard Clause
                    if (!subMenu)
                    {
                        console.log(`${logPrefix}element with id "${menuId}" was not found`);
                        return;
                    }



                    menuItem.setAttribute(
                        'aria-expanded',
                        'true');

                    subMenu.classList.add('show');
                }
            }

            if (menuItem.id === 'SideBar.Settings'
                && id.indexOf('SideBar.Settings') > -1)
            {
                if (menuItem.classList.contains('collapsed'))
                {
                    const menuId = menuItem.getAttribute('silkflo-sub-menu-id');

                    // Guard Clause
                    if (!menuId)
                    {
                        console.log(logPrefix +
                            'silkflo-sub-menu-id attribute is missing for element with id = ' +
                            menuItem.id);
                        return;
                    }


                    const subMenu = document.getElementById(menuId);

                    // Guard Clause
                    if (!subMenu)
                    {
                        console.log(`${logPrefix}element with id "${menuId}" was not found`);
                        return;
                    }



                    menuItem.setAttribute(
                        'aria-expanded',
                        'true');

                    subMenu.classList.add('show');
                }
            }


            const parent = menuItem.parentNode;
            if (parent.id === id)
            {
                parent.classList.add ('active');
                SilkFlo.SideBar.PositionSelectBar (parent);
            }
            else
            {
                parent.classList.remove ('active');
                const parentMenuId = menuItem.getAttribute ('silkflo-parent-menu-id');

                if (parentMenuId
                && parent.id !== parentMenuId)
                    menuItem.classList.add ('collapsed');
            }
        }
    },

    SetSideBarDeltaX: function (sidebar)
    {
        if (!sidebar)
        {
            const logPrefix = 'SilkFlo.SideBar.SetSideBarDeltaX: ';
            console.log (`${logPrefix}sidebar parameter is missing.`);
            return;
        }

        const selectBarLeft = 20.5;
        const sidebarNarrow = SilkFlo.GetCookie ('sidebarNarrow');
        let deltaX = selectBarLeft;

        if (sidebarNarrow.length === 0)
        {
            const selectBarWidth = 4;
            deltaX = deltaX + selectBarWidth;
        }


        SilkFlo.SideBar.SelectBarDeltaX = deltaX;
    },

    SetSideBarZeroY: function ()
    {
        const selectBar = document.getElementById ('selectBar');

        if (!selectBar)
            return;

        const rect = selectBar.getBoundingClientRect();
        SilkFlo.SideBar.SelectBarZeroY = rect.top;
    },

    // SilkFlo.SideBar.SetWorkspace
    SetWorkspace: function ()
    {
        const workspaceSelector = document.getElementById('workspaceSelector');

        if (!workspaceSelector)
            return;

        const url = '/api/Business/Client/GetWorkspaceSelector';
        SilkFlo.DataAccess.UpdateElement (url,
            '',
            workspaceSelector,
            '',
            'GET',
            SilkFlo.SideBar.NavigationFirstLoad_Callback);
    },

    // Parameters:
    // element        - The calling element
    //  url           - This will be prefixed with '/api'
    //  callbackFunction - The function to run after the content is returned.
    // SilkFlo.SideBar.OnClick
    OnClick: function (
                 url,
                 callbackFunction)
    {
        const logPrefix = 'SilkFlo.SideBar.OnClick: ';

        // Guard Clause
        if (!url)
        {
            console.log (`${logPrefix}url parameter is missing`);
            return;
        }


        Delaney.UI.ToolTip.Hide();

        // Add a '/' prefix if required.
        if (url.charAt (0) !== '/')
            url = `/${url}`;

        if (window.location.href.indexOf (url) === -1)
            SilkFlo.SideBar.ReturnURL = window.location.href;


        const id = 'content';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        

        // Get content
        SilkFlo.DataAccess.UpdateElement (
            `/api${url}`,
            '',
            element,
            url,
            'GET',
            () =>
            {
                SilkFlo.SideBar.SetMenu();
                if (callbackFunction)
                    callbackFunction();
            });
    },


    SubmitIdea: function (menuId, containerId, position)
    {
        SilkFlo.SideBar.SubmitIdeaMenu.Show (menuId,
            containerId,
            position);
    },

    SubmitIdeaMenu:
    {
        Menu: null,

        Show: function (menuId, containerId, position)
        {
            if (SilkFlo.SideBar.SubmitIdeaMenu.Menu === null)
            {
                SilkFlo.SideBar.SubmitIdeaMenu.Menu = document.getElementById ('submitIdeaMenu');
                window.addEventListener ('mouseup',
                    SilkFlo.SideBar.SubmitIdeaMenu.Hide);
            }

            SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.display = 'block';

            if (position === 'right')
                SilkFlo.SideBar.SubmitIdeaMenu.Position(
                    menuId,
                    containerId);
            else
                SilkFlo.SideBar.SubmitIdeaMenu.PositionBottom(
                    menuId,
                    containerId);
        },

        Hide: function ()
        {
            const menu = SilkFlo.SideBar.SubmitIdeaMenu.Menu;

            if (menu && event.target !== menu && event.target.parentNode !== menu)
                menu.style.display = 'none';
        },

        Position: function (menuId, containerId)
        {
            if (SilkFlo.SideBar.SubmitIdeaMenu.Menu === null)
                SilkFlo.SideBar.SubmitIdeaMenu.Menu = document.getElementById (menuId);


            if (SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.display !== 'block')
                return;

            SilkFlo.SideBar.SubmitIdeaMenu.Menu.classList.remove ('modelSubmitIdea_bottom');
            SilkFlo.SideBar.SubmitIdeaMenu.Menu.classList.add ('modelSubmitIdea');


            const anchor = document.getElementById (containerId);
            const rect = anchor.getBoundingClientRect();

            const left = rect.left + rect.width;

            SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.left = left + 'px';
            SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.top = rect.top + 'px';
        },

        PositionBottom: function (menuId, containerId)
        {
            if (SilkFlo.SideBar.SubmitIdeaMenu.Menu === null)
                SilkFlo.SideBar.SubmitIdeaMenu.Menu = document.getElementById (menuId);


            if (SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.display !== 'block')
                return;


            SilkFlo.SideBar.SubmitIdeaMenu.Menu.classList.remove ('modelSubmitIdea');
            SilkFlo.SideBar.SubmitIdeaMenu.Menu.classList.add ('modelSubmitIdea_bottom');

            const anchor = document.getElementById (containerId);
            const rect = anchor.getBoundingClientRect();

            const left = rect.left + (rect.width - 170) / 2;
            const top = rect.top + rect.height + 10;

            SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.left = left + 'px';
            SilkFlo.SideBar.SubmitIdeaMenu.Menu.style.top = top + 'px';
        }
    },

    WorkspaceSelector_OnChange: function (element, navigationIsPracticeAccountId)
    {
        const logPrefix = 'SilkFlo.SideBar.WorkspaceSelector_OnChange: ';

        // Guard Clause
        if (!element)
        {
            console.log (`${logPrefix}element parameter is missing`);
            return;
        }


        // Guard Clause
        if (!navigationIsPracticeAccountId)
        {
            console.log (`${logPrefix}navigationIsPracticeAccountId parameter is missing`);
            return;
        }

        const navigationIsPracticeAccount = document.getElementById (navigationIsPracticeAccountId);

        let isPractice = false;
        if (navigationIsPracticeAccount)
            isPractice = navigationIsPracticeAccount.checked;

        const id = element.value;

        SilkFlo.SideBar.GetClient(
            id,
            isPractice);
    },


    // SilkFlo.SideBar.PracticeAccount_OnInput
    PracticeAccount_OnInput: function (element, navigationSelectorClientId)
    {
        const logPrefix = 'SilkFlo.SideBar.PracticeAccount_OnInput: ';

        // Guard Clause
        if (!element)
        {
            console.log (`${logPrefix}element parameter is missing`);
            return;
        }


        // Guard Clause
        if (!navigationSelectorClientId)
        {
            console.log (`${logPrefix}navigationSelectorClientId parameter is missing`);
            return;
        }


        const cboClient = document.getElementById ('Navigation.SelectorClientId');


        // Guard Clause
        if (!cboClient)
        {
            console.log (`${logPrefix}navigationSelectorClientId parameter does not produce an element`);
            return;
        }

        const id = cboClient.value;
        const isPractice = element.checked;

        SilkFlo.SideBar.GetClient (id,
                                   isPractice);
    },


    GetClient: function (id, isPractice)
    {
        // Guard Clause
        if (!id)
        {
            const logPrefix = 'SilkFlo.SideBar.GetClient: ';
            console.log (`${logPrefix}No value`);
            return;
        }


        // Choose an Id production or practice
        let productionId;
        let practiceId;
        const indexOfColon = id.indexOf(',');

        if (indexOfColon > -1)
        {
            const ids = id.split (',');
            [productionId, practiceId] = ids;
        }
        else
        {
            productionId = id;
            practiceId = id;
            isPractice = false;
        }


        // Select an Id
        let selectedId = productionId;
        if (isPractice)
            selectedId = practiceId;


        SilkFlo.SetCookie ('ClientId',
            selectedId,
            1000);

        SilkFlo.SetCookie ('IsPractice',
            isPractice,
            1000);


        SilkFlo.SideBar.GetNavigation ();


        const url = `/Dashboard/clientid/${selectedId}`;


        window.history.pushState ('',
            location.hostname,
            url);



        SilkFlo.DataAccess.UpdateElementById (
            `/api${url}`,
            '',
            'content',
            url,
            'SET');

        SilkFlo.TogglePracticeMode ();
    },


    // SilkFlo.SideBar.GetNavigation
    GetNavigation: function ()
    {
        const id = 'navigation';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.SideBar.GetNavigation: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        SilkFlo.DataAccess.UpdateElement(
            '/api/GetNavigation',
            null,
            element,
            '',
            'GET',
            SilkFlo.SideBar.GetNavigation_Callback);
    },


    // SilkFlo.SideBar.GetNavigation_Callback
    GetNavigation_Callback: function ()
    {
        const id = 'btnSubmitContainer';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.SideBar.GetNavigation_Callback: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        SilkFlo.DataAccess.UpdateElement(
            '/api/business/Idea/GetSubmitIdeaButton',
            null,
            element,
            '',
            'GET',
            SilkFlo.SideBar.SetMenu);

        SilkFlo.DataAccess.LoadOnce(element);

        SilkFlo.SideBar.NavigationFirstLoad_Callback();
    },



    // SilkFlo.SideBar.NavigationFirstLoad_Callback
    NavigationFirstLoad_Callback: function ()
    {
        let id = 'NavigationIsPracticeAccount_Container';
        const elementIsPracticeAccount = document.getElementById(id);

        // Guard Clause
        if (!elementIsPracticeAccount)
            return;



        id = 'NavigationInviteTeam';
        const elementInviteTeam = document.getElementById(id);

        // Guard Clause
        if (!elementInviteTeam)
        {
            const logPrefix = 'SilkFlo.SideBar.NavigationFirstLoad_Callback: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }



        id = 'SideBar.Workshop.Container';
        const elementWorkshop = document.getElementById(id);


        if (elementWorkshop)
        {
            elementIsPracticeAccount.style.display = '';
            elementInviteTeam.style.display = '';
        }
        else
        {
            elementIsPracticeAccount.style.display = 'none';
            elementInviteTeam.style.display = 'none';
        }



        const isPractice = SilkFlo.GetCookie('IsPractice');

        if (elementWorkshop && isPractice.toLowerCase() === 'false')
            elementInviteTeam.style.display = '';
        else
            elementInviteTeam.style.display = 'none';
    },




    // SilkFlo.SideBar.ShowModelSubmitIdea
    ShowModelSubmitIdea: function ()
    {

        const id = 'ModalSubmitIdeaContent';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.SideBar.ShowModelSubmitIdea: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        element.style.paddingTop = '1rem;';
        element.style.paddingLeft = '1rem;';

        SilkFlo.DataAccess.UpdateElement (
            '/api/Idea/Modal',
            null,
            element,
            '',
            'GET',
            SilkFlo.ViewModels.Business.Idea.Modal.Reset);
    },

    ContentLoaded: function ()
    {
        SilkFlo.SideBar.SetMenu();

        window.scroll({
            top: 0,
            left: 0
        });
    }
};