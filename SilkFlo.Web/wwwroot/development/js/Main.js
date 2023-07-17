if (!SilkFlo.Main)
    SilkFlo.Main = {};

SilkFlo.Main.Window = {
    SideBarWideClass: 'sideBar_wide',


    OnResize: function ()
    {
        const logPrefix = 'SilkFlo.Main.Window.OnResize: ';

        const layout = document.getElementById ( 'layout' );

        // Guard Clause
        if (!layout)
        {
            console.log ( logPrefix + 'layout element is missing.' );
            return;
        }

        const topBar = document.getElementById ( 'topBar' );

        // Guard Clause
        if (!topBar)
        {
            console.log ( logPrefix + 'topBar element is missing.' );
            return;
        }


        const content = document.getElementById ( 'content' );

        // Guard Clause
        if (!content)
        {
            console.log ( logPrefix + 'content element is missing.' );
            return;
        }



        if (window.outerWidth < 600)
        {
            layout.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );
            topBar.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );
            content.style.marginLeft = 'var(--silkFlo_sidebar_width)';
            SilkFlo.Main.Window.ToggleSideBarElement ( true );
        }
        else
        {
            if (SilkFlo.GetCookie ( 'sidebarNarrow' )
                .length ===
                0)
            {
                layout.classList.add ( SilkFlo.Main.Window.SideBarWideClass );
                topBar.classList.add ( SilkFlo.Main.Window.SideBarWideClass );
                content.style.marginLeft = 'var(--silkFlo_sidebar_width_wide)';
                SilkFlo.Main.Window.ToggleSideBarElement ( false );
            }
        }
    },

    OnDOMContentLoaded: function ()
    {
        SilkFlo.Analytics.PostURL();
        SilkFlo.Analytics.AssignClickEvents(document);

        // Content observer
        const content = document.getElementById ( 'content' );
        if (content)
        {
            const observer = new MutationObserver (
                function ()
                {
                    SilkFlo.DataAccess.GetComponents ();
                } );
            observer.observe (
                content,
                {
                    characterData: false,
                    childList: true,
                    attributes: false
                } );
        }

        SilkFlo.DataAccess.GetComponents ();
        SilkFlo.DataAccess.GetJsonComponents ( document );
        SilkFlo.ReplaceImgWithSVG();
        Delaney.UI.Grid.Create ();
        HotSpot.Get ( document );
        SilkFlo.TabBar.SetUp(document);
        const layout = document.getElementById ( 'layout' );

        // Guard Clause
        if (!layout)
            return;

        SilkFlo.SVGTools.AnimatePaths ( layout );

        const topBar = document.getElementById ( 'topBar' );

        // Guard Clause
        if (!topBar)
            return;

        const sidebar = document.getElementById ( 'sidebar' );

        // Guard Clause
        if (!sidebar)
            return;

        const sidebarNarrow = SilkFlo.GetCookie ( 'sidebarNarrow' );

        if (sidebarNarrow.length === 0)
        {
            layout.classList.add ( SilkFlo.Main.Window.SideBarWideClass );
            topBar.classList.add ( SilkFlo.Main.Window.SideBarWideClass );
            SilkFlo.Main.Window.ToggleSideBarElement ( false );
        }
        else
        {
            layout.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );
            topBar.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );
            SilkFlo.Main.Window.ToggleSideBarElement ( true );
        }

        SilkFlo.SideBar.SetSideBarDeltaX ( sidebar );
        SilkFlo.SideBar.SetSideBarZeroY ();
        SilkFlo.SideBar.SetWorkspace ();

        const detailBusinessIdeaId = 'Detail_BusinessIdea';
        const detailBusinessIdea = document.getElementById ( detailBusinessIdeaId );
        if (detailBusinessIdea)
        {
            const rate = detailBusinessIdea.querySelector ( '[name="Business.Idea.Rating"]' );
            if (rate)
            {
                DelaneysSlider_Main ( rate );
            }
        }


        const path = window.location.pathname;

        if (path.toLocaleLowerCase ()
            .indexOf ( '/idea/detail' ) >
            -1)
        {
            SilkFlo.Business.Idea.Summary.DetailLoaded ();
        }

    },

    OnReady: function ()
    {
        window.$ ( '#Logo' )
            .on (
                'click',
                function ()
                {
                    SilkFlo.Main.Window.ToggleSidebar ();
                } );

        window.$ ( '#ToggleSidebar' )
            .on (
                'click',
                function ()
                {
                    SilkFlo.Main.Window.ToggleSidebar ();
                } );
    },

    OnPopState: function (e)
    {
        const logPrefix = 'SilkFlo.Main.Window.OnPopState';

        const parentId = 'content';
        const parent = document.getElementById
            (parentId);

        // Guard Clause
        if (!parent) {
            console.log
                (`${logPrefix}Element with id "${parentId}" missing`);
            return;
        }


        // e.state is equal to the data parameter of window.history.pushState(data, title, url);
        if (e.state) {
            SilkFlo.DataAccess.UpdateElement
            (
                '/api' + e.state,
                null,
                parent,
                '',
                'GET',
                SilkFlo.SideBar.SetMenu);
        }
        else {
            SilkFlo.DataAccess.UpdateElement
            (
                '/api/Dashboard',
                null,
                parent,
                '',
                'GET',
                SilkFlo.SideBar.SetMenu);
        }
    },

    ToggleSidebar: function ()
    {
        const logPrefix = 'SilkFlo.Main.Window.ToggleSidebar: ';

        // Toggle menu and store the state
        const layout = document.getElementById ( 'layout' );

        // Guard Clause
        if (!layout)
        {
            console.log ( logPrefix + 'layout element is missing.' );
            return;
        }


        const topBar = document.getElementById ( 'topBar' );

        // Guard Clause
        if (!topBar)
        {
            console.log ( logPrefix + 'topBar element is missing.' );
            return;
        }

        const sidebar = document.getElementById ( 'sidebar' );

        // Guard Clause
        if (!sidebar)
        {
            console.log ( logPrefix + 'sidebar element is missing.' );
            return;
        }

        const content = document.getElementById ( 'content' );

        // Guard Clause
        if (!content)
        {
            console.log ( logPrefix + 'content element is missing.' );
            return;
        }


        let isNarrow = false;
        if (layout.classList.contains ( SilkFlo.Main.Window.SideBarWideClass ))
        {
            layout.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );
            topBar.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );
            SilkFlo.SetCookie (
                'sidebarNarrow',
                'true' );

            sidebar.addEventListener (
                'transitionend',
                SilkFlo.SideBar.AfterResize ( sidebar ) );

            layout.classList.remove ( SilkFlo.Main.Window.SideBarWideClass );


            content.style.marginLeft = 'var(--silkFlo_sidebar_width)';

            isNarrow = true;
        }
        else
        {
            SilkFlo.DeleteCookie ( 'sidebarNarrow' );
            sidebar.addEventListener (
                'transitionend',
                SilkFlo.SideBar.AfterResize ( sidebar ) );

            layout.classList.add ( SilkFlo.Main.Window.SideBarWideClass );
            topBar.classList.add ( SilkFlo.Main.Window.SideBarWideClass );
            content.style.marginLeft = 'var(--silkFlo_sidebar_width_wide)';
        }

        SilkFlo.Main.Window.ToggleSideBarElement ( isNarrow );
    },

    ToggleSideBarElement: function (isNarrow)
    {
        const logPrefix = 'SilkFlo.Main.Window.ToggleSideBarElement: ';

        const id = 'sideBar_arrow';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        if (isNarrow === true) {
            element.classList.add('rotateY180');
        }
        else {
            element.classList.remove('rotateY180');
        }
    }
};